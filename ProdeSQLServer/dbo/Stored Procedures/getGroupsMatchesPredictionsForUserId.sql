-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getGroupsMatchesPredictionsForUserId]
	-- Add the parameters for the stored procedure here
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  s.id as 'situacionId'
		,s.groupLetter as 'groupLetter'
		,tL.resultado as 'teamL'
		,tV.resultado as 'teamV'
		,pt.resultado as 'prediccion'
		,r.resultado as 'resultado'
		,p.puntosGanados as 'puntosGanados'
		,s.[date] as 'date'
	FROM [dbo].[Situaciones] s 
		JOIN [dbo].[PosiblesResultados] tL ON tL.id = s.teamL
		JOIN [dbo].[PosiblesResultados] tV ON tV.id = s.teamV
		LEFT JOIN [dbo].[PosiblesResultados] r ON r.id = s.resultado
		LEFT JOIN [dbo].[Predicciones] p ON s.id = p.situacionId
		LEFT JOIN [dbo].[PosiblesResultados] pt ON p.prediccion = pt.id
END
