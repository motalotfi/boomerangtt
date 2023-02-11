/*
	========================================================================================================
	Create By : F.Ghaffari
	Create Date : 1401/04/29
	Description : دریافت شهر ها
	========================================================================================================
*/

	CREATE FUNCTION [dbo].[FnGetCities]
	(
		@provinceId INT = NULL
	)

	RETURNS TABLE
	AS
	RETURN
	(	
		SELECT [Id], 
			   [Title]
			   
		FROM  [dbo].[City]

		WHERE [Province_Id] = @provinceId OR @provinceId IS NULL
	)