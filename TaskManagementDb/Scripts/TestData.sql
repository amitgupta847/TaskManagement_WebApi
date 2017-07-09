declare @statusId int,
	@taskId int,
	@userId int

if not exists (select * from [Users] where Username = 'bhogg')
	INSERT into [dbo].[Users] ([Firstname], [Lastname], [Username]) 
		VALUES (N'Boss', N'Hogg', N'bhogg')

if not exists (select * from [Users] where Username = 'jbob')
	INSERT into [dbo].[Users] ([Firstname], [Lastname], [Username]) 
		VALUES (N'Jim', N'Bob', N'jbob')

if not exists (select * from [Users] where Username = 'jdoe')
	INSERT into [dbo].[Users] ([Firstname], [Lastname], [Username]) 
		VALUES (N'John', N'Doe', N'jdoe')

if not exists(select * from dbo.Tasks where Subject = 'Test Task')
begin
	select top 1 @statusId = StatusId from Status order by StatusId;
	select top 1 @userId = UserId from [Users] order by UserId;

	insert into dbo.Tasks(Subject, StartDate, Status_StatusId, CreatedDate, CreatedBy_UserId)
		values('Test Task', getdate(), @statusId, getdate(), @userId);

	set @taskId = SCOPE_IDENTITY();
	
	INSERT [dbo].[TaskUser] ([TaskId], [UserId]) 
		VALUES (@taskId, @userId)
end





