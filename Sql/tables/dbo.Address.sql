CREATE TABLE [dbo].[Address] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [country]     VARCHAR (255) NOT NULL,
    [state]       VARCHAR (255) NOT NULL,
    [city]        VARCHAR (255) NOT NULL,
    [line1]       VARCHAR (255) NOT NULL,
    [line2]       VARCHAR (255) NULL,
    [postal_code] CHAR (5)      NOT NULL,
    [created_at]  DATE          NOT NULL,
    [updated_at]  DATE          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

