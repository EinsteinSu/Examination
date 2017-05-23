CREATE TABLE [dbo].[TechReports] (
    [TechReportId] INT            IDENTITY (1, 1) NOT NULL,
    [Subject]      NVARCHAR (200) NOT NULL,
    [Description]  NVARCHAR (MAX) NOT NULL,
    [UserId]       INT            NOT NULL,
    CONSTRAINT [PK_dbo.TechReports] PRIMARY KEY CLUSTERED ([TechReportId] ASC),
    CONSTRAINT [FK_dbo.TechReports_dbo.UserProfiles_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfiles] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[TechReports]([UserId] ASC);

