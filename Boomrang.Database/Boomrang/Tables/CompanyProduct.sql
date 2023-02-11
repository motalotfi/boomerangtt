CREATE TABLE [dbo].[CompanyProduct]
(
	Id						UNIQUEIDENTIFIER	NOT NULL,
	Company_Id				UNIQUEIDENTIFIER	NULL,
	CompanyImportantProduct NVARCHAR(100)		NULL,
	ProductKnowledgeField	NVARCHAR(100)		NULL,
	ProductApplication 	    NVARCHAR(500)		NULL,
	ProductDescription 	    NVARCHAR(1000)		NULL,
	CreateDateTime			DATETIMEOFFSET		NULL,
	IsActive				BIT					NULL,
	CONSTRAINT [PK_CompanyProduct_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_CompanyProduct_Company_Id] FOREIGN KEY ([Company_Id]) REFERENCES [dbo].[Company] ([Id])
)
