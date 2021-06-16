IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AdvertistmentPages] (
    [Id] nvarchar(20) NOT NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_AdvertistmentPages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AppRoleClaims] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Description] nvarchar(250) NULL,
    CONSTRAINT [PK_AppRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserClaims] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppUserLogins] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [ProviderKey] nvarchar(max) NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserLogins] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [AppUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([RoleId], [UserId])
);

GO

CREATE TABLE [AppUsers] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [FullName] nvarchar(max) NULL,
    [BirthDay] datetime2 NULL,
    [Balance] decimal(18,2) NOT NULL,
    [Avatar] nvarchar(max) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserTokens] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [Blogs] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NOT NULL,
    [Image] nvarchar(256) NULL,
    [Description] nvarchar(500) NULL,
    [Content] nvarchar(max) NULL,
    [HomeFlag] bit NULL,
    [HotFlag] bit NULL,
    [ViewCount] int NULL,
    [Tags] nvarchar(max) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [SeoPageTitle] nvarchar(256) NULL,
    [SeoAlias] nvarchar(256) NULL,
    [SeoKeywords] nvarchar(256) NULL,
    [SeoDescription] nvarchar(256) NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Colors] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(250) NULL,
    [Code] nvarchar(250) NULL,
    CONSTRAINT [PK_Colors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ContactDetails] (
    [Id] nvarchar(255) NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [Phone] nvarchar(50) NULL,
    [Email] nvarchar(250) NULL,
    [Website] nvarchar(250) NULL,
    [Address] nvarchar(250) NULL,
    [Other] nvarchar(max) NULL,
    [Lat] float NULL,
    [Lng] float NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_ContactDetails] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Feedbacks] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(250) NOT NULL,
    [Email] nvarchar(250) NULL,
    [Message] nvarchar(500) NULL,
    [Status] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Footers] (
    [Id] varchar(255) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Footers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Functions] (
    [Id] varchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [URL] nvarchar(250) NOT NULL,
    [ParentId] nvarchar(128) NULL,
    [IconCss] nvarchar(max) NULL,
    [SortOrder] int NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Functions] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Languages] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [IsDefault] bit NOT NULL,
    [Resources] nvarchar(max) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Pages] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NOT NULL,
    [Alias] nvarchar(256) NOT NULL,
    [Content] nvarchar(max) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Pages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ParentId] int NULL,
    [HomeOrder] int NULL,
    [Image] nvarchar(max) NULL,
    [HomeFlag] bit NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    [SortOrder] int NOT NULL,
    [Status] int NOT NULL,
    [SeoPageTitle] nvarchar(max) NULL,
    [SeoAlias] nvarchar(max) NULL,
    [SeoKeywords] nvarchar(max) NULL,
    [SeoDescription] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Sizes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(250) NULL,
    CONSTRAINT [PK_Sizes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Slides] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(250) NOT NULL,
    [Description] nvarchar(250) NULL,
    [Image] nvarchar(250) NOT NULL,
    [Url] nvarchar(250) NULL,
    [DisplayOrder] int NULL,
    [Status] bit NOT NULL,
    [Content] nvarchar(max) NULL,
    [GroupAlias] nvarchar(25) NOT NULL,
    CONSTRAINT [PK_Slides] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SystemConfigs] (
    [Id] nvarchar(255) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value1] nvarchar(max) NULL,
    [Value2] int NULL,
    [Value3] bit NULL,
    [Value4] datetime2 NULL,
    [Value5] decimal(18,2) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_SystemConfigs] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Tags] (
    [Id] varchar(50) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AdvertistmentPositions] (
    [Id] nvarchar(20) NOT NULL,
    [PageId] nvarchar(20) NOT NULL,
    [Name] nvarchar(250) NULL,
    CONSTRAINT [PK_AdvertistmentPositions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AdvertistmentPositions_AdvertistmentPages_PageId] FOREIGN KEY ([PageId]) REFERENCES [AdvertistmentPages] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Announcements] (
    [Id] nvarchar(450) NOT NULL,
    [Title] nvarchar(250) NOT NULL,
    [Content] nvarchar(250) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Announcements] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Announcements_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Bills] (
    [Id] int NOT NULL IDENTITY,
    [CustomerName] nvarchar(256) NOT NULL,
    [CustomerAddress] nvarchar(256) NOT NULL,
    [CustomerMobile] nvarchar(50) NOT NULL,
    [CustomerMessage] nvarchar(256) NOT NULL,
    [PaymentMethod] int NOT NULL,
    [BillStatus] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [CustomerId] uniqueidentifier NULL,
    CONSTRAINT [PK_Bills] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bills_AppUsers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [AppUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Permissions] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [FunctionId] varchar(128) NOT NULL,
    [CanCreate] bit NOT NULL,
    [CanRead] bit NOT NULL,
    [CanUpdate] bit NOT NULL,
    [CanDelete] bit NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Permissions_Functions_FunctionId] FOREIGN KEY ([FunctionId]) REFERENCES [Functions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Permissions_AppRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AppRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [CategoryId] int NOT NULL,
    [Image] nvarchar(255) NULL,
    [Price] decimal(18,2) NOT NULL,
    [PromotionPrice] decimal(18,2) NULL,
    [OriginalPrice] decimal(18,2) NOT NULL,
    [Description] nvarchar(255) NULL,
    [Content] nvarchar(max) NULL,
    [HomeFlag] bit NULL,
    [HotFlag] bit NULL,
    [ViewCount] int NULL,
    [Tags] nvarchar(255) NULL,
    [Unit] nvarchar(255) NULL,
    [SeoPageTitle] nvarchar(max) NULL,
    [SeoAlias] nvarchar(255) NULL,
    [SeoKeywords] nvarchar(255) NULL,
    [SeoDescription] nvarchar(255) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_ProductCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [ProductCategories] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BlogTags] (
    [Id] int NOT NULL IDENTITY,
    [BlogId] int NOT NULL,
    [TagId] varchar(50) NOT NULL,
    CONSTRAINT [PK_BlogTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BlogTags_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blogs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlogTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Advertistments] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(250) NULL,
    [Description] nvarchar(250) NULL,
    [Image] nvarchar(250) NULL,
    [Url] nvarchar(250) NULL,
    [PositionId] nvarchar(20) NULL,
    [Status] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NOT NULL,
    [SortOrder] int NOT NULL,
    CONSTRAINT [PK_Advertistments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Advertistments_AdvertistmentPositions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [AdvertistmentPositions] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AnnouncementUsers] (
    [Id] int NOT NULL IDENTITY,
    [AnnouncementId] nvarchar(450) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [HasRead] bit NULL,
    CONSTRAINT [PK_AnnouncementUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementUsers_Announcements_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcements] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BillDetails] (
    [Id] int NOT NULL IDENTITY,
    [BillId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [ColorId] int NOT NULL,
    [SizeId] int NOT NULL,
    CONSTRAINT [PK_BillDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BillDetails_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [Bills] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BillDetails_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BillDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BillDetails_Sizes_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [Sizes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductImages] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Path] nvarchar(250) NULL,
    [Caption] nvarchar(250) NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductQuantities] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [SizeId] int NOT NULL,
    [ColorId] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_ProductQuantities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductQuantities_Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Colors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductQuantities_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductQuantities_Sizes_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [Sizes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductTags] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [TagId] varchar(50) NOT NULL,
    CONSTRAINT [PK_ProductTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductTags_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [WholePrices] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [FromQuantity] int NOT NULL,
    [ToQuantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_WholePrices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WholePrices_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AdvertistmentPositions_PageId] ON [AdvertistmentPositions] ([PageId]);

GO

CREATE INDEX [IX_Advertistments_PositionId] ON [Advertistments] ([PositionId]);

GO

CREATE INDEX [IX_Announcements_UserId] ON [Announcements] ([UserId]);

GO

CREATE INDEX [IX_AnnouncementUsers_AnnouncementId] ON [AnnouncementUsers] ([AnnouncementId]);

GO

CREATE INDEX [IX_BillDetails_BillId] ON [BillDetails] ([BillId]);

GO

CREATE INDEX [IX_BillDetails_ColorId] ON [BillDetails] ([ColorId]);

GO

CREATE INDEX [IX_BillDetails_ProductId] ON [BillDetails] ([ProductId]);

GO

CREATE INDEX [IX_BillDetails_SizeId] ON [BillDetails] ([SizeId]);

GO

CREATE INDEX [IX_Bills_CustomerId] ON [Bills] ([CustomerId]);

GO

CREATE INDEX [IX_BlogTags_BlogId] ON [BlogTags] ([BlogId]);

GO

CREATE INDEX [IX_BlogTags_TagId] ON [BlogTags] ([TagId]);

GO

CREATE INDEX [IX_Permissions_FunctionId] ON [Permissions] ([FunctionId]);

GO

CREATE INDEX [IX_Permissions_RoleId] ON [Permissions] ([RoleId]);

GO

CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);

GO

CREATE INDEX [IX_ProductQuantities_ColorId] ON [ProductQuantities] ([ColorId]);

GO

CREATE INDEX [IX_ProductQuantities_ProductId] ON [ProductQuantities] ([ProductId]);

GO

CREATE INDEX [IX_ProductQuantities_SizeId] ON [ProductQuantities] ([SizeId]);

GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);

GO

CREATE INDEX [IX_ProductTags_ProductId] ON [ProductTags] ([ProductId]);

GO

CREATE INDEX [IX_ProductTags_TagId] ON [ProductTags] ([TagId]);

GO

CREATE INDEX [IX_WholePrices_ProductId] ON [WholePrices] ([ProductId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210614060905_V0', N'3.1.5');

GO

