CREATE TABLE [dbo].[Favourite](
	[id] INT IDENTITY(1,1) NOT NULL,
	[name] VARCHAR(50),
    [art_id]  INT NOT NULL,
    [user_id] INT NOT NULL,
    PRIMARY KEY ([id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]),
    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id])
);

