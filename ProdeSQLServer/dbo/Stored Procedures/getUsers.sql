-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].getUsers
AS
	SELECT [username] AS 'username'
		,[email] AS 'email'
		,[id] AS 'id'
	FROM [dbo].[Usuarios]