CREATE TABLE [dbo].[PassingPlayerGameStats] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [PlayerId]          INT NOT NULL,
    [PassingYards]      INT NOT NULL,
    [PassCompletions]   INT NOT NULL,
    [PassAttempts]      INT NOT NULL,
    [Interceptions]     INT NOT NULL,
    [PassingTouchdowns] INT NOT NULL,
    [GameId]            INT DEFAULT ((0)) NOT NULL,
    [NotAvailable]      BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.PassingPlayerGameStats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PassingPlayerGameStats_dbo.Games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Games] ([Id]) ON DELETE CASCADE
);




GO


