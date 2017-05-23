CREATE TABLE [dbo].[Tests] (
    [TestId]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [EndTime]      DATETIME       NOT NULL,
    [TestPaperId]  INT            NOT NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [Generated]    BIT            DEFAULT ((0)) NOT NULL,
    [GenerateTime] DATETIME       NULL,
    [StartTime]    DATETIME       DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    CONSTRAINT [PK_dbo.Tests] PRIMARY KEY CLUSTERED ([TestId] ASC),
    CONSTRAINT [FK_dbo.Tests_dbo.TestPapers_TestPaperId] FOREIGN KEY ([TestPaperId]) REFERENCES [dbo].[TestPapers] ([TestPaperId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_TestPaperId]
    ON [dbo].[Tests]([TestPaperId] ASC);

