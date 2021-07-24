CREATE TABLE [dbo].[OrderArt] (
    [art_id]   INT NOT NULL,
    [order_id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([art_id] ASC, [order_id] ASC),
    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id]),
    FOREIGN KEY ([order_id]) REFERENCES [dbo].[Order] ([id])
);

