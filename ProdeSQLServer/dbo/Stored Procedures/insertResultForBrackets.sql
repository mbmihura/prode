-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insertResultForBrackets]
	@desc nvarchar(50),
	@Result nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @SitId int
	DECLARE @SitLetter int
	DECLARE @ResultId int

	SELECT @SitId = [id], @SitLetter = [groupLetter] FROM [dbo].[Situaciones] WHERE [descripcion] = @desc
	SELECT @ResultId=[id] FROM [dbo].[PosiblesResultados] WHERE [resultado] = @Result

	IF (@SitId IS NOT NULL AND @SitLetter IN ('8','4','2','1') AND  @ResultId IS NOT NULL )
	BEGIN
		-- guardar resultado en la situacion
		UPDATE [dbo].[Situaciones]
		   SET [resultado] = @ResultId
		 WHERE [id] = @SitId

		-- calcular esquemas de puntos
		DECLARE @pExacto int
		DECLARE @pCerca int

		IF (@SitLetter = 8)
			BEGIN
				SET @pExacto = 4
				SET @pCerca = 2
			END
		ELSE IF (@SitLetter = 4)
			BEGIN
				SET @pExacto = 6
				SET @pCerca = 3
			END
		ELSE IF (@SitLetter = 2)
			BEGIN
				SET @pExacto = 7
				SET @pCerca = 4
			END
		ELSE IF (@SitLetter = 1)
			BEGIN
				SET @pExacto = 8
				SET @pCerca = 8
			END

		UPDATE [dbo].[Predicciones]
		   SET [puntosGanados] = (
				SELECT 
					CASE
						 WHEN @ResultId IS NULL THEN NULL
						 WHEN @ResultId = [prediccion] THEN @pExacto  -- le pega exacto
						 WHEN [prediccion] IN (
							SELECT id 
							FROM [dbo].[Situaciones]
							WHERE [groupLetter] = @SitLetter
						) THEN @pCerca -- aparece en otro lado
						 ELSE 0 
					END
				[resultado] FROM [dbo].[Situaciones] s WHERE s.id = @SitId)

		 WHERE [situacionId] = @SitId
	 END ELSE BEGIN
		RAISERROR('Bracket or/and Result not found.', 16, 1) --change to > 10
		RETURN --exit now
	 END
END