

CREATE procedure [dbo].[TestResultSelect]
@testId int
as
if @testId = 0 begin
select t.TestId, t.Name as testname, t.StartTime, t.EndTime, tp.Name as testpapername, ut.testerCount from tests as t with (nolock)
join
TestPapers as tp with (nolock) on t.TestPaperId = tp.TestPaperId
join
(
	select testid, count(testid) as testerCount  from UserTests with (nolock)
	group by
	testid
) as ut on t.testid = ut.TestId
end
else begin
select t.TestId, t.Name as testname, t.StartTime, t.EndTime, tp.Name as testpapername, ut.testerCount from tests as t with (nolock)
join
TestPapers as tp with (nolock) on t.TestPaperId = tp.TestPaperId
join
(
	select testid, count(testid) as testerCount  from UserTests with (nolock)
	group by
	testid
) as ut on t.testid = ut.TestId
where t.TestId = @testId
end