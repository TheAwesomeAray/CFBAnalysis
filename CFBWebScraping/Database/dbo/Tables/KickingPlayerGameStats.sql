CREATE TABLE [dbo].[KickingPlayerGameStats] (
    [id]                 INT IDENTITY (1, 1) NOT NULL,
    [GameId]             INT NOT NULL,
    [PlayerId]           INT NOT NULL,
    [ExtraPointsMade]    INT NOT NULL,
    [ExtraPointAttempts] INT NOT NULL,
    [FieldGoalsMade]     INT NOT NULL,
    [FieldGoalAttemtps]  INT NOT NULL,
    [Punts]              INT NOT NULL,
    [PuntYards]          INT NOT NULL,
    [NotAvailable]       BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.KickingPlayerGameStats] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_dbo.KickingPlayerGameStats_dbo.Games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Games] ([Id]) ON DELETE CASCADE
);




GO


