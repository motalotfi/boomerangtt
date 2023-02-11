/*
	========================================================================================================
	Create By : F.Ghaffari
	Create Date : 1401/07/15
	Description : دریافت زمینه فعالیت
	========================================================================================================
*/

	CREATE FUNCTION [dbo].[FnGetActivityField]()

	RETURNS TABLE
	AS
	RETURN
	(	
		SELECT [Id],
			   [Title]	

		FROM  [dbo].[ActivityField]
	)