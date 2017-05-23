CREATE procedure [SecurityRoleActonUpdate]
@roleId int,
@actionList nvarchar(100)
as

begin try
begin transaction

delete from SecurityRoleActions
where
not exists (
	select top 1 1 
		from dbo.DecompressIntList(@actionList,null) as l 
		where
		SecurityRoleActions.SecurityActionId = l.id 
		)
and SecurityRoleActions.SecurityRoleId = @roleId

insert into SecurityRoleActions
select @roleid, id 
	from dbo.DecompressIntList(@actionList,null) as l
	where
	not exists(
		select top 1 1 from SecurityRoleActions as sa 
			where sa.SecurityActionId = l.id and sa.SecurityRoleId = @roleId
			)
commit
end try
begin catch
RAISERROR ('Update Security Role Action failed', 16, 1)
rollback
end catch