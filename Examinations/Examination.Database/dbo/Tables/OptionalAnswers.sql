CREATE TABLE [dbo].[OptionalAnswers] (
    [AnswerId]    INT            IDENTITY (1, 1) NOT NULL,
    [OrderNumber] NVARCHAR (2)   NOT NULL,
    [Content]     NVARCHAR (MAX) NOT NULL,
    [QuestionId]  INT            NOT NULL,
    CONSTRAINT [PK_dbo.OptionalAnswers] PRIMARY KEY CLUSTERED ([AnswerId] ASC),
    CONSTRAINT [FK_dbo.OpitionalAnswers_dbo.Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([QuestionId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_QuestionId]
    ON [dbo].[OptionalAnswers]([QuestionId] ASC);

