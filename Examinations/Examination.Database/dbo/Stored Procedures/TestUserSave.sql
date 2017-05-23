


CREATE PROCEDURE [dbo].[TestUserSave]
@testId int,
@siteId int,
@users nvarchar(1000)
AS

BEGIN TRANSACTION
BEGIN TRY
IF(@siteId>0) BEGIN
	DELETE FROM UserTests 
	WHERE
	EXISTS( SELECT * FROM (
				SELECT UT.UserId, UT.TestId FROM UserTests AS UT 
				JOIN
				UserProfiles AS U WITH (NOLOCK) ON U.UserId = UT.UserId
				JOIN
				(SELECT SiteId FROM Sites WITH (NOLOCK) WHERE SiteId = @siteId) AS S ON U.SiteId = S.SiteId) AS C
				WHERE C.UserId = UserTests.UserId AND C.TestId = @testId)
END
ELSE BEGIN
	DELETE FROM UserTests WHERE TestId = @testId
END

INSERT INTO UserTests(UserId,TestId,Status)
SELECT id, @testId,0 FROM dbo.DecompressIntList(@users,',')

--generate user answers
insert into UserAnswers(UserTestId,QuestionId)
select UserTestId,qr.QuestionId from UserTests
left join
(select * from Questions as q
	where
	exists(
		select top 1 1 from TestPaperQuestions as tq
		join
		TestPapers as p on p.TestPaperId = tq.TestPaperId
		join
		Tests as t on t.TestPaperId = p.TestPaperId 
		where tq.QuestionId = q.QuestionId and t.TestId = @testId
		)
) as qr on 0 = 0
where
UserTests.TestId = @testId
and
exists(
	select top 1 1 from dbo.DecompressIntList(@users,',') as d
	where
	d.id = UserTests.UserId
	)



COMMIT TRANSACTION
END TRY
BEGIN CATCH
Raiserror('Update User Test Error',16,1)
ROLLBACK TRANSACTION
END CATCH