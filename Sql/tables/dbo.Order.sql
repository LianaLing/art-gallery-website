CREATE TABLE [dbo].[Order] (
    [id]         INT          IDENTITY (1, 1) NOT NULL,
    [status]     VARCHAR (10) NOT NULL,
    [remark]     TEXT         NULL,
    [payment_id] INT          NULL,
    [user_id]    INT          NULL,
    [address_id] INT          NULL,
    [created_at] DATE NULL, 
    [updated_at] DATE NULL, 
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([Id]),
    FOREIGN KEY ([payment_id]) REFERENCES [dbo].[Payment] ([id]),
    FOREIGN KEY ([address_id]) REFERENCES [dbo].[Address] ([id]),
    CHECK ([status] = 'succeeded'
           OR [status] = 'processing'
           OR [status] = 'canceled')
);

