CREATE TABLE [dbo].[TeamGameStats] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [GameId]            INT NOT NULL,
    [TeamId]            INT NOT NULL,
    [NotAvailable]      BIT NOT NULL,
    [TotalYards]        INT NOT NULL,
    [TotalPlays]        INT NOT NULL,
    [TotalFirstDowns]   INT NOT NULL,
    [PenaltyFirstDowns] INT NOT NULL,
    [Penalties]         INT NOT NULL,
    [PenaltyYards]      INT NOT NULL,
    [TurnOvers]         INT NOT NULL,
    [PassingYards]      INT NOT NULL,
    [PassCompletions]   INT NOT NULL,
    [PassAttempts]      INT NOT NULL,
    [PassingFirstDowns] INT NOT NULL,
    [Interceptions]     INT NOT NULL,
    [RushingYards]      INT NOT NULL,
    [Rushes]            INT NOT NULL,
    [RushingFirstDowns] INT NOT NULL,
    [FumblesLost]       INT NOT NULL,
    CONSTRAINT [PK_dbo.TeamGameStats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TeamGameStats_dbo.Games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Games] ([Id]) ON DELETE CASCADE
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_GameId]
    ON [dbo].[TeamGameStats]([GameId] ASC);

