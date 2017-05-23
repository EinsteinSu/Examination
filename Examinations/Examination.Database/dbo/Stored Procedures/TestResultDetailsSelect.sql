



CREATE procedure [dbo].[TestResultDetailsSelect]
@testId int,
@userTestId int
As

if exists( select top 1 1 from sys.objects where name = '#ScoreTemp' and type = 'U') begin
	drop table #ScoreTemp
end

Create Table #ScoreTemp (TestId int, UserTestId int, Score int)

if @userTestId = 0 begin
	insert into #ScoreTemp
	select  @testId as TestId, UserTestId, 
	Isnull(sum(
		case 
			when ua.Answer = q.CorrectAnswer then q.Score
			when  REPLACE(ua.Answer,',','') = q.CorrectAnswer then q.Score
			else 0 end),0) as Score
	from UserAnswers as ua with (nolock)
	join Questions as q with (nolock) on ua.QuestionId = q.QuestionId
	where
	exists(
		select top 1 1 from UserTests as ut where ut.UserTestId = ua.UserTestId and ut.TestId = @testId
		)
	group by UserTestId
end
else begin
	insert into #ScoreTemp
	select  @testId as TestId, UserTestId, 
	Isnull(sum(
		case 
			when ua.Answer = q.CorrectAnswer then q.Score
			when  REPLACE(ua.Answer,',','') = q.CorrectAnswer then q.Score
			else 0 end),0) as Score
	from UserAnswers as ua with (nolock)
	join Questions as q with (nolock) on ua.QuestionId = q.QuestionId
	where
	exists(
		select top 1 1 from UserTests as ut where ut.UserTestId = ua.UserTestId and ut.TestId = @testId
		)
	and
	UserTestId = @userTestId
	group by UserTestId
end


select ut.UserTestId,up.UserId, up.Name, s.Name as SiteName, st.Score from UserTests as ut with (nolock)
join
#ScoreTemp as st on ut.UserTestId = st.UserTestId
join
UserProfiles as up with (nolock) on ut.UserId = up.UserId
left join
Sites as s with (nolock) on s.SiteId = up.SiteId