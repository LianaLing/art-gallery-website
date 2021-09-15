CREATE TABLE [dbo].[ShoppingCart] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [Total]  INT DEFAULT ((0)) NOT NULL,
    [UserId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);