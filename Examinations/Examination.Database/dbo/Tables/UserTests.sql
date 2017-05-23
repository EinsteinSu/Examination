CREATE TABLE [dbo].[UserTests] (
    [UserTestId] INT      IDENTITY (1, 1) NOT NULL,
    [UserId]     INT      NOT NULL,
    [TestId]     INT      NOT NULL,
    [Status]     INT      NOT NULL,
    [StartTime]  DATETIME NULL,
    [EndTime]    DATETIME NULL,
    CONSTRAINT [PK_dbo.UserTests] PRIMARY KEY CLUSTERED ([UserTestId] ASC),
    CONSTRAINT [FK_dbo.UserTests_dbo.Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Tests] ([TestId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.UserTests_dbo.UserProfiles_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfiles] ([UserId]) ON DELETE CASCADE
);










GO
CREATE NONCLUSTERED INDEX [IX_TestId]
    ON [dbo].[UserTests]([TestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserTests]([UserId] ASC);


GO

GO
