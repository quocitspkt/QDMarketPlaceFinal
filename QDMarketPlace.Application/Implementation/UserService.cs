using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDMarketPlace.Application.Interfaces;
using QDMarketPlace.Application.ViewModels.System;
using QDMarketPlace.Data.Entities;
using QDMarketPlace.Utilities.Dtos;
using QDMarketPlace.Infrastructure.Interfaces;

namespace QDMarketPlace.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserManager<IdentityResult> _userManagerModel;
        private readonly IMapper _mapper;
        IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<bool> AddAsync(AppUserViewModel userVm)
        {
            var user = new AppUser()
            {
                UserName = userVm.UserName,
                Avatar = userVm.Avatar,
                Email = userVm.Email,
                FullName = userVm.FullName,
                DateCreated = DateTime.Now,
                PhoneNumber = userVm.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, userVm.Password);
            if (result.Succeeded && userVm.Roles.Count > 0)
            {
                var appUser = await _userManager.FindByNameAsync(user.UserName);
                if (appUser != null)
                    await _userManager.AddToRolesAsync(appUser, userVm.Roles);
            }
            return true;
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<AppUserViewModel>> GetAllAsync()
        {
            return await _mapper.ProjectTo<AppUserViewModel>(_userManager.Users).ToListAsync();
        }

        public PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = _userManager.Users.Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.FullName.Contains(keyword)
                || x.UserName.Contains(keyword)
                || x.Email.Contains(keyword));

            int totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize)
               .Take(pageSize);

            var data = query.Select(x => new AppUserViewModel()
            {
                UserName = x.UserName,
                Avatar = x.Avatar,
                BirthDay = (DateTime)x.BirthDay,
                Email = x.Email,
                FullName = x.FullName,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status,
                DateCreated = x.DateCreated
            }).ToList();
            var paginationSet = new PagedResult<AppUserViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public async Task<AppUserViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = _mapper.Map<AppUser, AppUserViewModel>(user);
            userVm.Roles = roles.ToList();
            return userVm;
        }

        public async Task UpdateAsync(AppUserViewModel userVm)
        {
            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());
            //Remove current roles in db
            var currentRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user,
                userVm.Roles.Except(currentRoles).ToArray());

            if (result.Succeeded)
            {
                string[] needRemoveRoles = currentRoles.Except(userVm.Roles).ToArray();
                await _userManager.RemoveFromRolesAsync(user, needRemoveRoles);

                //Update user detail
                user.FullName = userVm.FullName;
                user.BirthDay = userVm.BirthDay;
                user.Status = userVm.Status;
                user.Email = userVm.Email;
                user.PhoneNumber = userVm.PhoneNumber;
                await _userManager.UpdateAsync(user);
            }
        }
        public async Task UpdateAccountAsync(AppUserViewModel userVm)
        {
            //var appUser = _appUserRepository.FindById(userVm.Id);
            //appUser.FullName = userVm.FullName;
            //appUser.BirthDay = userVm.BirthDay;
            //appUser.Email = userVm.Email;
            //appUser.PhoneNumber = userVm.PhoneNumber;
            //appUser.DateCreated = userVm.DateCreated;
            //_appUserRepository.Update(appUser);
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            string pass = "XuanDuc@1999";
            var user = await _userManager.FindByEmailAsync(email);

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user,pass );
            return pass;
        }

        public int CountUser() 
        {
            List<AppUserViewModel> lst = _mapper.ProjectTo<AppUserViewModel>(_userManager.Users).ToList();
            return lst.Count() - 1;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        
    }
}