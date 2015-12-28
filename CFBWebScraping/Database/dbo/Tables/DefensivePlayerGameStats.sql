CREATE TABLE [dbo].[DefensivePlayerGameStats] (
    [Id]                     INT IDENTITY (1, 1) NOT NULL,
    [GameId]                 INT NOT NULL,
    [PlayerId]               INT NOT NULL,
    [SoloTackles]            INT NOT NULL,
    [AssistTackles]          INT NOT NULL,
    [TacklesForLoss]         INT NOT NULL,
    [Interceptions]          INT NOT NULL,
    [InterceptionYards]      INT NOT NULL,
    [InterceptionTouchdowns] INT NOT NULL,
    [PassDefended]           INT NOT NULL,
    [FumbleForced]           INT NOT NULL,
    [FumbleRecovery]         INT NOT NULL,
    [FumbleYards]            INT NOT NULL,
    [FumbleTouchdown]        INT NOT NULL,
    [Sacks]                  INT DEFAULT ((0)) NOT NULL,
    [NotAvailable]           BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.DefensivePlayerGameStats] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO


