if (exists(select * from sys.objects where name = 'proc_Post_List_By_Category'))
	drop procedure proc_Post_List_By_Category
go

create procedure proc_Post_List_By_Category
	@page int = 1,					--Trang cần hiển thị
	@pageSize int = 5,				--Số dòng trên mỗi trang
	@rowCount int output,			--Tổng số dòng tìm đc
	@pageCount int output,			--Tổng số trang
	@categorySlug nvarchar(max),
	@sort nvarchar(50) = 'hot'			
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.

	--Kiểm tra dữ liệu đầu vào.
	if(@page <= 0) set @page = 1;
	if(@pageSize <= 0) set @pageSize = 5;
	if(not exists (select * from Categories where Slug = @categorySlug))
		return;

	--Tìm categoryId theo categorySlug
	declare @categoryId uniqueidentifier;
	select	@categoryId = Id
	from	Categories
	where	slug = @categorySlug;

	--Đếm số dòng.
	select	@rowCount = count(*)
	from	Posts as p
	where	p.CategoryId = @categoryId
	
	--Tính số trang.
	set @pageCount = @rowCount / @pageSize;
	if(@rowCount % @pageSize > 0)
		set @pageCount += 1;

	--Truy vấn dữ liệu.
	if(@sort = 'hot')
		begin
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
					ROW_NUMBER() over(order by LikesCount desc) as RowNumber
				from Posts
				where CategoryId = @categoryId	
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
			order by p.RowNumber
		end

	if(@sort = 'news')
		begin
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
				where CategoryId = @categoryId
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize) 
			order by p.RowNumber
		end

	if(@sort = 'controversial')
		begin
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
					ROW_NUMBER() over(order by CommentsCount desc) as RowNumber
				from Posts
				where CategoryId = @categoryId
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize) 
			order by p.RowNumber
		end

	if(@sort = 'top')
		begin
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
					ROW_NUMBER() over(order by (LikesCount + CommentsCount + SavedCount) desc) as RowNumber
				from Posts
				where CategoryId = @categoryId
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize) 
			order by p.RowNumber
		end

end
go

--Test case:

declare @page int = 1,
	@pageSize int = 5,
	@rowCount int,
	@pageCount int,
	@categorySlug nvarchar(max),
	@sort nvarchar(50);
execute proc_Post_List_By_Category
	@page = @page,
	@pageSize = @pageSize,
	@rowCount = @rowCount out,
	@pageCount = @pageCount out,
	@categorySlug = @categorySlug,
	@sort = @sort; 
select @rowCount as [RowCount], @pageCount as [PageCount];
go
