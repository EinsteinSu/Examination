
CREATE procedure [dbo].[TestPaperSelectById]
@id int
as

Select t.*,isnull(s.score,0) as Score from TestPapers as t with (nolock) 
left join
	(
	select TestPaperId,SUM(q.score) as score from TestPaperQuestions as tq with (nolock)
		join 
	Questions as q with (nolock) on tq.QuestionId = q.QuestionId
		group by TestPaperId
	) as s on t.TestPaperId = s.TestPaperId
Where t.TestPaperId = @id