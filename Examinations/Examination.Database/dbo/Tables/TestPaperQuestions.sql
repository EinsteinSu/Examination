CREATE TABLE [dbo].[TestPaperQuestions] (
    [TestPaperQuestionId] INT IDENTITY (1, 1) NOT NULL,
    [TestPaperId]         INT NOT NULL,
    [QuestionId]          INT NOT NULL,
    CONSTRAINT [PK_dbo.TestPaperQuestions] PRIMARY KEY CLUSTERED ([TestPaperQuestionId] ASC),
    CONSTRAINT [FK_dbo.TestPaperQuestions_dbo.Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([QuestionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TestPaperQuestions_dbo.TestPapers_TestPaperId] FOREIGN KEY ([TestPaperId]) REFERENCES [dbo].[TestPapers] ([TestPaperId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_QuestionId]
    ON [dbo].[TestPaperQuestions]([QuestionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TestPaperId]
    ON [dbo].[TestPaperQuestions]([TestPaperId] ASC);

