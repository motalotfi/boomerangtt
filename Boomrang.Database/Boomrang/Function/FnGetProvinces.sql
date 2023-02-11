/*
	========================================================================================================
	Create By : F.Ghaffari
	Create Date : 1401/07/15
	Description : دریافت استان ها
	========================================================================================================
*/

	CREATE FUNCTION [dbo].[FnGetProvinces]()

	RETURNS TABLE
	AS
	RETURN
	(	
		SELECT [Id],
			   [Title]	

		FROM  [dbo].[Province]
	)