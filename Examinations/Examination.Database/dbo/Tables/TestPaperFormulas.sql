CREATE TABLE [dbo].[TestPaperFormulas] (
    [FormulaId]     INT IDENTITY (1, 1) NOT NULL,
    [CategoryId]    INT NOT NULL,
    [QuestionCount] INT NOT NULL,
    [TestPaperId]   INT NOT NULL,
    CONSTRAINT [PK_dbo.TestPaperFormulas] PRIMARY KEY CLUSTERED ([FormulaId] ASC),
    CONSTRAINT [FK_dbo.TestPaperFormulas_dbo.QuestionCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[QuestionCategories] ([QuestionCategoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TestPaperFormulas_dbo.TestPapers_TestPaperId] FOREIGN KEY ([TestPaperId]) REFERENCES [dbo].[TestPapers] ([TestPaperId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TestPaperId]
    ON [dbo].[TestPaperFormulas]([TestPaperId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CategoryId]
    ON [dbo].[TestPaperFormulas]([CategoryId] ASC);

