CREATE TABLE [dbo].[Players]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CurrentTeamId] INT NOT NULL,
	[Name] NVARCHAR(20) NOT NULL,
	[NickName] NVARCHAR(10) NULL,
	[Number] NVARCHAR(4) NULL,
	[OnLoan] BIT,
	CONSTRAINT [FK_Players_Teams] FOREIGN KEY ([CurrentTeamId]) REFERENCES [dbo].Teams([Id])
)
