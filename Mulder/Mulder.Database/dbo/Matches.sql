﻿CREATE TABLE [dbo].[Matches]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Location] NVARCHAR(20) NOT NULL,
	[Address] NVARCHAR(255) NOT NULL,
	[Date] DateTime,
	[Year] SMALLINT,
)
