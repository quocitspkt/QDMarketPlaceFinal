using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QDMarketPlace.Models;
using QDMarketPlace.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using QDMarketPlace.Extensions;
using QDMarketPlace.Application.Interfaces;
using QDMarketPlace.Application.ViewModels.Product;
using QDMarketPlace.Data.Enums;
using System.Security.Claims;
using QDMarketPlace.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using PayPal.Core;
using PayPal.Payments;
using BraintreeHttp;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace QDMarketPlace.Controllers
{
    public class CartController : Controller
    {
        private IProductService _productService;
        private IBillService _billService;
        private IViewRenderService _viewRenderService;
        private IKeyService _keyService;
        private IConfiguration _configuration;
        private IEmailSender _emailSender;
        private readonly string _clientId;
        private readonly string _secretKey;

        public decimal TyGiaUSD = 23300;
        public static string content;
        public CartController(IProductService productService,
            IViewRenderService viewRenderService, IEmailSender emailSender,
            IConfiguration configuration, IBillService billService,IConfiguration config,IKeyService keyService)
        {
            _productService = productService;
            _billService = billService;
            _keyService = keyService;
            _viewRenderService = viewRenderService;
            _configuration = configuration;
            _emailSender = emailSender;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];  
        }

        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            return View();
        }
        public List<ShoppingCartViewModel>Carts
        {
            get
            {
                var carts = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
                if (carts ==null)
                {
                    carts = new List<ShoppingCartViewModel>();

                }
                return carts;
            }    
        }

        [Route("checkout.html", Name = "Checkout")]
        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new CheckoutViewModel();
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            model.Carts = session;
            return View(model);
        }

        [Route("checkout.html", Name = "Checkout")]
        [ValidateAntiForgeryToken]
        [ValidateRecaptcha]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            
            if (ModelState.IsValid)
            {
                if (session != null)
                {
                    var details = new List<BillDetailViewModel>();
                    foreach (var item in session)
                    {
                        //string keys = "";
                        //for (int i = 0; i < item.Quantity; i++)
                        //{
                        //    keys = keys + _keyService.GetById(item.Product.Id) + ", ";
                        //    //_keyService.Save();
                        //}
                        details.Add(new BillDetailViewModel()
                        {
                            Product = item.Product,
                            Price = item.Price,
                            ColorId = 1,
                            SizeId = 1,
                            Quantity = item.Quantity,
                            Key = _keyService.GetById(item.Product.Id,item.Quantity),
                            ProductId = item.Product.Id

                        });
                        var billDetailViewModel = new BillDetailViewModel()
                        {
                            Product = item.Product,
                            Price = item.Price,
                            ColorId = 1,
                            SizeId = 1,
                            Quantity = item.Quantity,
                            ProductId = item.Product.Id
                        };
                        _billService.CreateDetail(billDetailViewModel);
                    }
                    var billViewModel = new BillViewModel()
                    {
                        CustomerMobile = model.CustomerMobile,
                        BillStatus = BillStatus.New,
                        CustomerAddress = model.CustomerAddress,
                        CustomerName = model.CustomerName,
                        CustomerMessage = model.CustomerMessage,
                        BillDetails = details,
                        DateCreated = DateTime.Now,
                        
                    };
                    if (User.Identity.IsAuthenticated == true)
                    {
                        billViewModel.CustomerId = Guid.Parse(User.GetSpecificClaim("UserId"));
                    }
                    _billService.Create(billViewModel);
                    
                    
                    try
                    {

                        _billService.Save();
                        var environment = new SandboxEnvironment(_clientId, _secretKey);
                        var client = new PayPalHttpClient(environment);


                        #region Create paypal order
                        var itemList = new ItemList()
                        {
                            Items = new List<Item>()
                        };
                        var total = Math.Round(Carts.Sum(p => p.TotalPrice) / TyGiaUSD, 2);
                        foreach (var item in Carts)
                        {
                            itemList.Items.Add(new Item()
                            {
                                Name = item.Product.Name,
                                Currency = "USD",
                                Price = Math.Round(item.Price / TyGiaUSD, 2).ToString(),
                                Quantity = item.Quantity.ToString(),
                                Sku = "sku",
                                Tax = "0"

                            });
                        }
                        #endregion
                        var paypalOrderId = DateTime.Now.Ticks;
                        var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                        var payment = new Payment()
                        {
                            Intent = "sale",
                            Transactions = new List<Transaction>()
                            {
                                new Transaction()
                                {
                                    Amount = new Amount()
                                    {
                                        Total = total.ToString(),
                                        Currency = "USD",
                                        Details = new AmountDetails
                                        {
                                            Tax ="0",
                                            Shipping ="0",
                                            Subtotal = total.ToString()
                                        }
                                    },
                                    ItemList = itemList,
                                    Description =$"Invoice #{paypalOrderId}",
                                    InvoiceNumber = paypalOrderId.ToString()
                                }
                            },
                            RedirectUrls = new RedirectUrls
                            {
                                CancelUrl = $"{hostname}/Cart/CheckoutFail",
                                ReturnUrl = $"{hostname}/Cart/CheckoutSuccess"
                            },
                            Payer = new Payer()
                            {
                                PaymentMethod = "paypal"
                            }
                        };
                        PaymentCreateRequest request = new PaymentCreateRequest();
                        request.RequestBody(payment);
                        try
                        {
                            var response = await client.Execute(request);
                            var statusCode = response.StatusCode;
                            Payment result = response.Result<Payment>();

                            var links = result.Links.GetEnumerator();
                            string paypalRedirectUrl = null;
                            while (links.MoveNext())
                            {
                                LinkDescriptionObject lnk = links.Current;
                                if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                                {
                                    //saving the payapalredirect URL to which user will be redirected for payment  
                                    paypalRedirectUrl = lnk.Href;
                                }
                            }
                            
                            content = await _viewRenderService.RenderToStringAsync("Cart/_BillMail", billViewModel);
                            //Send mail
                            //await _emailSender.SendEmailAsync(User.GetSpecificClaim("Email"), "Đơn hàng của bạn từ QDMarketPlace", content);
                            

                            return Redirect(paypalRedirectUrl);
                        }
                        catch (HttpException httpException)
                        {
                            var statusCode = httpException.StatusCode;
                            var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                            //Process when Checkout with Paypal fails
                            return Redirect("/Cart/CheckoutFail");
                        }
                        
                        ViewData["Success"] = true;  
                    }
                    catch (Exception ex)
                    {
                        ViewData["Success"] = false;
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            
            model.Carts = session;
            return View(model);
        }

        #region AJAX Request

        /// <summary>
        /// Get list item
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCart()
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session == null)
                session = new List<ShoppingCartViewModel>();
            return new OkObjectResult(session);
        }

        /// <summary>
        /// Remove all products in cart
        /// </summary>
        /// <returns></returns>
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CommonConstants.CartSession);
            return new OkObjectResult("OK");
        }

        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity, int color, int size)
        {
            //Get product detail
            var product = _productService.GetById(productId);

            //Get session with item list from cart
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                //Convert string to list object
                bool hasChanged = false;

                //Check exist with item product id
                if (session.Any(x => x.Product.Id == productId))
                {
                    foreach (var item in session)
                    {
                        //Update quantity for product if match product id
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                            item.Price = product.PromotionPrice ?? product.Price;
                            hasChanged = true;
                        }
                    }
                }
                else
                {
                    session.Add(new ShoppingCartViewModel()
                    {
                        Product = product,
                        Quantity = quantity,
                        Color = _billService.GetColor(color),
                        Size = _billService.GetSize(size),
                        Price = product.PromotionPrice ?? product.Price
                    });
                    hasChanged = true;
                }

                //Update back to cart
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
            }
            else
            {
                //Add new cart
                var cart = new List<ShoppingCartViewModel>();
                cart.Add(new ShoppingCartViewModel()
                {
                    Product = product,
                    Quantity = quantity,
                    Color = _billService.GetColor(color),
                    Size = _billService.GetSize(size),
                    Price = product.PromotionPrice ?? product.Price
                    //Price = product.Price
                });
                HttpContext.Session.Set(CommonConstants.CartSession, cart);
            }
            return new OkObjectResult(productId);
        }

        /// <summary>
        /// Remove a product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult RemoveFromCart(int productId)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                bool hasChanged = false;
                foreach (var item in session)
                {
                    if (item.Product.Id == productId)
                    {
                        session.Remove(item);
                        hasChanged = true;
                        break;
                    }
                }
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
                return new OkObjectResult(productId);
            }
            return new EmptyResult();
        }

        /// <summary>
        /// Update product quantity
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public IActionResult UpdateCart(int productId, int quantity, int color, int size)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                bool hasChanged = false;
                foreach (var item in session)
                {
                    if (item.Product.Id == productId)
                    {
                        var product = _productService.GetById(productId);
                        item.Product = product;
                        item.Size = _billService.GetSize(size);
                        item.Color = _billService.GetColor(color);
                        if (quantity > 6)
                        {
                            item.Quantity = 6;
                        }
                        else
                        {
                            item.Quantity = quantity;
                        }
                        //if (quantity > _productService.GetAmount(productId))
                        //{
                        //    item.Quantity = _productService.GetAmount(productId);
                        //}
                        //else
                        //{
                        //    item.Quantity = quantity;
                        //}
                        item.Price = product.PromotionPrice ?? product.Price;
                        hasChanged = true;
                    }
                }
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
                return new OkObjectResult(productId);
            }
            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult GetColors()
        {
            var colors = _billService.GetColors();
            return new OkObjectResult(colors);
        }

        [HttpGet]
        public IActionResult GetSizes()
        {
            var sizes = _billService.GetSizes();
            return new OkObjectResult(sizes);
        }
        [Authorize]
        public async Task<IActionResult> PaypalCheckout()
        {
            var environment = new SandboxEnvironment(_clientId,_secretKey);
            var client = new PayPalHttpClient(environment);
            

            #region Create paypal order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = Math.Round(Carts.Sum(p => p.TotalPrice) / TyGiaUSD, 2);
            foreach (var item in Carts)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.Product.Name,
                    Currency = "USD",
                    Price = Math.Round(item.Price /TyGiaUSD,2).ToString(),
                    Quantity =item.Quantity.ToString(),
                    Sku ="sku",
                    Tax ="0"

                });
            }
            #endregion
            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax ="0",
                                Shipping ="0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description =$"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls
                {
                    CancelUrl = $"{hostname}/Cart/CheckoutFail",
                    ReturnUrl = $"{hostname}/Cart/CheckoutSuccess"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);
            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Redirect(paypalRedirectUrl);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/Cart/CheckoutFail");
            }
        }
        public IActionResult CheckoutFail()
        { 
            return View();
        }
        public async Task<IActionResult> CheckoutSuccess(CheckoutViewModel model)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            model.Carts = session;
            if (session == null)
                return View();

            //var details = new List<BillDetailViewModel>();
            //foreach (var item in session)
            //{
            //    details.Add(new BillDetailViewModel()
            //    {
            //        Product = item.Product,
            //        Price = item.Price,
            //        ColorId = 1,
            //        SizeId = 1,
            //        Quantity = item.Quantity,
            //        Key = _keyService.GetById(item.Product.Id),
            //        ProductId = item.Product.Id

            //    });
                
            //}

            //var billViewModel = new BillViewModel()
            //{
            //    CustomerMobile = model.CustomerMobile,
            //    BillStatus = BillStatus.New,
            //    CustomerAddress = model.CustomerAddress,
            //    CustomerName = model.CustomerName,
            //    CustomerMessage = model.CustomerMessage,
            //    BillDetails = details,
            //    DateCreated = DateTime.Now,

            //};
            foreach (var item in Carts)
            {
                _productService.SetUnitProduct(item.Product.Id, item.Quantity);
                _productService.Save();
            }

            //var content = await _viewRenderService.RenderToStringAsync("Cart/_BillMail", billViewModel);
            //Send mail
            await _emailSender.SendEmailAsync(User.GetSpecificClaim("Email"), "Đơn hàng của bạn từ QDMarketPlace", content);
            HttpContext.Session.Remove(CommonConstants.CartSession);
            return View();
        }
        #endregion AJAX Request
    }
}