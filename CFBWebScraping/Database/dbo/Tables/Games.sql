CREATE TABLE [dbo].[Games] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NULL,
    [GameDate]      DATETIME       NOT NULL,
    [Season]        INT            NOT NULL,
    [HomeTeamId]    INT            NULL,
    [HomeTeamScore] INT            NOT NULL,
    [AwayTeamScore] INT            NOT NULL,
    [WinnerId]      INT            NOT NULL,
    [AwayTeamId]    INT            NULL,
    CONSTRAINT [PK_dbo.Games] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Games_dbo.Teams_Team_Id] FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Teams] ([Id])
);




GO



GO


