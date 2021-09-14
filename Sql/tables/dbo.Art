CREATE TABLE [dbo].[Art] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Style]       VARCHAR (50) NOT NULL,
    [Description] TEXT         NULL,
    [Price]       MONEY        NOT NULL,
    [Stock]       INT          DEFAULT ((1)) NOT NULL,
    [Likes]       INT          DEFAULT ((0)) NOT NULL,
    [Url]         TEXT         NOT NULL,
    [AuthorId]   INT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([author_id]) REFERENCES [dbo].[Author] ([Id])
);

