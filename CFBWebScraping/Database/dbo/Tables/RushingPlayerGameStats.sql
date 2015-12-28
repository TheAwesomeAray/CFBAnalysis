CREATE TABLE [dbo].[RushingPlayerGameStats] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [RushingYards]      INT NOT NULL,
    [Rushes]            INT NOT NULL,
    [FumblesLost]       INT NOT NULL,
    [RushingTouchdowns] INT NOT NULL,
    [PlayerId]          INT NOT NULL,
    [GameId]            INT CONSTRAINT [DF__RushingPl__GameI__5CD6CB2B] DEFAULT ((0)) NOT NULL,
    [NotAvailable]      BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.RushingPlayerGameStats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.RushingPlayerGameStats_dbo.Games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Games] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.RushingPlayerGameStats_dbo.Players_Player_Id] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Players] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Player_Id]
    ON [dbo].[RushingPlayerGameStats]([PlayerId] ASC);



