

CREATE procedure [dbo].[TestAbsentSelect]
as
select t.TestId, t.Name as testname, t.StartTime, t.EndTime, tp.Name as testpapername, ut.testerCount, uat.absenttestercount from tests as t with (nolock)
join
TestPapers as tp with (nolock) on t.TestPaperId = tp.TestPaperId
join
(
	select testid, count(testid) as testerCount  from UserTests with (nolock)
	group by
	testid
) as ut on t.testid = ut.TestId
join
(
	select testid, count(testid) as absenttestercount from UserTests as u with (nolock)
	where 
	status = 0
	group by 
	testid
) as uat on t.testid = uat.TestId