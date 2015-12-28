CREATE TABLE [dbo].[ReceivingPlayerGameStats] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [PlayerId]            INT NOT NULL,
    [Receptions]          INT NOT NULL,
    [ReceivingYards]      INT NOT NULL,
    [ReceivingTouchdowns] INT NOT NULL,
    [GameId]              INT DEFAULT ((0)) NOT NULL,
    [NotAvailable]        BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.ReceivingPlayerGameStats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ReceivingPlayerGameStats_dbo.Games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Games] ([Id]) ON DELETE CASCADE
);




GO


