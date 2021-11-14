
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/13/2021 23:37:53
-- Generated from EDMX file: D:\Documents\Labs_prog\5th sem\db\lab7\dbBrowser\dbBrowser\Data\Model\UniversityDataBase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbBrowserDataBase-GV];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StudentParentFamilyRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FamilyRelations] DROP CONSTRAINT [FK_StudentParentFamilyRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentPrivilege]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Privileges] DROP CONSTRAINT [FK_StudentPrivilege];
GO
IF OBJECT_ID(N'[dbo].[FK_StudyGroupStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_StudyGroupStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentFamilyRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FamilyRelations] DROP CONSTRAINT [FK_StudentFamilyRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_FacultyStudyGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudyGroups] DROP CONSTRAINT [FK_FacultyStudyGroup];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StudentParents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentParents];
GO
IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[StudyGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudyGroups];
GO
IF OBJECT_ID(N'[dbo].[Privileges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Privileges];
GO
IF OBJECT_ID(N'[dbo].[Faculties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Faculties];
GO
IF OBJECT_ID(N'[dbo].[FamilyRelations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FamilyRelations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StudentParents'
CREATE TABLE [dbo].[StudentParents] (
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NULL,
    [Birthday] datetime  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Gender] tinyint  NOT NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NULL,
    [Birthday] datetime  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [StudyGroup_Id] int  NOT NULL
);
GO

-- Creating table 'StudyGroups'
CREATE TABLE [dbo].[StudyGroups] (
    [StudentsAmount] int  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Faculty_Id] int  NOT NULL
);
GO

-- Creating table 'Privileges'
CREATE TABLE [dbo].[Privileges] (
    [ReleaseDate] datetime  NOT NULL,
    [DocumenPath] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Student_Id] int  NOT NULL
);
GO

-- Creating table 'Faculties'
CREATE TABLE [dbo].[Faculties] (
    [Dean] nvarchar(max)  NOT NULL,
    [Auditorium] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'FamilyRelations'
CREATE TABLE [dbo].[FamilyRelations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParentType] tinyint  NOT NULL,
    [StudentParent_Id] int  NOT NULL,
    [Student_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StudentParents'
ALTER TABLE [dbo].[StudentParents]
ADD CONSTRAINT [PK_StudentParents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StudyGroups'
ALTER TABLE [dbo].[StudyGroups]
ADD CONSTRAINT [PK_StudyGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Privileges'
ALTER TABLE [dbo].[Privileges]
ADD CONSTRAINT [PK_Privileges]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Faculties'
ALTER TABLE [dbo].[Faculties]
ADD CONSTRAINT [PK_Faculties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FamilyRelations'
ALTER TABLE [dbo].[FamilyRelations]
ADD CONSTRAINT [PK_FamilyRelations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudentParent_Id] in table 'FamilyRelations'
ALTER TABLE [dbo].[FamilyRelations]
ADD CONSTRAINT [FK_StudentParentFamilyRelation]
    FOREIGN KEY ([StudentParent_Id])
    REFERENCES [dbo].[StudentParents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentParentFamilyRelation'
CREATE INDEX [IX_FK_StudentParentFamilyRelation]
ON [dbo].[FamilyRelations]
    ([StudentParent_Id]);
GO

-- Creating foreign key on [Student_Id] in table 'Privileges'
ALTER TABLE [dbo].[Privileges]
ADD CONSTRAINT [FK_StudentPrivilege]
    FOREIGN KEY ([Student_Id])
    REFERENCES [dbo].[Students]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentPrivilege'
CREATE INDEX [IX_FK_StudentPrivilege]
ON [dbo].[Privileges]
    ([Student_Id]);
GO

-- Creating foreign key on [StudyGroup_Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_StudyGroupStudent]
    FOREIGN KEY ([StudyGroup_Id])
    REFERENCES [dbo].[StudyGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudyGroupStudent'
CREATE INDEX [IX_FK_StudyGroupStudent]
ON [dbo].[Students]
    ([StudyGroup_Id]);
GO

-- Creating foreign key on [Student_Id] in table 'FamilyRelations'
ALTER TABLE [dbo].[FamilyRelations]
ADD CONSTRAINT [FK_StudentFamilyRelation]
    FOREIGN KEY ([Student_Id])
    REFERENCES [dbo].[Students]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentFamilyRelation'
CREATE INDEX [IX_FK_StudentFamilyRelation]
ON [dbo].[FamilyRelations]
    ([Student_Id]);
GO

-- Creating foreign key on [Faculty_Id] in table 'StudyGroups'
ALTER TABLE [dbo].[StudyGroups]
ADD CONSTRAINT [FK_FacultyStudyGroup]
    FOREIGN KEY ([Faculty_Id])
    REFERENCES [dbo].[Faculties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FacultyStudyGroup'
CREATE INDEX [IX_FK_FacultyStudyGroup]
ON [dbo].[StudyGroups]
    ([Faculty_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------