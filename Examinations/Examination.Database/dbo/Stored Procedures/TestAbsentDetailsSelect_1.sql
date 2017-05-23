
CREATE Procedure [dbo].[TestAbsentDetailsSelect]
@testId int
as

select u.TestId,u.UserTestId,up.Name,s.Name as SiteName from UserTests as u with (nolock)
join
UserProfiles as up with (nolock) on u.UserId = up.UserId
join
Sites as s with (nolock) on up.SiteId = s.SiteId
where 
status = 0
and
u.TestId = @testId