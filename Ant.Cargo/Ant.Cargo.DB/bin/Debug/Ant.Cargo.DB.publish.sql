﻿/*
Deployment script for Ant.Cargo

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Ant.Cargo"
:setvar DefaultFilePrefix "Ant.Cargo"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key 03bfd7fb-6a71-43e3-b730-6986ee50f790 is skipped, element [dbo].[District].[Id] (SqlSimpleColumn) will not be renamed to ID';


GO
PRINT N'Rename refactoring operation with key 4c07a45f-588b-462c-8f94-42508aef3f31 is skipped, element [dbo].[Vehicle].[Id] (SqlSimpleColumn) will not be renamed to ID';


GO
PRINT N'Creating [dbo].[District]...';


GO
CREATE TABLE [dbo].[District] (
    [ID]   INT            NOT NULL,
    [Name] NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Vehicle]...';


GO
CREATE TABLE [dbo].[Vehicle] (
    [ID]                 INT            NOT NULL,
    [Model]              NVARCHAR (250) NOT NULL,
    [RegistrationNumber] NVARCHAR (250) NOT NULL,
    [PhoneNumber]        NVARCHAR (250) NOT NULL,
    [DistrictID]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Vehicle_District]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [FK_Vehicle_District] FOREIGN KEY ([DistrictID]) REFERENCES [dbo].[District] ([ID]);


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '03bfd7fb-6a71-43e3-b730-6986ee50f790')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('03bfd7fb-6a71-43e3-b730-6986ee50f790')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '4c07a45f-588b-462c-8f94-42508aef3f31')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('4c07a45f-588b-462c-8f94-42508aef3f31')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [FK_Vehicle_District];


GO
PRINT N'Update complete.';


GO
