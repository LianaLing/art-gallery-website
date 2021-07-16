CREATE TABLE [dbo].[PaymentMethod] (
    [id]                 INT          IDENTITY (1, 1) NOT NULL,
    [type]               VARCHAR (10) NOT NULL,
    [created_at]         DATE         NOT NULL,
    [updated_at]         DATE         NOT NULL,
    [user_id]            INT          NULL,
    [card_id]            INT          NULL,
    [billing_details_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]),
    FOREIGN KEY ([card_id]) REFERENCES [dbo].[Card] ([id]),
    FOREIGN KEY ([billing_details_id]) REFERENCES [dbo].[BillingDetails] ([id]),
    CHECK ([type]='tng' OR [type]='grabpay' OR [type]='card' OR [type]='fpx')
);

