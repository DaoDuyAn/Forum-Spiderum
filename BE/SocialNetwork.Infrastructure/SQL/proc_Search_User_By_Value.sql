if (exists(select * from sys.objects where name = 'proc_Search_User_By_Value'))
	drop procedure proc_Search_User_By_Value
go

create procedure proc_Search_User_By_Value
	@page int = 1,					--Trang cần hiển thị
	@pageSize int = 5,				--Số dòng trên mỗi trang
	@rowCount int output,			--Tổng số dòng tìm đc
	@pageCount int output,			--Tổng số trang
	@searchValue nvarchar(255) = N''
			
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.

	--Kiểm tra dữ liệu đầu vào.
	if(@page <= 0) set @page = 1;
	if(@pageSize <= 0) set @pageSize = 5;
	if(@searchValue <> N'') set @searchValue = '%' + @searchValue + '%'; 

	--Đếm số dòng.
	select	@rowCount = count(*)
	from	Users as u
	where	(@searchValue = N'') 
		or	(u.UserName like @searchValue);
	
	--Tính số trang.
	set @pageCount = @rowCount / @pageSize;
	if(@rowCount % @pageSize > 0)
		set @pageCount += 1;

	--Truy vấn dữ liệu.
	select  u.UserName as UserName,
            u.AvatarImagePath as AvatarImagePath
	from (
		select  *,
				ROW_NUMBER() over(order by UserName desc) as RowNumber
		from Users
		where	(@searchValue = N'') 
			or	(UserName like @searchValue) 
		) as u	
	where (u.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
	order by u.RowNumber

end
go

--Test case:
declare @page int = 1,
	@pageSize int = 5,
	@rowCount int,
	@pageCount int,
	@searchValue nvarchar(255) = N'';
execute proc_Search_User_By_Value
	@page = @page,
	@pageSize = @pageSize,
	@rowCount = @rowCount out,
	@pageCount = @pageCount out,
	@searchValue = N'an'
select @rowCount as [RowCount], @pageCount as [PageCount];
go
