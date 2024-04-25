if(OBJECT_ID('trg_Likes_Delete', 'TR')) is not null
    drop trigger trg_Likes_Delete
go
CREATE TRIGGER trg_Likes_Delete
ON Likes
FOR DELETE
AS

UPDATE Posts
SET Posts.LikesCount = Posts.LikesCount - 1
FROM Posts JOIN deleted ON Posts.Id = deleted.PostId

go

declare @Result int
exec proc_UnlikePost
	@PostId = '2415b4e2-4489-401f-c84b-08dc62dd80ca',
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@Result = @Result output 
select @Result
select * from Posts
select * from Likes
go
