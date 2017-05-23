
CREATE PROCEDURE [dbo].[UserAnswerUpdate]
@userTestId int,
@questionId int,
@answer nvarchar(30)
AS
UPDATE UserAnswers SET Answer = @answer WHERE QuestionId = @questionId AND UserTestId = @userTestId

declare @status int
select @status = status from UserTests where UserTestId = @userTestId
if(@status = 0) begin
	UPDATE UserTests SET Status = 1, StartTime = getdate() where UserTestId = @userTestId
end