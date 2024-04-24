if(exists(select * from sys.objects where name = 'proc_AddUserAndAccount'))
	drop procedure proc_AddUserAndAccount
go

create procedure proc_AddUserAndAccount
	@UserName nvarchar(max),
	@FullName nvarchar(max),
	@Phone nvarchar(max),
	@RoleId uniqueidentifier,
	@Password nvarchar(50),
	@Result int output
as
begin

	set nocount on; --Tắt chế độ đếm số dòng tác động bởi câu lệnh.
	set @Result = 1;
	-- Same Username error
	if(exists(select * from Users as u where u.UserName = @UserName))
		begin
			set @Result = -1;
			return;
		end
		
	

	if(exists(select * from Accounts as a where a.UserName = @UserName))
		begin
			set @Result = -1;
			return;
		end

	-- Same Phone error
	if(exists(select * from Users as u where u.Phone = @Phone))
		begin
			set @Result = -2;
			return;
		end

	-- Add
	declare @UserId uniqueidentifier = NEWID(), 
			@AccountId uniqueidentifier = NEWID();


	insert into Users(Id, UserName, FullName, Phone)
	values (@UserId, @UserName, @FullName, @Phone)

	insert into Accounts(Id, UserName, Password, RoleId, UserId)
	values(@AccountId, @UserName, @Password, @RoleId, @UserId)

end
go
-- Test
declare @Result int
exec proc_AddUserAndAccount
	@UserName = N'chuongdoan',
	@Fullname = N'Đoàn Hữu Chương',
	@Phone = N'0289195503',
	@RoleId = 'a02a928b-425c-45dc-9441-82cae13dc44a',
	@Password = '234234',	
	@Result = @Result output
select @Result
select * from Users
select * from Accounts
go


