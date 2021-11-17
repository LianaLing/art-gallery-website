CREATE TABLE [dbo].[UserLikes]
(
	[user_id] INT NOT NULL , 
    [art_id] INT NOT NULL, 
    PRIMARY KEY ([user_id], [art_id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]),
    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id])
)
