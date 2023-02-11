/*
	========================================================================================================
	Create By : Fatemeh Ghaffari
	Create Date : 1401/07/13
	Description : گرفتن اطلاعات کمپانی
	========================================================================================================
*/

CREATE PROCEDURE [dbo].[MspGetUsersInfo]

	@mobileNo CHAR(11) = NULL,
	@roleType INT = NULL


AS
BEGIN
	
	IF NOT EXISTS (SELECT 1 FROM dbo.[User]
				   WHERE MobileNumber = @mobileNo
				   AND IsActive = 1)
		THROW 50003, N'کاربر در سامانه وجود ندارد', 1;


	SELECT US.Id,
		   US.FirstName,
		   US.LastName,
		   US.MobileNumber AS MobileNo,
		   US.Gender

	FROM dbo.[User] AS US
	WHERE US.RoleType = @roleType
	AND (US.MobileNumber = @mobileNo OR @mobileNo IS NULL)
	AND US.IsActive = 1
END
GO
