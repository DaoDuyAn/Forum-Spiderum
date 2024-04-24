if(exists (select * from sys.objects where name = 'proc_UnlikePost'))
	drop proc proc_UnlikePost
go
create proc proc_UnlikePost
	@PostId UNIQUEIDENTIFIER,
	@UserId UNIQUEIDENTIFIER,
	@Result int output
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.
	set @Result = 0;

	if(not exists (select * from Posts where Id = @PostId))
		return;

	-- Invalid UserID
	if(not exists (select * from Users where Id = @UserId))
		return;


	if(not exists (select * from Likes where UserId = @UserId and PostId = @PostId))
		return;

	--  Unlike
	delete from Likes
	where UserId = @UserId and PostId = @PostId

	if (@@ROWCOUNT > 0)
		set @Result = 1
	else 
		set @Result = 0;
end
go
-- TEST
declare @Result int
exec proc_UnlikePost
	@PostId = '2415b4e2-4489-401f-c84b-08dc62dd80ca',
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@Result = @Result output 
select @Result
select * from Likes
select * from Posts
go
