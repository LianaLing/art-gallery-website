CREATE TABLE [dbo].[Address] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Country]     VARCHAR (255) NOT NULL,
    [State]       VARCHAR (255) NOT NULL,
    [City]        VARCHAR (255) NOT NULL,
    [Line1]       VARCHAR (255) NOT NULL,
    [Line2]       VARCHAR (255) NULL,
    [PostalCode] CHAR (5)      NOT NULL,
    [CreatedAt]  DATE          NOT NULL,
    [UpdatedAt]  DATE          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

