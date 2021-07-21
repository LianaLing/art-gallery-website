CREATE TABLE [dbo].[Payment] (
    [id]                INT          IDENTITY (1, 1) NOT NULL,
    [amount]            MONEY        NOT NULL,
    [currency]          CHAR (3)     NOT NULL,
    [description]       TEXT         NULL,
    [status]            VARCHAR (10) NOT NULL,
    [tax]               SMALLMONEY   NOT NULL,
    [canceled_at]       DATE         NULL,
    [succeeded_at]      DATE         NULL,
    [created_at]        DATE         NOT NULL,
    [updated_at]        DATE         NOT NULL,
    [payment_method_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([payment_method_id]) REFERENCES [dbo].[PaymentMethod] ([id]),
    CHECK ([status]='succeeded' OR [status]='processing' OR [status]='canceled')
);

