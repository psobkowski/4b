CREATE TABLE [dbo].[MatchesSpectators]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MatchId] INT NOT NULL,
	[SpectatorId] INT NOT NULL,
    CONSTRAINT [FK_MatchesSpectators_Matches] FOREIGN KEY ([MatchId]) REFERENCES [dbo].Matches([Id]),
	CONSTRAINT [FK_MatchesSpectators_Spectators] FOREIGN KEY ([SpectatorId]) REFERENCES [dbo].Spectators([Id]),
	CONSTRAINT [UQ_Matches_Spectators] UNIQUE([MatchId], [SpectatorId])
)
