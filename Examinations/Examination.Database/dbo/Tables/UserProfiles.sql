CREATE TABLE [dbo].[UserProfiles] (
    [UserId]         INT            IDENTITY (1, 1) NOT NULL,
    [UserCode]       NVARCHAR (6)   NOT NULL,
    [Name]           NVARCHAR (20)  NOT NULL,
    [Password]       NVARCHAR (MAX) NULL,
    [Gender]         INT            DEFAULT ((0)) NOT NULL,
    [Mobile]         NVARCHAR (11)  NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [SiteId]         INT            DEFAULT ((0)) NULL,
    [SecurityRoleId] INT            NULL,
    CONSTRAINT [PK_dbo.UserProfiles] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_dbo.UserProfiles_dbo.SecurityRoles_SecurityRoleId] FOREIGN KEY ([SecurityRoleId]) REFERENCES [dbo].[SecurityRoles] ([SecurityRoleId]),
    CONSTRAINT [FK_dbo.UserProfiles_dbo.Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Sites] ([SiteId])
);








GO
CREATE NONCLUSTERED INDEX [IX_SiteId]
    ON [dbo].[UserProfiles]([SiteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SecurityRoleId]
    ON [dbo].[UserProfiles]([SecurityRoleId] ASC);

