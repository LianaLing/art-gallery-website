CREATE TABLE [dbo].[Author] (
    [id]          INT  IDENTITY (1, 1) NOT NULL,
    [description] TEXT NULL,
    [verified]    BIT  DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

