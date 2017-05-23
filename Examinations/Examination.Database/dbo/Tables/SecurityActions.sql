CREATE TABLE [dbo].[SecurityActions] (
    [SecurityActionId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_dbo.SecurityActions] PRIMARY KEY CLUSTERED ([SecurityActionId] ASC)
);

