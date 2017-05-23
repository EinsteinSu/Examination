CREATE procedure UserTestStatusUpdate
@userId int
as
update UserTests set status = 3, EndTime = t.EndTime from UserTests as u
join Tests as t on u.TestId = t.TestId
where userid = @userId and getdate() >= t.EndTime