CREATE TABLE [dbo].[Province]
(
	[Id]            INT            NOT NULL,
    [Title]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
