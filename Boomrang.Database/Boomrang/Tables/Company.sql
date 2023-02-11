﻿CREATE TABLE [dbo].[Company]
(
	Id						UNIQUEIDENTIFIER	NOT NULL PRIMARY KEY,
	FirstName				NVARCHAR(50)		NULL,
	LastName				NVARCHAR(50)		NULL,
	Gender					INT					NULL,
	Education				INT					NULL,
	Major					NVARCHAR(50)		NULL,
	City_Id					INT					NULL,
	MobileNumber			CHAR(11)			NULL,
	EmailAddress			VARCHAR(100)		NULL,
	ProficiencyField		VARCHAR(500)		NULL,
	ActivityField			VARCHAR(500)		NULL,
	CompanyName				NVARCHAR(50)		NULL,
	UserPosition 			NVARCHAR(50)		NULL,
	IsKnowledgeEnterprise  	BIT					NULL,
	PhoneNumber			  	VARCHAR(11)			NULL,
	WebsiteAddress			NVARCHAR(250)		NULL,
	EstablishmentYear		NVARCHAR(250)		NULL,
	ManpowerCount			INT					NULL,
	DataImporter			NVARCHAR(50)		NULL,
	DataEntryDateTime		DATETIMEOFFSET		NULL,
	UpdatedDataImporter		NVARCHAR(50)		NULL,
	DataUpdateDateTime		DATETIMEOFFSET		NULL,
	CreateDateTime			DATETIMEOFFSET		NULL,
	IsActive				BIT					NULL,
	CONSTRAINT [PK_Company_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Company_City_Id] FOREIGN KEY ([City_Id]) REFERENCES [dbo].[City] ([Id])
)