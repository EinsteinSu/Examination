CREATE TABLE [dbo].[SecurityRoles] (
    [SecurityRoleId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_dbo.SecurityRoles] PRIMARY KEY CLUSTERED ([SecurityRoleId] ASC)
);

