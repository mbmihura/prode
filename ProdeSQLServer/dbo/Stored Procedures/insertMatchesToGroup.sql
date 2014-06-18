-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE insertMatchesToGroup
	-- Add the parameters for the stored procedure here
	@TeamV nvarchar(50), 
	@TeamL nvarchar(50),
	@Group nchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @teamLid int
	SELECT TOP 1 @teamLid = id FROM PosiblesResultados WHERE [resultado] = @TeamL
	IF (@teamLid IS NULL)
	BEGIN
		INSERT INTO [dbo].[PosiblesResultados] ([resultado]) VALUES (@TeamL)
		SELECT TOP 1 @teamLid = id FROM PosiblesResultados WHERE [resultado] = @TeamL
	END

	DECLARE @teamVid int
	SELECT TOP 1 @teamVid = id FROM PosiblesResultados WHERE [resultado] = @TeamV
	IF (@teamVid IS NULL)
	BEGIN
		INSERT INTO [dbo].[PosiblesResultados] ([resultado]) VALUES (@TeamV)
		SELECT TOP 1 @teamVid = id FROM PosiblesResultados WHERE [resultado] = @TeamV
	END

	DECLARE @desc nvarchar(50)
	SET @desc = @teamL + '/' + @teamV
	INSERT INTO [dbo].[Situaciones] ([descripcion],[teamL],[teamV],[groupLetter])
    VALUES (@desc, @teamLid, @teamVid, @Group)
END
