CREATE TABLE [dbo].[PosiblesResultados] (
    [id]        INT           IDENTITY (1, 1) NOT NULL,
    [resultado] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_posiblesResultados] PRIMARY KEY CLUSTERED ([id] ASC)
);

