CREATE TABLE [dbo].[Favourite] (
    [id]      INT          IDENTITY (1, 1) NOT NULL,
    [name]    VARCHAR (50) NULL,
    [user_id] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id])
);

