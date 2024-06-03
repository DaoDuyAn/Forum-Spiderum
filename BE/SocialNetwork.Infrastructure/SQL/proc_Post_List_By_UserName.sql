if (exists(select * from sys.objects where name = 'proc_Post_List_By_UserName'))
	drop procedure proc_Post_List_By_UserName
go

create procedure proc_Post_List_By_UserName
	@page int = 1,					--Trang cần hiển thị
	@pageSize int = 5,				--Số dòng trên mỗi trang
	@rowCount int output,			--Tổng số dòng tìm đc
	@pageCount int output,			--Tổng số trang
	@userName nvarchar(max),
	@tab nvarchar(50) = 'createdPosts'			
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.

	--Kiểm tra dữ liệu đầu vào.
	if(@page <= 0) set @page = 1;
	if(@pageSize <= 0) set @pageSize = 5;
	if(not exists (select * from Users where UserName = @userName))
		return;

	--Tìm userId theo userName
	declare	@userId uniqueidentifier;
	select	@userId = Id
	from	Users u
	where	u.UserName = @userName;

	--Đếm số dòng.
	if(@tab = 'savedPosts')
		begin
			select @rowCount = count(*)
			from Posts as p 
			where p.UserId in	(
									select s.UserId
									from SavedPosts s
									where s.UserId = @userId
								)
		end
	else
		begin
			select	@rowCount = count(*)
			from	Posts as p
			where	p.UserId = @userId
		end

	--Tính số trang.
	set @pageCount = @rowCount / @pageSize;
	if(@rowCount % @pageSize > 0)
		set @pageCount += 1;

	--Truy vấn dữ liệu.

	if(@tab = 'createdPosts')
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
				where UserId = @userId
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize) 
			order by p.RowNumber
		end


	if(@tab = 'savedPosts')
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
				where UserId in (
									select s.UserId
									from SavedPosts s
									where s.UserId = @userId
								)
				) as p
					join Users u ON p.UserId = u.Id
					join Categories c ON p.CategoryId = c.Id
			where (p.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize) 
			order by p.RowNumber
		end

end
go

--Test case:

declare @page int = 3,
	@pageSize int = 5,
	@rowCount int,
	@pageCount int,
	@userName nvarchar(max),
	@tab nvarchar(50);
execute proc_Post_List_By_UserName
	@page = @page,
	@pageSize = @pageSize,
	@rowCount = @rowCount out,
	@pageCount = @pageCount out,
	@userName = @userName,
	@tab = @tab; 
select @rowCount as [RowCount], @pageCount as [PageCount];
go
