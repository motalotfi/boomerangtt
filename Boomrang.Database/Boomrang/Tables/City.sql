CREATE TABLE [dbo].[City]
(
	[Id]            INT            NOT NULL,
    [Province_Id]   INT            NOT NULL,
    [Title]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_Province_Id] FOREIGN KEY ([Province_Id]) REFERENCES [dbo].[Province] ([Id])
)
