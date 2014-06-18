-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE getUserDataForUserId
	@UserId int
AS
	SELECT [username] AS 'username'
		,[email] AS 'email'
		,[id] AS 'id'
	FROM [dbo].[Usuarios]
	WHERE [id] = @UserId
