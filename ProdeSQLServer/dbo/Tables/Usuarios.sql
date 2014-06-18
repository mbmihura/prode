CREATE TABLE [dbo].[Usuarios] (
    [id]       INT           NOT NULL,
    [username] NVARCHAR (50) NOT NULL,
    [email]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([id] ASC)
);

