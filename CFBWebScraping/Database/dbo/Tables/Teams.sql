CREATE TABLE [dbo].[Teams] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (100) NULL,
    [Mascot] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([Id] ASC)
);

