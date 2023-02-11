
CREATE TYPE [dbo].[TpCompanyProduct] AS TABLE (
    [Id]                        UNIQUEIDENTIFIER NULL,
    [CompanyImportantProduct]   NVARCHAR(100)	 NULL,
    [ProductKnowledgeField]     NVARCHAR(100)	 NULL,
    [ProductApplication]        NVARCHAR(500)	 NULL,
    [ProductDescription]        NVARCHAR(1000)	 NULL
)
    
