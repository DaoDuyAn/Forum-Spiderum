if (exists(select * from sys.objects where name = 'proc_Post_Suggestion_List'))
	drop procedure proc_Post_Suggestion_List
go

create procedure proc_Post_Suggestion_List
	@page int = 1,					--Trang cần hiển thị
	@pageSize int = 9,				--Số dòng trên mỗi trang
	@postSlug nvarchar(max),
	@rowCount int output,			--Tổng số dòng tìm đc
	@pageCount int output			--Tổng số trang
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.

	--Kiểm tra dữ liệu đầu vào.
	if(@page <= 0) set @page = 1;
	if(@pageSize <= 0) set @pageSize = 9;
	if(not exists (select * from Posts where Slug = @postSlug))
		return;

	--Đếm số dòng.
	select	@rowCount = count(*)
	from	Posts as p

	--Tính số trang.
	set @pageCount = @rowCount / @pageSize;
	if (@rowCount % @pageSize > 0)
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
			ROW_NUMBER() over(order by CreationDate desc, (LikesCount + CommentsCount + SavedCount) desc) as RowNumber
		from Posts
		where Slug <> @postSlug 
		) as p
			join Users u ON p.UserId = u.Id
			join Categories c ON p.CategoryId = c.Id
	where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize) 
	order by p.RowNumber
	

end
go

--Test case:

declare @page int = 1,
	@pageSize int = 9,
	@postSlug nvarchar(max),
	@rowCount int,
	@pageCount int;
execute proc_Post_Suggestion_List
	@page = @page,
	@pageSize = @pageSize,
	@rowCount = @rowCount out,
	@pageCount = @pageCount out,
	@postSlug = '234-75e23539';
select @rowCount as [RowCount], @pageCount as [PageCount];
go
