CREATE TABLE [dbo].[Players] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50)  NULL,
    [LastName]   NVARCHAR (50)  NULL,
    [FullName]   NVARCHAR (100) NULL,
    [TeamId]     INT            NOT NULL,
    [PositionId] INT            NULL,
    CONSTRAINT [PK_dbo.Players] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO


