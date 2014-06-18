-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].getBracketsPredictionsForUserId
	@UserId int
AS
	SELECT s.[id] AS 'situacionId'
		,s.[descripcion] AS 'desc'
		,s.[groupLetter] AS 'etapa'
		,r.[resultado] AS 'resultado'
		,pd.[resultado] AS 'prediccion'
		,p.[puntosGanados] AS 'puntosGanados'
	FROM [dbo].[Situaciones] s
		LEFT JOIN [dbo].[PosiblesResultados] r ON s.[resultado] = r.[id]
		LEFT JOIN (SELECT * FROM [dbo].[Predicciones] WHERE [userId] = @UserId) p ON s.[id] = p.[situacionId] 
		LEFT JOIN [dbo].[PosiblesResultados] pd ON p.[prediccion] = pd.[id]
	WHERE
		[groupLetter] IN ('8','4','2','1')

