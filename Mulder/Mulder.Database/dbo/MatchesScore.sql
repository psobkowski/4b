CREATE TABLE [dbo].[MatchesScore]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MatchId] INT NOT NULL,
	[TeamId] INT NOT NULL,
	[HalfTimeScore] SMALLINT NOT NULL,
	[FullTimeScore] SMALLINT NOT NULL,
	CONSTRAINT [FK_MatchesScore_Matches] FOREIGN KEY ([MatchId]) REFERENCES [dbo].Matches([Id]),
	CONSTRAINT [FK_MatchesScore_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].Teams([Id]),
)
