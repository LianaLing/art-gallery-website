CREATE TABLE [dbo].[User] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [username]      VARCHAR (255) NOT NULL,
    [password_hash] CHAR (128)    NOT NULL,
    [name]          VARCHAR (255) NULL,
    [ic]            CHAR (12)     NULL,
    [dob]           DATE          NULL,
    [avatar_url]    TEXT          NULL,
    [contact_no]    VARCHAR (20)  NULL,
    [email]         VARCHAR (255) NOT NULL,
    [author_id]     INT           NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([author_id]) REFERENCES [dbo].[Author] ([id])
);

