CREATE TABLE [dbo].[SecurityRoleActions] (
    [SecurityRoleActionId] INT IDENTITY (1, 1) NOT NULL,
    [SecurityRoleId]       INT NOT NULL,
    [SecurityActionId]     INT NOT NULL,
    CONSTRAINT [PK_dbo.SecurityRoleActions] PRIMARY KEY CLUSTERED ([SecurityRoleActionId] ASC),
    CONSTRAINT [FK_dbo.SecurityRoleActions_dbo.SecurityActions_SecurityActionId] FOREIGN KEY ([SecurityActionId]) REFERENCES [dbo].[SecurityActions] ([SecurityActionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.SecurityRoleActions_dbo.SecurityRoles_SecurityRoleId] FOREIGN KEY ([SecurityRoleId]) REFERENCES [dbo].[SecurityRoles] ([SecurityRoleId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SecurityActionId]
    ON [dbo].[SecurityRoleActions]([SecurityActionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SecurityRoleId]
    ON [dbo].[SecurityRoleActions]([SecurityRoleId] ASC);

