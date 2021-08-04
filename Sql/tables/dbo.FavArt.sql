CREATE TABLE [dbo].[FavArt] (
    [fav_id] INT NOT NULL,
    [art_id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([fav_id] ASC, [art_id] ASC),
    FOREIGN KEY ([fav_id]) REFERENCES [dbo].[Favourite] ([id]),
    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id])
);

