CREATE TABLE [dbo].[Questions] (
    [QuestionId]    INT            IDENTITY (1, 1) NOT NULL,
    [Type]          INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Score]         INT            NOT NULL,
    [CorrectAnswer] NVARCHAR (100) NULL,
    [CategoryId]    INT            DEFAULT ((0)) NOT NULL,
    [Deleted]       BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Questions] PRIMARY KEY CLUSTERED ([QuestionId] ASC),
    CONSTRAINT [FK_dbo.Questions_dbo.QuestionCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[QuestionCategories] ([QuestionCategoryId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_CategoryId]
    ON [dbo].[Questions]([CategoryId] ASC);

