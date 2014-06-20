-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE getPosiciones 
AS
	SELECT u.[username] AS 'user'
		,SUM(p.[puntosGanados]) AS 'puntos'
	FROM [dbo].[Predicciones] p
		JOIN [dbo].[Usuarios] u ON u.[id] = p.[userId]
	GROUP BY  u.[username]
	ORDER BY [puntos] desc