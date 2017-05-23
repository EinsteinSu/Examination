create procedure TestPaperQuestionDelete
@testPaperId int
as

Delete from TestPaperQuestions where TestPaperId = @testPaperId