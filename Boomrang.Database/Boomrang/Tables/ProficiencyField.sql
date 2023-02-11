CREATE TABLE [dbo].[ProficiencyField]
(
	[Id]            INT            NOT NULL,
    [Title]         NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ProficiencyField] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
