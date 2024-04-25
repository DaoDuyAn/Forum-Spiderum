if(OBJECT_ID('trg_SavedPosts_Insert', 'TR')) is not null
    drop trigger trg_SavedPosts_Insert
go
CREATE TRIGGER trg_SavedPosts_Insert
ON SavedPosts
FOR INSERT
AS

UPDATE Posts
SET Posts.SavedCount = Posts.SavedCount + 1
FROM Posts join inserted ON Posts.Id = inserted.PostId

go

declare @Result int
exec proc_AddSavedPost
	@PostId = '2415b4e2-4489-401f-c84b-08dc62dd80ca',
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@Result = @Result output 
select @Result
select * from SavedPosts
select * from Posts
go
