/*
	========================================================================================================
	Create By : Fatemeh Ghaffari
	Create Date : 1401/07/13
	Description : گرفتن اطلاعات کمپانی
	========================================================================================================
*/

CREATE PROCEDURE [dbo].[MspGetCompanyInformation]

	@mobileNo CHAR(11) = NULL


AS
BEGIN
	
	IF NOT EXISTS (SELECT 1 FROM dbo.Company
				   WHERE MobileNumber = @mobileNo
				   AND IsActive = 1)
		THROW 50003, N'کاربر در سامانه وجود ندارد', 1;


	SELECT CM.Id,
		   CM.FirstName,
		   CM.LastName,
		   CAST(CM.Gender AS INT) AS Gender,
		   CM.Education,
		   CM.Major,
		   CM.City_Id,
		   CM.MobileNumber,
		   CM.EmailAddress,
		   CM.ProficiencyField,
		   CM.ActivityField,
		   CM.CompanyName,
		   CM.UserPosition,
		   CM.IsKnowledgeEnterprise,
		   CM.PhoneNumber,
		   CM.WebsiteAddress,
		   CM.EstablishmentYear,
		   CM.ManpowerCount,
		   CM.DataImporter,
		   CM.DataEntryDateTime,
		   CM.UpdatedDataImporter,
		   CM.DataUpdateDateTime,
		   CP.Id AS CompanyProductId,
		   CP.CompanyImportantProduct,
		   CP.ProductKnowledgeField,
		   CP.ProductApplication,
		   CP.ProductDescription

	FROM dbo.[Company] AS CM
	LEFT JOIN dbo.CompanyProduct AS CP
	ON CM.Id = CP.Company_Id
	WHERE (CM.MobileNumber = @mobileNo OR @mobileNo IS NULL)
	AND CM.IsActive = 1
	AND CP.IsActive = 1
END
GO
