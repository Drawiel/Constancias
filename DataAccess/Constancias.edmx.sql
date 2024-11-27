
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/27/2024 06:49:42
-- Generated from EDMX file: C:\Users\marla\Constancias\DataAccess\Constancias.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Constancias];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Academico__IdAca__5070F446]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicoExperienciaEducativa] DROP CONSTRAINT [FK__Academico__IdAca__5070F446];
GO
IF OBJECT_ID(N'[dbo].[FK__Academico__IdExp__5165187F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicoExperienciaEducativa] DROP CONSTRAINT [FK__Academico__IdExp__5165187F];
GO
IF OBJECT_ID(N'[dbo].[FK__Academico__IdPro__398D8EEE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Academico] DROP CONSTRAINT [FK__Academico__IdPro__398D8EEE];
GO
IF OBJECT_ID(N'[dbo].[FK__Constanci__IdAca__3C69FB99]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Constancia] DROP CONSTRAINT [FK__Constanci__IdAca__3C69FB99];
GO
IF OBJECT_ID(N'[dbo].[FK__Experienc__IdPro__47DBAE45]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExperienciaEducativa] DROP CONSTRAINT [FK__Experienc__IdPro__47DBAE45];
GO
IF OBJECT_ID(N'[dbo].[FK__Participa__IdAca__4BAC3F29]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participacion] DROP CONSTRAINT [FK__Participa__IdAca__4BAC3F29];
GO
IF OBJECT_ID(N'[dbo].[FK__Participa__IdPro__4AB81AF0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participacion] DROP CONSTRAINT [FK__Participa__IdPro__4AB81AF0];
GO
IF OBJECT_ID(N'[dbo].[FK__ProductoA__IdAca__4222D4EF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoAcademico] DROP CONSTRAINT [FK__ProductoA__IdAca__4222D4EF];
GO
IF OBJECT_ID(N'[dbo].[FK__ProgramaE__IdExp__5535A963]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramaEducativoExperienciaEducativa] DROP CONSTRAINT [FK__ProgramaE__IdExp__5535A963];
GO
IF OBJECT_ID(N'[dbo].[FK__ProgramaE__IdPro__5441852A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramaEducativoExperienciaEducativa] DROP CONSTRAINT [FK__ProgramaE__IdPro__5441852A];
GO
IF OBJECT_ID(N'[dbo].[FK__ProyectoC__IdAca__3F466844]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProyectoCampo] DROP CONSTRAINT [FK__ProyectoC__IdAca__3F466844];
GO
IF OBJECT_ID(N'[dbo].[FK__TrabajoRe__IdAca__44FF419A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrabajoRecepcional] DROP CONSTRAINT [FK__TrabajoRe__IdAca__44FF419A];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Academico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Academico];
GO
IF OBJECT_ID(N'[dbo].[AcademicoExperienciaEducativa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AcademicoExperienciaEducativa];
GO
IF OBJECT_ID(N'[dbo].[Administrativa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Administrativa];
GO
IF OBJECT_ID(N'[dbo].[Constancia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Constancia];
GO
IF OBJECT_ID(N'[dbo].[ExperienciaEducativa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExperienciaEducativa];
GO
IF OBJECT_ID(N'[dbo].[Participacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Participacion];
GO
IF OBJECT_ID(N'[dbo].[ProductoAcademico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductoAcademico];
GO
IF OBJECT_ID(N'[dbo].[ProgramaEducativo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramaEducativo];
GO
IF OBJECT_ID(N'[dbo].[ProgramaEducativoExperienciaEducativa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramaEducativoExperienciaEducativa];
GO
IF OBJECT_ID(N'[dbo].[ProyectoCampo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProyectoCampo];
GO
IF OBJECT_ID(N'[dbo].[TrabajoRecepcional]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrabajoRecepcional];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Academico'
CREATE TABLE [dbo].[Academico] (
    [IdAcademico] int  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [NumeroPersonal] nvarchar(20)  NOT NULL,
    [FechaContratacion] nvarchar(50)  NULL,
    [TipoContratacion] nvarchar(50)  NULL,
    [IdPrograma] int  NOT NULL,
    [AreaAcademica] nvarchar(max)  NOT NULL,
    [Contrasena] nvarchar(256)
);
GO

-- Creating table 'Administrativa'
CREATE TABLE [dbo].[Administrativa] (
    [IdAdministrativo] int  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [ProgramaEducativo] nvarchar(100)  NULL,
    [usuario] nvarchar(max)  NOT NULL,
    [contrasena] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Constancia'
CREATE TABLE [dbo].[Constancia] (
    [IdConstancia] int  NOT NULL,
    [Tipo] nvarchar(50)  NULL,
    [FechaExpedicion] nvarchar(50)  NULL,
    [Solicitante] nvarchar(100)  NULL,
    [IdAcademico] int  NULL
);
GO

-- Creating table 'ExperienciaEducativa'
CREATE TABLE [dbo].[ExperienciaEducativa] (
    [IdExperienciaEducativa] int  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [ProgramaEducativo] nvarchar(100)  NULL,
    [IdPrograma] int  NULL
);
GO

-- Creating table 'Participacion'
CREATE TABLE [dbo].[Participacion] (
    [IdParticipacion] int  NOT NULL,
    [PeriodoParticipacion] nvarchar(50)  NULL,
    [TipoParticipacion] nvarchar(50)  NULL,
    [IdPrograma] int  NULL,
    [IdAcademico] int  NULL
);
GO

-- Creating table 'ProductoAcademico'
CREATE TABLE [dbo].[ProductoAcademico] (
    [IdProducto] int  NOT NULL,
    [Titulo] nvarchar(200)  NOT NULL,
    [Tipo] nvarchar(50)  NULL,
    [TipoPublicacion] nvarchar(50)  NULL,
    [FechaPublicacion] nvarchar(50)  NULL,
    [IdAcademico] int  NULL
);
GO

-- Creating table 'ProgramaEducativo'
CREATE TABLE [dbo].[ProgramaEducativo] (
    [IdPrograma] int  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [Año] nvarchar(4)  NULL,
    [AreaAcademica] nvarchar(100)  NULL
);
GO

-- Creating table 'ProyectoCampo'
CREATE TABLE [dbo].[ProyectoCampo] (
    [IdProyectoCampo] int  NOT NULL,
    [NombreProyecto] nvarchar(100)  NOT NULL,
    [LugarRealizacion] nvarchar(100)  NULL,
    [Periodo] nvarchar(50)  NULL,
    [RolAcademico] nvarchar(50)  NULL,
    [IdAcademico] int  NULL
);
GO

-- Creating table 'TrabajoRecepcional'
CREATE TABLE [dbo].[TrabajoRecepcional] (
    [IdTrabajoRecepcional] int  NOT NULL,
    [Titulo] nvarchar(200)  NOT NULL,
    [NombreEstudiante] nvarchar(100)  NULL,
    [FechaPresentacion] nvarchar(50)  NULL,
    [TipoTrabajo] nvarchar(50)  NULL,
    [IdAcademico] int  NOT NULL,
    [RolAcademico] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AcademicoExperienciaEducativa'
CREATE TABLE [dbo].[AcademicoExperienciaEducativa] (
    [Academico_IdAcademico] int  NOT NULL,
    [ExperienciaEducativa_IdExperienciaEducativa] int  NOT NULL
);
GO

-- Creating table 'ProgramaEducativoExperienciaEducativa'
CREATE TABLE [dbo].[ProgramaEducativoExperienciaEducativa] (
    [ExperienciaEducativa1_IdExperienciaEducativa] int  NOT NULL,
    [ProgramaEducativo2_IdPrograma] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdAcademico] in table 'Academico'
ALTER TABLE [dbo].[Academico]
ADD CONSTRAINT [PK_Academico]
    PRIMARY KEY CLUSTERED ([IdAcademico] ASC);
GO

-- Creating primary key on [IdAdministrativo] in table 'Administrativa'
ALTER TABLE [dbo].[Administrativa]
ADD CONSTRAINT [PK_Administrativa]
    PRIMARY KEY CLUSTERED ([IdAdministrativo] ASC);
GO

-- Creating primary key on [IdConstancia] in table 'Constancia'
ALTER TABLE [dbo].[Constancia]
ADD CONSTRAINT [PK_Constancia]
    PRIMARY KEY CLUSTERED ([IdConstancia] ASC);
GO

-- Creating primary key on [IdExperienciaEducativa] in table 'ExperienciaEducativa'
ALTER TABLE [dbo].[ExperienciaEducativa]
ADD CONSTRAINT [PK_ExperienciaEducativa]
    PRIMARY KEY CLUSTERED ([IdExperienciaEducativa] ASC);
GO

-- Creating primary key on [IdParticipacion] in table 'Participacion'
ALTER TABLE [dbo].[Participacion]
ADD CONSTRAINT [PK_Participacion]
    PRIMARY KEY CLUSTERED ([IdParticipacion] ASC);
GO

-- Creating primary key on [IdProducto] in table 'ProductoAcademico'
ALTER TABLE [dbo].[ProductoAcademico]
ADD CONSTRAINT [PK_ProductoAcademico]
    PRIMARY KEY CLUSTERED ([IdProducto] ASC);
GO

-- Creating primary key on [IdPrograma] in table 'ProgramaEducativo'
ALTER TABLE [dbo].[ProgramaEducativo]
ADD CONSTRAINT [PK_ProgramaEducativo]
    PRIMARY KEY CLUSTERED ([IdPrograma] ASC);
GO

-- Creating primary key on [IdProyectoCampo] in table 'ProyectoCampo'
ALTER TABLE [dbo].[ProyectoCampo]
ADD CONSTRAINT [PK_ProyectoCampo]
    PRIMARY KEY CLUSTERED ([IdProyectoCampo] ASC);
GO

-- Creating primary key on [IdTrabajoRecepcional] in table 'TrabajoRecepcional'
ALTER TABLE [dbo].[TrabajoRecepcional]
ADD CONSTRAINT [PK_TrabajoRecepcional]
    PRIMARY KEY CLUSTERED ([IdTrabajoRecepcional] ASC);
GO

-- Creating primary key on [Academico_IdAcademico], [ExperienciaEducativa_IdExperienciaEducativa] in table 'AcademicoExperienciaEducativa'
ALTER TABLE [dbo].[AcademicoExperienciaEducativa]
ADD CONSTRAINT [PK_AcademicoExperienciaEducativa]
    PRIMARY KEY CLUSTERED ([Academico_IdAcademico], [ExperienciaEducativa_IdExperienciaEducativa] ASC);
GO

-- Creating primary key on [ExperienciaEducativa1_IdExperienciaEducativa], [ProgramaEducativo2_IdPrograma] in table 'ProgramaEducativoExperienciaEducativa'
ALTER TABLE [dbo].[ProgramaEducativoExperienciaEducativa]
ADD CONSTRAINT [PK_ProgramaEducativoExperienciaEducativa]
    PRIMARY KEY CLUSTERED ([ExperienciaEducativa1_IdExperienciaEducativa], [ProgramaEducativo2_IdPrograma] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdPrograma] in table 'Academico'
ALTER TABLE [dbo].[Academico]
ADD CONSTRAINT [FK__Academico__IdPro__398D8EEE]
    FOREIGN KEY ([IdPrograma])
    REFERENCES [dbo].[ProgramaEducativo]
        ([IdPrograma])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Academico__IdPro__398D8EEE'
CREATE INDEX [IX_FK__Academico__IdPro__398D8EEE]
ON [dbo].[Academico]
    ([IdPrograma]);
GO

-- Creating foreign key on [IdAcademico] in table 'Constancia'
ALTER TABLE [dbo].[Constancia]
ADD CONSTRAINT [FK__Constanci__IdAca__3C69FB99]
    FOREIGN KEY ([IdAcademico])
    REFERENCES [dbo].[Academico]
        ([IdAcademico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Constanci__IdAca__3C69FB99'
CREATE INDEX [IX_FK__Constanci__IdAca__3C69FB99]
ON [dbo].[Constancia]
    ([IdAcademico]);
GO

-- Creating foreign key on [IdAcademico] in table 'Participacion'
ALTER TABLE [dbo].[Participacion]
ADD CONSTRAINT [FK__Participa__IdAca__4BAC3F29]
    FOREIGN KEY ([IdAcademico])
    REFERENCES [dbo].[Academico]
        ([IdAcademico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Participa__IdAca__4BAC3F29'
CREATE INDEX [IX_FK__Participa__IdAca__4BAC3F29]
ON [dbo].[Participacion]
    ([IdAcademico]);
GO

-- Creating foreign key on [IdAcademico] in table 'ProductoAcademico'
ALTER TABLE [dbo].[ProductoAcademico]
ADD CONSTRAINT [FK__ProductoA__IdAca__4222D4EF]
    FOREIGN KEY ([IdAcademico])
    REFERENCES [dbo].[Academico]
        ([IdAcademico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ProductoA__IdAca__4222D4EF'
CREATE INDEX [IX_FK__ProductoA__IdAca__4222D4EF]
ON [dbo].[ProductoAcademico]
    ([IdAcademico]);
GO

-- Creating foreign key on [IdAcademico] in table 'ProyectoCampo'
ALTER TABLE [dbo].[ProyectoCampo]
ADD CONSTRAINT [FK__ProyectoC__IdAca__3F466844]
    FOREIGN KEY ([IdAcademico])
    REFERENCES [dbo].[Academico]
        ([IdAcademico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ProyectoC__IdAca__3F466844'
CREATE INDEX [IX_FK__ProyectoC__IdAca__3F466844]
ON [dbo].[ProyectoCampo]
    ([IdAcademico]);
GO

-- Creating foreign key on [IdAcademico] in table 'TrabajoRecepcional'
ALTER TABLE [dbo].[TrabajoRecepcional]
ADD CONSTRAINT [FK__TrabajoRe__IdAca__44FF419A]
    FOREIGN KEY ([IdAcademico])
    REFERENCES [dbo].[Academico]
        ([IdAcademico])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TrabajoRe__IdAca__44FF419A'
CREATE INDEX [IX_FK__TrabajoRe__IdAca__44FF419A]
ON [dbo].[TrabajoRecepcional]
    ([IdAcademico]);
GO

-- Creating foreign key on [IdPrograma] in table 'ExperienciaEducativa'
ALTER TABLE [dbo].[ExperienciaEducativa]
ADD CONSTRAINT [FK__Experienc__IdPro__47DBAE45]
    FOREIGN KEY ([IdPrograma])
    REFERENCES [dbo].[ProgramaEducativo]
        ([IdPrograma])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Experienc__IdPro__47DBAE45'
CREATE INDEX [IX_FK__Experienc__IdPro__47DBAE45]
ON [dbo].[ExperienciaEducativa]
    ([IdPrograma]);
GO

-- Creating foreign key on [IdPrograma] in table 'Participacion'
ALTER TABLE [dbo].[Participacion]
ADD CONSTRAINT [FK__Participa__IdPro__4AB81AF0]
    FOREIGN KEY ([IdPrograma])
    REFERENCES [dbo].[ProgramaEducativo]
        ([IdPrograma])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Participa__IdPro__4AB81AF0'
CREATE INDEX [IX_FK__Participa__IdPro__4AB81AF0]
ON [dbo].[Participacion]
    ([IdPrograma]);
GO

-- Creating foreign key on [Academico_IdAcademico] in table 'AcademicoExperienciaEducativa'
ALTER TABLE [dbo].[AcademicoExperienciaEducativa]
ADD CONSTRAINT [FK_AcademicoExperienciaEducativa_Academico]
    FOREIGN KEY ([Academico_IdAcademico])
    REFERENCES [dbo].[Academico]
        ([IdAcademico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ExperienciaEducativa_IdExperienciaEducativa] in table 'AcademicoExperienciaEducativa'
ALTER TABLE [dbo].[AcademicoExperienciaEducativa]
ADD CONSTRAINT [FK_AcademicoExperienciaEducativa_ExperienciaEducativa]
    FOREIGN KEY ([ExperienciaEducativa_IdExperienciaEducativa])
    REFERENCES [dbo].[ExperienciaEducativa]
        ([IdExperienciaEducativa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AcademicoExperienciaEducativa_ExperienciaEducativa'
CREATE INDEX [IX_FK_AcademicoExperienciaEducativa_ExperienciaEducativa]
ON [dbo].[AcademicoExperienciaEducativa]
    ([ExperienciaEducativa_IdExperienciaEducativa]);
GO

-- Creating foreign key on [ExperienciaEducativa1_IdExperienciaEducativa] in table 'ProgramaEducativoExperienciaEducativa'
ALTER TABLE [dbo].[ProgramaEducativoExperienciaEducativa]
ADD CONSTRAINT [FK_ProgramaEducativoExperienciaEducativa_ExperienciaEducativa]
    FOREIGN KEY ([ExperienciaEducativa1_IdExperienciaEducativa])
    REFERENCES [dbo].[ExperienciaEducativa]
        ([IdExperienciaEducativa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProgramaEducativo2_IdPrograma] in table 'ProgramaEducativoExperienciaEducativa'
ALTER TABLE [dbo].[ProgramaEducativoExperienciaEducativa]
ADD CONSTRAINT [FK_ProgramaEducativoExperienciaEducativa_ProgramaEducativo]
    FOREIGN KEY ([ProgramaEducativo2_IdPrograma])
    REFERENCES [dbo].[ProgramaEducativo]
        ([IdPrograma])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramaEducativoExperienciaEducativa_ProgramaEducativo'
CREATE INDEX [IX_FK_ProgramaEducativoExperienciaEducativa_ProgramaEducativo]
ON [dbo].[ProgramaEducativoExperienciaEducativa]
    ([ProgramaEducativo2_IdPrograma]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------