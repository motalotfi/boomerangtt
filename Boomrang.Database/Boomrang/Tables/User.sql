CREATE TABLE [dbo].[User]
(
	Id				UNIQUEIDENTIFIER	NOT NULL ,
	UserName		NVARCHAR(50)		NULL,
	[Password]		NVARCHAR(200)		NULL,
	FirstName		NVARCHAR(50)		NULL,
	LastName		NVARCHAR(50)		NULL,
	MobileNumber	CHAR(11)			NULL,
	Gender			INT					NULL,
	RoleType		INT					NULL,
	CreateDateTime  DATETIMEOFFSET		NULL,
	IsActive		BIT					NULL,
	CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
)
