CREATE TABLE [dbo].[Author] (
    [Id]          INT  IDENTITY (1, 1) NOT NULL,
    [Description] TEXT NULL,
    [Verified]    BIT  DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

