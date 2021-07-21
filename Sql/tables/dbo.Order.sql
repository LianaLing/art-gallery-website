CREATE TABLE [dbo].[Order] (
    [id]         INT          IDENTITY (1, 1) NOT NULL,
    [status]     VARCHAR (10) NOT NULL,
    [remark]     TEXT         NULL,
    [payment_id] INT          NULL,
    [art_id]     INT          NULL,
    [user_id]    INT          NULL,
    [address_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([payment_id]) REFERENCES [dbo].[Payment] ([id]),
    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]),
    FOREIGN KEY ([address_id]) REFERENCES [dbo].[Address] ([id])
);

