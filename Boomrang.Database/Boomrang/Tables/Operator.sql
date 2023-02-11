CREATE TABLE [dbo].[Operator]
(
	Id				UNIQUEIDENTIFIER	NOT NULL PRIMARY KEY,
	Admin_Id		UNIQUEIDENTIFIER	NULL,
	UserName		NVARCHAR(50)		NULL,
	[Password]		NVARCHAR(200)		NULL,
	FirstName		NVARCHAR(50)		NULL,
	LastName		NVARCHAR(50)		NULL,
	MobileNumber	CHAR(11)			NULL,
	Gender			INT					NULL,
	Role_Id			INT					NULL,
	CreateDateTime  DATETIMEOFFSET		NULL,
	IsActive		BIT					NULL,
	CONSTRAINT [PK_Operator_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Operator_Admin_Id] FOREIGN KEY ([Admin_Id]) REFERENCES [dbo].[Admin] ([Id])
)
