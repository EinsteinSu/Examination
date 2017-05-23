CREATE TABLE [dbo].[Sites] (
    [SiteId]      INT            IDENTITY (1, 1) NOT NULL,
    [SiteCode]    NVARCHAR (10)  NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Sites] PRIMARY KEY CLUSTERED ([SiteId] ASC)
);



