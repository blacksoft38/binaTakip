
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/17/2016 00:43:14
-- Generated from EDMX file: C:\Users\hp\Documents\Visual Studio 2015\Projects\binaTakip\binaTakip\BinaTakipDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbB703CB358A];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[dairelerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dairelerSet];
GO
IF OBJECT_ID(N'[dbo].[kisilerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[kisilerSet];
GO
IF OBJECT_ID(N'[dbo].[OdemelerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OdemelerSet];
GO
IF OBJECT_ID(N'[dbo].[OgSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OgSet];
GO
IF OBJECT_ID(N'[dbo].[BinalarSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BinalarSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'dairelerSet'
CREATE TABLE [dbo].[dairelerSet] (
    [daireId] int IDENTITY(1,1) NOT NULL,
    [daireno] int  NOT NULL,
    [kisi_id] int  NOT NULL,
    [ev_sahibi_id] int  NOT NULL,
    [binaId] int  NOT NULL
);
GO

-- Creating table 'kisilerSet'
CREATE TABLE [dbo].[kisilerSet] (
    [kisiId] int IDENTITY(1,1) NOT NULL,
    [isim] nvarchar(max)  NOT NULL,
    [telefon] nvarchar(max)  NOT NULL,
    [bina_id] int  NOT NULL
);
GO

-- Creating table 'OdemelerSet'
CREATE TABLE [dbo].[OdemelerSet] (
    [OdemeID] int IDENTITY(1,1) NOT NULL,
    [daireId] int  NOT NULL,
    [tutar] float  NOT NULL,
    [tarih] datetime  NOT NULL,
    [Ogid] int  NOT NULL
);
GO

-- Creating table 'OgSet'
CREATE TABLE [dbo].[OgSet] (
    [OgId] int IDENTITY(1,1) NOT NULL,
    [Tutar] float  NOT NULL,
    [Tur] nvarchar(max)  NOT NULL,
    [isim] nvarchar(max)  NOT NULL,
    [taksitli] bit  NOT NULL,
    [taksitlitutar] float  NULL,
    [taksitsayisi] int  NULL
);
GO

-- Creating table 'BinalarSet'
CREATE TABLE [dbo].[BinalarSet] (
    [BinaId] int IDENTITY(1,1) NOT NULL,
    [binaAdi] nvarchar(max)  NOT NULL,
    [daireSayisi] int  NOT NULL,
    [userId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [userId] int IDENTITY(1,1) NOT NULL,
    [kullanici_adi] nvarchar(max)  NOT NULL,
    [sifre] nvarchar(max)  NOT NULL,
    [onay] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [daireId] in table 'dairelerSet'
ALTER TABLE [dbo].[dairelerSet]
ADD CONSTRAINT [PK_dairelerSet]
    PRIMARY KEY CLUSTERED ([daireId] ASC);
GO

-- Creating primary key on [kisiId] in table 'kisilerSet'
ALTER TABLE [dbo].[kisilerSet]
ADD CONSTRAINT [PK_kisilerSet]
    PRIMARY KEY CLUSTERED ([kisiId] ASC);
GO

-- Creating primary key on [OdemeID] in table 'OdemelerSet'
ALTER TABLE [dbo].[OdemelerSet]
ADD CONSTRAINT [PK_OdemelerSet]
    PRIMARY KEY CLUSTERED ([OdemeID] ASC);
GO

-- Creating primary key on [OgId] in table 'OgSet'
ALTER TABLE [dbo].[OgSet]
ADD CONSTRAINT [PK_OgSet]
    PRIMARY KEY CLUSTERED ([OgId] ASC);
GO

-- Creating primary key on [BinaId] in table 'BinalarSet'
ALTER TABLE [dbo].[BinalarSet]
ADD CONSTRAINT [PK_BinalarSet]
    PRIMARY KEY CLUSTERED ([BinaId] ASC);
GO

-- Creating primary key on [userId] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------