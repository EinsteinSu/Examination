CREATE TABLE [dbo].[UserAnswers] (
    [UserAnswerId] INT            IDENTITY (1, 1) NOT NULL,
    [QuestionId]   INT            NOT NULL,
    [Answer]       NVARCHAR (MAX) NULL,
    [UserTestId]   INT            NOT NULL,
    CONSTRAINT [PK_dbo.UserAnswers] PRIMARY KEY CLUSTERED ([UserAnswerId] ASC),
    CONSTRAINT [FK_dbo.UserAnswers_dbo.Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([QuestionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.UserAnswers_dbo.UserTests_UserTestId] FOREIGN KEY ([UserTestId]) REFERENCES [dbo].[UserTests] ([UserTestId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserTestId]
    ON [dbo].[UserAnswers]([UserTestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QuestionId]
    ON [dbo].[UserAnswers]([QuestionId] ASC);

