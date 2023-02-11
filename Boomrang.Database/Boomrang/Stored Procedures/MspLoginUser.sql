/*
	========================================================================================================
	Create By : Fatemeh Ghaffari
	Create Date : 1401/07/15
	Description : لاگین کاربر
	========================================================================================================
*/


CREATE PROCEDURE [dbo].[MspLoginUser]
	
	@userName NVARCHAR(50),
	@password NVARCHAR(200)

AS
BEGIN

	IF NOT EXISTS(SELECT * FROM dbo.[User] AS US
				  WHERE US.UserName = @userName 
				        OR
			            US.[Password] = @password)
		
		THROW 50003, N'نام کاربری یا رمز عبور مطابقت ندارد', 1;


	IF EXISTS(SELECT * FROM dbo.[User] AS US 
			  WHERE US.UserName = @userName 
			  AND US.[Password] = @password)

		SELECT
				US.Id AS [Id],
				US.RoleType AS [RoleType]

		FROM dbo.[User] AS us 
		WHERE US.UserName = @userName 
			  AND US.[Password] = @password


	ELSE
		THROW 50004, N'کاربری با این مشخصه ثبت نام نشده است', 1;
		
END
