CREATE TABLE [dbo].[KickReturnPlayerGameStats] (
    [Id]                   INT IDENTITY (1, 1) NOT NULL,
    [PlayerId]             INT NOT NULL,
    [GameId]               INT NOT NULL,
    [KickReturns]          INT NOT NULL,
    [KickReturnYards]      INT NOT NULL,
    [KickReturnTouchdowns] INT NOT NULL,
    [PuntReturns]          INT NOT NULL,
    [PuntReturnYards]      INT NOT NULL,
    [PuntReturnTouchdowns] INT NOT NULL,
    [NotAvailable]         BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.KickReturnPlayerGameStats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.KickReturnPlayerGameStats_dbo.Games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Games] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_GameId]
    ON [dbo].[KickReturnPlayerGameStats]([GameId] ASC);


GO


