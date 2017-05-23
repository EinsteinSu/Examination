CREATE TABLE [dbo].[QuestionCategories] (
    [QuestionCategoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (20)  NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.QuestionCategories] PRIMARY KEY CLUSTERED ([QuestionCategoryId] ASC)
);

