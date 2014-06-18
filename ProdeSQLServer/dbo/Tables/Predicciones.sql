CREATE TABLE [dbo].[Predicciones] (
    [id]            INT IDENTITY (1, 1) NOT NULL,
    [situacionId]   INT NOT NULL,
    [userId]        INT NOT NULL,
    [prediccion]    INT NOT NULL,
    [puntosGanados] INT NULL,
    CONSTRAINT [PK_Predicciones_1] PRIMARY KEY CLUSTERED ([id] ASC)
);

