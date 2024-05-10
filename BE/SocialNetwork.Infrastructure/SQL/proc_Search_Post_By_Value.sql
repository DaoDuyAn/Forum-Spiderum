if (exists(select * from sys.objects where name = 'proc_Search_Post_By_Value'))
	drop procedure proc_Search_Post_By_Value
go

create procedure proc_Search_Post_By_Value
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
	select @rowCount = count(*)
	from Posts as p
	where	(@searchValue = N'') 
		or	(p.Title like @searchValue) 
		or	(p.Description like @searchValue);
	
	--Tính số trang.
	set @pageCount = @rowCount / @pageSize;
	if(@rowCount % @pageSize > 0)
		set @pageCount += 1;

	--Truy vấn dữ liệu.
	select  p.Id,
            p.Title,
            p.Description,
            p.CreationDate,
            p.ThumbnailImagePath,
            p.Slug,
            p.LikesCount,
            p.CommentsCount,
            u.FullName AS FullName,
            u.UserName AS UserName,
            u.AvatarImagePath AS AvatarImagePath,
            c.CategoryName AS CategoryName,
            c.Slug
	from (
		select  *,
			ROW_NUMBER() over(order by CreationDate desc) as RowNumber
		from Posts
		where	(@searchValue = N'') 
			or	(Title like @searchValue) 
			or	(Description like @searchValue)
		) as p
			join Users u ON p.UserId = u.Id
			join Categories c ON p.CategoryId = c.Id
	where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
	order by p.RowNumber

end
go

--Test case:
declare @page int = 1,
	@pageSize int = 5,
	@rowCount int,
	@pageCount int,
	@searchValue nvarchar(255) = N'';
execute proc_Search_Post_By_Value
	@page = @page,
	@pageSize = @pageSize,
	@rowCount = @rowCount out,
	@pageCount = @pageCount out,
	@searchValue = N'test'
select @rowCount as [RowCount], @pageCount as [PageCount];
go
