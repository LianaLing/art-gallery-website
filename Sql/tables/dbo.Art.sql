CREATE TABLE [dbo].[Art] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [style]       VARCHAR (50) NOT NULL,
    [description] TEXT         NULL,
    [price]       MONEY        NOT NULL,
    [stock]       INT          DEFAULT ((1)) NOT NULL,
    [likes]       INT          DEFAULT ((0)) NOT NULL,
	[url]		  TEXT		   NOT NULL,
    [author_id]   INT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([author_id]) REFERENCES [dbo].[Author] ([id])
);

