CREATE TABLE [dbo].[TestPapers] (
    [TestPaperId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (20)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.TestPapers] PRIMARY KEY CLUSTERED ([TestPaperId] ASC)
);



