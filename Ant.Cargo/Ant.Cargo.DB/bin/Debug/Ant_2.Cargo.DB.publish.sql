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
USE [$(DatabaseName)];


GO
PRINT N'Dropping [dbo].[FK_Vehicle_District]...';


GO
ALTER TABLE [dbo].[Vehicle] DROP CONSTRAINT [FK_Vehicle_District];


GO
PRINT N'Starting rebuilding table [dbo].[District]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_District] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[District])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_District] ON;
        INSERT INTO [dbo].[tmp_ms_xx_District] ([ID], [Name])
        SELECT   [ID],
                 [Name]
        FROM     [dbo].[District]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_District] OFF;
    END

DROP TABLE [dbo].[District];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_District]', N'District';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[User]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_User] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Email]    NVARCHAR (250) NOT NULL,
    [Password] NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[User])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_User] ON;
        INSERT INTO [dbo].[tmp_ms_xx_User] ([ID], [Email], [Password])
        SELECT   [ID],
                 [Email],
                 [Password]
        FROM     [dbo].[User]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_User] OFF;
    END

DROP TABLE [dbo].[User];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_User]', N'User';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Vehicle]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Vehicle] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Model]              NVARCHAR (250) NOT NULL,
    [RegistrationNumber] NVARCHAR (250) NOT NULL,
    [PhoneNumber]        NVARCHAR (250) NOT NULL,
    [DistrictID]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Vehicle])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Vehicle] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Vehicle] ([ID], [Model], [RegistrationNumber], [PhoneNumber], [DistrictID])
        SELECT   [ID],
                 [Model],
                 [RegistrationNumber],
                 [PhoneNumber],
                 [DistrictID]
        FROM     [dbo].[Vehicle]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Vehicle] OFF;
    END

DROP TABLE [dbo].[Vehicle];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Vehicle]', N'Vehicle';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_Vehicle_District]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [FK_Vehicle_District] FOREIGN KEY ([DistrictID]) REFERENCES [dbo].[District] ([ID]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [FK_Vehicle_District];


GO
PRINT N'Update complete.';


GO
