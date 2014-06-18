-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertPredictionsForBrackets 
	-- Add the parameters for the stored procedure here
	@Desc nvarchar(50),
	@Prediccion int,
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @SitId int
	SELECT TOP 1 @SitId = id FROM [dbo].[Situaciones] WHERE [descripcion] = @Desc

	IF (@SitId IS NOT NULL)
	BEGIN
		INSERT INTO [dbo].[Predicciones] ([situacionId],[userId],[prediccion]) 
		VALUES (@SitId, @UserId, @Prediccion)
	END
END
