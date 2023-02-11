CREATE TABLE [dbo].[ActivityField]
(
	[Id]            INT            NOT NULL,
    [Title]         NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ActivityField] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
