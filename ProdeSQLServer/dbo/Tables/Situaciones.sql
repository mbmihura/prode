CREATE TABLE [dbo].[Situaciones] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [descripcion] NVARCHAR (50) NOT NULL,
    [resultado]   INT           NULL,
    [date]        DATE          NULL,
    [teamL]       INT           NULL,
    [flagUrlL]    NVARCHAR (50) NULL,
    [teamV]       INT           NULL,
    [flagUrlV]    NVARCHAR (50) NULL,
    [groupLetter] NCHAR (1)     NULL,
    CONSTRAINT [PK_Predicciones] PRIMARY KEY CLUSTERED ([id] ASC)
);

