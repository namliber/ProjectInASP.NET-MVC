CREATE TABLE [dbo].[Medicine] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [CommercialName]     NVARCHAR (MAX) NULL,
    [GenericName]        NVARCHAR (MAX) NULL,
    [Producer]           NVARCHAR (MAX) NULL,
    [ActiveIngredients]  NVARCHAR (MAX) NULL,
    [DoseCharacteristic] NVARCHAR (MAX) NULL,
    [ImageUrl]           NVARCHAR (MAX) NULL,
	[NDC]                NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Medicine] PRIMARY KEY CLUSTERED ([Id] ASC),
);



