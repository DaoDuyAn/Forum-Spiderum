if(exists (select * from sys.objects where name = 'proc_UnfollowUser'))
	drop proc proc_UnfollowUser
go
create proc proc_UnfollowUser
	@UserId UNIQUEIDENTIFIER,
	@FollowerId UNIQUEIDENTIFIER,
	@Result int output
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.
	set @Result = 0;

	-- Invalid FollowerId
	if(not exists (select * from Users where Id = @FollowerId))
		return;

	-- Invalid UserId
	if(not exists (select * from Users where Id = @UserId))
		return;

	if(@FollowerId = @UserId)
		return;


	if(not exists (select * from Followers where UserId = @UserId and FollowerId = @FollowerId))
		return;

	--  Unfollow
	delete from Followers
	where UserId = @UserId and FollowerId = @FollowerId

	if (@@ROWCOUNT > 0)
		set @Result = 1
	else 
		set @Result = 0;
end
go
-- TEST
declare @Result int
exec proc_UnfollowUser
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@FollowerId = 'b3d6e9a2-3b26-46a9-8a10-642ab9fd8d91',
	@Result = @Result output 
select @Result
select * from Followers
select * from Users
go
