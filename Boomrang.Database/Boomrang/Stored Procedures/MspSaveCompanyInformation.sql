/*
	========================================================================================================
	Create By : Fatemeh Ghaffari
	Create Date : 1401/07/13
	Description : ذخیره یا آپدیت اطلاعات کاربر
	========================================================================================================
*/

CREATE PROCEDURE [dbo].[MspSaveCompanyInformation]
	
	@userId UNIQUEIDENTIFIER,
	@companyId UNIQUEIDENTIFIER,
	@firstName NVARCHAR(50),
	@lastName NVARCHAR(50),
	@gender	INT,
	@education INT,
	@major NVARCHAR(50),
	@cityId INT,
	@mobileNumber CHAR(11),
	@emailAddress VARCHAR(100),
	@proficiencyField VARCHAR(500),
	@activityField VARCHAR(500),
	@companyName NVARCHAR(50),
	@userPosition NVARCHAR(50),
	@isKnowledgeEnterprise BIT,
	@phoneNumber VARCHAR(11),
	@websiteAddress	NVARCHAR(250),
	@establishmentYear NVARCHAR(250),
	@manpowerCount INT,
	@dataImporter NVARCHAR(50),
	@dataEntryDateTime DATETIMEOFFSET,
	@updatedDataImporter NVARCHAR(50) = NULL,
	@dataUpdateDateTime DATETIMEOFFSET = NULL,
	@companyProduct  [dbo].[TpCompanyProduct]  READONLY


AS
BEGIN

	DECLARE @fullName NVARCHAR(100)
	SELECT @fullName = FirstName + ' ' + LastName
	FROM dbo.[User]
	WHERE Id = @userId

	IF EXISTS (SELECT 1 FROM dbo.Company AS CM
	           WHERE CM.Id = @companyId
			   AND CM.IsActive = 1) 
		BEGIN

		DECLARE @updatedDataImporterOld NVARCHAR(50) = NULL, @dataUpdateDateTimeOld DATETIMEOFFSET = NULL
		SELECT @updatedDataImporter = CP.UpdatedDataImporter, 
		@dataUpdateDateTime = CP.DataUpdateDateTime
		FROM dbo.Company AS CP
	    WHERE CP.MobileNumber = @mobileNumber
		AND CP.IsActive = 1 

		IF @updatedDataImporter != NULL
			
			UPDATE dbo.Company
			SET FirstName = @firstName,
			LastName = @lastName,
			Gender = @gender,
			Education = @education,
			Major = @major,
			City_Id = @cityId,
			MobileNumber = @mobileNumber,
			EmailAddress = @emailAddress,
			ProficiencyField = @proficiencyField,
			ActivityField = @activityField,
			CompanyName = @companyName,
			UserPosition = @userPosition,
			IsKnowledgeEnterprise = @isKnowledgeEnterprise,
			PhoneNumber = @phoneNumber,
			WebsiteAddress = @websiteAddress,
			EstablishmentYear = @establishmentYear,
			ManpowerCount = @manpowerCount,
			DataImporter = @updatedDataImporter,
			DataEntryDateTime = @dataUpdateDateTime,
			UpdatedDataImporter = @fullName,
			DataUpdateDateTime = GETDATE()
			WHERE Id = @companyId
			AND IsActive = 1

		ELSE

			UPDATE dbo.Company
			SET FirstName = @firstName,
			LastName = @lastName,
			Gender = @gender,
			Education = @education,
			Major = @major,
			City_Id = @cityId,
			MobileNumber = @mobileNumber,
			EmailAddress = @emailAddress,
			ProficiencyField = @proficiencyField,
			ActivityField = @activityField,
			CompanyName = @companyName,
			UserPosition = @userPosition,
			IsKnowledgeEnterprise = @isKnowledgeEnterprise,
			PhoneNumber = @phoneNumber,
			WebsiteAddress = @websiteAddress,
			EstablishmentYear = @establishmentYear,
			ManpowerCount = @manpowerCount,
			UpdatedDataImporter = @fullName,
			DataUpdateDateTime = GETDATE()
			WHERE Id = @companyId
			AND IsActive = 1
		END

	ElSE 
		BEGIN

		INSERT INTO dbo.Company
		(
			Id,
			FirstName,
			LastName,
			Gender,
			Education,
			Major,
			City_Id,
			MobileNumber,
			EmailAddress,
			ProficiencyField,
			ActivityField,
			CompanyName,
			UserPosition,
			IsKnowledgeEnterprise,
			PhoneNumber,
			WebsiteAddress,
			EstablishmentYear,
			ManpowerCount,
			DataImporter,
			DataEntryDateTime,
			CreateDateTime,
			IsActive
		)
		VALUES
		(
			@companyId,
		    @firstName,
			@lastName,
			@gender,
			@education,
			@major,
			@cityId,
			@mobileNumber,
			@emailAddress,
			@proficiencyField,
			@activityField,
			@companyName,
			@userPosition,
			@isKnowledgeEnterprise,
			@phoneNumber,
			@websiteAddress,
			@establishmentYear,
			@manpowerCount,
			@fullName,
			GETDATE(),
			SYSDATETIMEOFFSET(),
			1
		)

		END

		INSERT INTO dbo.CompanyProduct
		(
			Id,
			Company_Id,
			CompanyImportantProduct,
			ProductKnowledgeField,
			ProductApplication,
			ProductDescription,
			CreateDateTime,
			IsActive
		)
		SELECT 
			NEWID(),
			@companyId,
			CP.CompanyImportantProduct,
			CP.ProductKnowledgeField,
			CP.ProductApplication,
			CP.ProductDescription,
			SYSDATETIMEOFFSET(),
			1

		FROM @companyProduct AS CP
		WHERE CP.Id = NULL


		UPDATE CP
		SET Id = CD.Id,
			Company_Id = @companyId,
			CompanyImportantProduct = CD.CompanyImportantProduct,
			ProductKnowledgeField = CD.ProductKnowledgeField,
			ProductApplication = CD.ProductApplication,
			ProductDescription = CD.ProductDescription

		FROM dbo.CompanyProduct AS CP
		INNER JOIN @companyProduct AS CD
		ON CP.Id = CD.Id
		WHERE CP.IsActive = 1

END

