CREATE TABLE [dbo].[User] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [UserName]      NVARCHAR(256) NOT NULL,
    [PasswordHash] NVARCHAR(MAX)    NULL,
    [name]          VARCHAR (255) NULL,
    [ic]            CHAR (12)     NULL,
    [dob]           DATE          NULL,
    [avatar_url]    TEXT          NULL,
    [PhoneNumber]    NVARCHAR(MAX)  NULL,
    [Email]         VARCHAR (255) NOT NULL,
    [author_id]     INT           NULL,
    [EmailConfirmed] BIT NOT NULL DEFAULT 0, 
    [SecurityStamp] NVARCHAR(MAX) NULL, 
    [PhoneNumberConfirmed] BIT NOT NULL DEFAULT 0, 
    [TwoFactorEnabled] BIT NOT NULL DEFAULT 0, 
    [LockoutEndDateUtc] DATETIME NULL, 
    [LockoutEnabled] BIT NOT NULL DEFAULT 0, 
    [AccessFailedCount] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([author_id]) REFERENCES [dbo].[Author] ([id])
);

