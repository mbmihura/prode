/****** Script para el comando SelectTopNRows de SSMS  ******/
SELECT rp.resultado,rp.id, u.username, p.puntosGanados, *
  FROM [dbc59bcc6197b14029ae0da34a00123d33].[dbo].[Situaciones] s
  JOIN [dbo].[Predicciones] p ON s.id = p.situacionId
  join [dbo].Usuarios u ON u.id = p.userId
  join [dbo].[PosiblesResultados] rp ON rp.[id] = p.[prediccion]
  WHERE descripcion = 'D1' OR descripcion = 'D2'