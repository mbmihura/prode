-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertPredictionsForGroups
	-- Add the parameters for the stored procedure here
	@TeamV nvarchar(50),
	@TeamL nvarchar(50),
	@UserId int,
	@Prediccion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @desc nvarchar(50)
	DECLARE @SitId int
	SET @desc = @teamL + '/' + @teamV
	SELECT TOP 1 @SitId = id FROM [dbo].[Situaciones] WHERE [descripcion] = @desc

	IF (@SitId IS NOT NULL)
	BEGIN
		INSERT INTO [dbo].[Predicciones] ([situacionId],[userId],[prediccion]) 
		VALUES (@SitId, @UserId, @Prediccion)
	END
END
