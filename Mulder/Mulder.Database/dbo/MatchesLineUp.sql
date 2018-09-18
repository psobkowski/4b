CREATE TABLE [dbo].[MatchesLineUp]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MatchId] INT NOT NULL,
	[TeamId] INT NOT NULL,
	[PlayerId] INT NOT NULL,
	[YellowCard] BIT NOT NULL DEFAULT 0,
	[RedCard] BIT NOT NULL DEFAULT 0,
	[ManOfTheMatch] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [FK_MatchesLineUp_Matches] FOREIGN KEY ([MatchId]) REFERENCES [dbo].Matches([Id]),
	CONSTRAINT [FK_MatchesLineUp_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].Teams([Id]),
	CONSTRAINT [FK_MatchesLineUp_Players] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].Players([Id]),
	CONSTRAINT [UQ_Matches_Teams_Players] UNIQUE([MatchId], [TeamId], [PlayerId])
)
