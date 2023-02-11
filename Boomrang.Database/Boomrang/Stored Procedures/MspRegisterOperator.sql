/*
	========================================================================================================
	Create By : Fatemeh Ghaffari
	Create Date : 1401/07/13
	Description : ذخیره اپراتور جدید
	========================================================================================================
*/

CREATE PROCEDURE [dbo].[MspRegisterOperator]

	@userId       UNIQUEIDENTIFIER  NULL,
	@userName	  NVARCHAR(50)		NULL,
	@password	  NVARCHAR(200)	    NULL,
	@mobileNo	  CHAR(11)			NULL,
	@firstName    NVARCHAR(50)		NULL,
	@lastName     NVARCHAR(50)		NULL,
	@gender		  INT				NULL,
	@roleType     INT				NULL


AS
BEGIN

	BEGIN TRY
		BEGIN TRAN

			
			IF EXISTS (SELECT * FROM dbo.[User] AS US
						WHERE US.UserName = LTRIM(RTRIM(@userName)))

				THROW 50001, N'نام کاربری وارد شده موجود است. لطفا نام کاربری دیگری را انتخاب کنید',1;


			INSERT INTO dbo.[User]
			(
				Id,
				FirstName,
				LastName,
				MobileNumber,
				Gender,
				CreateDateTime,
				IsActive
			)
			VALUES
			(
				NEWID(),
				@firstName,
				@lastName,
				@mobileNo,
				@gender,
				GETDATE(),
				@roleType
			)

			
		COMMIT TRAN;
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN;
		THROW;
	END CATCH
	
END
GO
