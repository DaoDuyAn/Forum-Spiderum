--- proc_Account_ChangePassword: thực hiện chức năng đổi mật khẩu

if (exists(select * from sys.objects where name = 'proc_Account_ChangePassword'))
	drop procedure proc_Account_ChangePassword;
go
create procedure proc_Account_ChangePassword
	@OldPassword nvarchar(100),
	@NewPassword nvarchar(100),
	@ConfirmPassword nvarchar(100),
	@UserId uniqueidentifier,
	@Result int OUTPUT
as
begin
	set nocount on;

	if(@UserId is null)
		begin
			set @Result = -1;
			return;
		end;

	if(not exists (select * from Accounts where UserId = @UserId))
		begin
			set @Result = -2;
			return;
		end;

	if ((@OldPassword is null) or (@OldPassword = N''))
		begin
			set @Result = -3;
			return;
		end;
		
	if ((@NewPassword is null) or (@NewPassword = N''))
		begin
			set @Result = -4;
			return;
		end;

	if ((@ConfirmPassword is null) or (@ConfirmPassword = N''))
		begin
			set @Result = -5;
			return;
		end;

	if(@OldPassword = @NewPassword)
		begin
			set @Result = -6;
			return;
		end;

	if(@NewPassword != @ConfirmPassword)
		begin
			set @Result = -7;
			return;
		end;
	
	set @Result = 1;

	update	Accounts
	set		Password = @NewPassword 
	where	UserId = @UserId

end
go

-- Test thủ tục:
declare @Result int;
exec proc_Account_ChangePassword
	@UserId = 'bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb',
	@OldPassword = N'234234',
	@NewPassword = N'234234',
	@ConfirmPassword = N'2342345',
	@Result = @Result output;
select  @Result;
select * from Accounts
go