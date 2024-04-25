if(exists (select * from sys.objects where name = 'proc_UnfollowCategory'))
	drop proc proc_UnfollowCategory
go
create proc proc_UnfollowCategory
	@UserId UNIQUEIDENTIFIER,
	@CategoryId UNIQUEIDENTIFIER,
	@Result int output
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.
	set @Result = 0;

	-- Invalid CategoryId
	if(not exists (select * from Categories where Id = @CategoryId))
		return;

	-- Invalid UserId
	if(not exists (select * from Users where Id = @UserId))
		return;

	if(@CategoryId = @UserId)
		return;

	if(not exists (select * from UserCategoryFollowings where UserId = @UserId and CategoryId = @CategoryId))
		return;

	--  Unfollow
	delete from UserCategoryFollowings
	where UserId = @UserId and CategoryId = @CategoryId

	if (@@ROWCOUNT > 0)
		set @Result = 1
	else 
		set @Result = 0;
end
go
-- TEST
declare @Result int
exec proc_UnfollowCategory
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@CategoryId = 'a7b7fd22-97f7-4ae0-8f11-7fe83c22d812',
	@Result = @Result output 
select @Result
select * from UserCategoryFollowings
select * from Users
go
