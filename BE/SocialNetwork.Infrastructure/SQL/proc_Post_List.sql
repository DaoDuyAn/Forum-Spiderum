if (exists(select * from sys.objects where name = 'proc_Post_List'))
	drop procedure proc_Post_List
go

create procedure proc_Post_List
	@page int = 1,					--Trang cần hiển thị
	@pageSize int = 5,				--Số dòng trên mỗi trang
	@rowCount int output,			--Tổng số dòng tìm đc
	@pageCount int output,			--Tổng số trang
	@userId uniqueidentifier,
	@sort nvarchar(50)				
as
begin
	set nocount on;

	--Kiểm tra dữ liệu đầu vào.
	if(@page <= 0) set @page = 1;
	if(@pageSize <= 0) set @pageSize = 5;


	--Đếm số dòng.
	if(@sort = 'follow')
		begin
			select @rowCount = count(*)
			from Posts as p 
			where p.UserId in	(
									select f.UserId
									from Followers f
									where f.FollowerId = @userId
								)
		end
	else
		begin
			select @rowCount = count(*)
			from Posts as p
		end
	

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
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize
			order by p.RowNumber
		end

	if(@sort = 'follow')
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
					ROW_NUMBER() over(order by Id) as RowNumber
				from Posts po
				where po.UserId in	(
										select f.UserId
										from Followers f
										where f.FollowerId = @userId
									)
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize
			order by p.RowNumber
		end

	if(@sort = 'new')
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
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize
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
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize
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
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id

			where p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize
			order by p.RowNumber
		end
	

end
go

--Test case:

declare @page int = 1,
	@pageSize int = 5,
	@rowCount int,
	@pageCount int,
	@userId uniqueidentifier,
	@sort nvarchar(50);
execute proc_Post_List
	@page = @page,
	@pageSize = @pageSize,
	@rowCount = @rowCount out,
	@pageCount = @pageCount out,
	@userId = @userId,
	@sort = @sort; 
select @rowCount as [RowCount], @pageCount as [PageCount];
go