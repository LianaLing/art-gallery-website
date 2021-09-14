CREATE TABLE [dbo].[CartItem] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [Quantity] INT DEFAULT ((1)) NOT NULL,
    [ArtId]    INT NOT NULL,
    [CartId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ArtId]) REFERENCES [dbo].[Art] ([id]),
    FOREIGN KEY ([CartId]) REFERENCES [dbo].[ShoppingCart] ([Id])
);

