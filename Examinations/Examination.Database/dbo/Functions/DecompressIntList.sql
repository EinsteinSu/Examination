CREATE FUNCTION [dbo].[DecompressIntList](@list nvarchar(1000), @split char(1))
RETURNS @table table(id int)
AS
BEGIN
if(ISNULL(@list,'') = '') BEGIN
	return
END
declare @position int, @tempId nvarchar(10)

if(@split is null) begin
	set @split = ',';
end
set @list = RTRIM(LTRIM(@list));
set @list = @list + @split;

set @position = ISNULL(CHARINDEX(@split,@list),0);
while @position > 0 begin
	set @tempId = LTRIM(SUBSTRING(@list,0,@position));
	insert into @table (id) values(convert(int,@tempId));
	set @list = SUBSTRING(@list,@position +1,LEN(@list));
	set @position = CHARINDEX(@split,@list);
end
return
END