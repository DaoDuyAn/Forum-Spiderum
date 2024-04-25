if(exists (select * from sys.objects where name = 'proc_AddLikePost'))
	drop proc proc_AddLikePost
go
create proc proc_AddLikePost
	@PostId UNIQUEIDENTIFIER,
	@UserId UNIQUEIDENTIFIER,
	@Result int output
as
begin
	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.
	set @Result = 0;

	-- Invalid PostID
	if(not exists (select * from Posts where Id = @PostId))
		return;

	-- Invalid UserID
	if(not exists (select * from Users where Id = @UserId))
		return;

	if(@UserId = @PostId)
		return;

	-- Add like
	declare @Id uniqueidentifier = NEWID();

	insert into Likes(Id, UserId, PostId)
	values (@Id, @UserId, @PostId)
	set @Result = 1;
end
go
-- TEST
declare @Result int
exec proc_AddLikePost
	@PostId = '2415b4e2-4489-401f-c84b-08dc62dd80ca',
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@Result = @Result output 
select @Result
select * from Likes
select * from Posts
go
