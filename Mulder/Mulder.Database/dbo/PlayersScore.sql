CREATE TABLE [dbo].[PlayersScore]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MatchesLineUpId] INT NOT NULL,
	[Minute] SMALLINT NULL,
	CONSTRAINT [FK_PlayersScore_MatchesLineUp] FOREIGN KEY ([MatchesLineUpId]) REFERENCES [dbo].[MatchesLineUp]([Id])
)
