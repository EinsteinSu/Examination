


CREATE procedure [dbo].[UserTestDetailsSelect]
@userTestId int
as
select ua.UserTestId, [Sequence] = identity(int,1,1) , q.Content as QuestionContent,q.QuestionId, q.CorrectAnswer, isnull(ua.Answer,'') as Answer
into #temp from UserAnswers as ua with (nolock)
join
questions as q with (nolock) on ua.QuestionId = q.QuestionId
join
UserTests as ut with (nolock) on ut.UserTestId = ua.UserTestId
where ua.UserTestId = @userTestId

select * from #temp