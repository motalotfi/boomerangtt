/*
	========================================================================================================
	Create By : Fatemeh Ghaffari
	Create Date : 1401/07/15
	Description : گرفتن اطلاعات ادمین ها یا اپراتورها
	========================================================================================================
*/

CREATE FUNCTION [dbo].[FnGetUsersInfo]
(
	@mobileNo CHAR(11) = NULL,
	@roleType INT = NULL
)
RETURNS TABLE
AS
RETURN
(
	SELECT US.Id,
		   US.FirstName,
		   US.LastName,
		   US.MobileNumber AS MobileNo,
		   US.Gender

	FROM dbo.[User] AS US
	WHERE US.RoleType = @roleType
	AND (US.MobileNumber = @mobileNo OR @mobileNo IS NULL)
)

