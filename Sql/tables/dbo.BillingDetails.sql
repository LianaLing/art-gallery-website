CREATE TABLE [dbo].[BillingDetails] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [name]       VARCHAR (255) NOT NULL,
    [email]      VARCHAR (255) NOT NULL,
    [phone]      VARCHAR (20)  NULL,
    [created_at] DATE          NOT NULL,
    [updated_at] DATE          NOT NULL,
    [address_id] INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([address_id]) REFERENCES [dbo].[Address] ([id])
);

