CREATE TABLE [dbo].[Favourite] (
    [art_id]  INT NOT NULL,
    [user_id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([art_id] ASC, [user_id] ASC),
    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id])
);

