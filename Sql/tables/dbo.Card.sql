CREATE TABLE [dbo].[Card] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [brand]      VARCHAR (10)  NOT NULL,
    [name]       VARCHAR (255) NOT NULL,
    [exp_month]  VARCHAR (2)   NOT NULL,
    [exp_year]   CHAR (4)      NOT NULL,
    [last4]      CHAR (4)      NOT NULL,
    [created_at] DATE          NOT NULL,
    [updated_at] DATE          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CHECK ([brand]='unknown' OR [brand]='mastercard' OR [brand]='unionpay' OR [brand]='amex' OR [brand]='visa')
);

