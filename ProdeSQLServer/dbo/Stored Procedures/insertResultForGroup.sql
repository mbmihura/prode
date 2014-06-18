-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insertResultForGroup]
	@Sit nvarchar(50),
	@Result nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @SitId int
	DECLARE @ResultId int

	SELECT @SitId = [id] FROM [dbo].[Situaciones] WHERE [descripcion] = @Sit
	SELECT @ResultId=[id] FROM [dbo].[PosiblesResultados] WHERE [resultado] = @Result

	IF (@SitId IS NOT NULL AND @ResultId IS NOT NULL )
	BEGIN
	UPDATE [dbo].[Situaciones]
	   SET [resultado] = @ResultId
	 WHERE [id] = @SitId

	UPDATE [dbo].[Predicciones]
	   SET [puntosGanados] = (
			SELECT 
				CASE
					 WHEN @ResultId IS NULL THEN NULL
					 WHEN @ResultId = [prediccion] THEN 2 
					 ELSE 0 
				END
			[resultado] FROM [dbo].[Situaciones] s WHERE s.id = @SitId)

	 WHERE [situacionId] = @SitId
	 END ELSE BEGIN
		RAISERROR('Match or/and Result not found.', 16, 1) --change to > 10
		RETURN --exit now
	 END
END
