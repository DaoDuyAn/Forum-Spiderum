if(OBJECT_ID('trg_SavedPosts_Delete', 'TR')) is not null
    drop trigger trg_SavedPosts_Delete
go
CREATE TRIGGER trg_SavedPosts_Delete
ON SavedPosts
FOR DELETE
AS

UPDATE Posts
SET Posts.SavedCount = Posts.SavedCount - 1
FROM Posts JOIN deleted ON Posts.Id = deleted.PostId

go

declare @Result int
exec proc_UnsavedPost
	@PostId = '2415b4e2-4489-401f-c84b-08dc62dd80ca',
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@Result = @Result output 
select @Result
select * from Likes
select * from Posts
go