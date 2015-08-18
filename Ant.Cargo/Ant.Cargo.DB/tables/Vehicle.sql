CREATE TABLE [dbo].[Vehicle]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Model] NVARCHAR(250) NULL, 
    [RegistrationNumber] NVARCHAR(250) NULL, 
    [PhoneNumber] NVARCHAR(250) NOT NULL, 
    [DistrictID] INT NOT NULL, 
    CONSTRAINT [FK_Vehicle_District] FOREIGN KEY ([DistrictID]) REFERENCES [District]([ID])
)
