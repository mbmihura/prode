-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAllPosibleResults]
AS
	SELECT resultado as 'results', id as 'id' FROM [dbo].[PosiblesResultados]
