/*
	========================================================================================================
	Create By : F.Ghaffari
	Create Date : 1401/07/15
	Description : دریافت حوزه تخصصی
	========================================================================================================
*/

	CREATE FUNCTION [dbo].[FnGetProficiencyField]()

	RETURNS TABLE
	AS
	RETURN
	(	
		SELECT [Id],
			   [Title]	

		FROM  [dbo].[ProficiencyField]
	)