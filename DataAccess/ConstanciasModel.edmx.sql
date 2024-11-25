
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/25/2024 08:00:07
-- Generated from EDMX file: C:\Users\marla\Constancias\DataAccess\ConstanciasModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [gestionconstancias];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_academico_area]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[academico] DROP CONSTRAINT [FK_academico_area];
GO
IF OBJECT_ID(N'[dbo].[FK_academico_tipoPersonal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[academico] DROP CONSTRAINT [FK_academico_tipoPersonal];
GO
IF OBJECT_ID(N'[dbo].[FK_constancia_academico]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[constancia] DROP CONSTRAINT [FK_constancia_academico];
GO
IF OBJECT_ID(N'[dbo].[FK_constancia_tipo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[constancia] DROP CONSTRAINT [FK_constancia_tipo];
GO
IF OBJECT_ID(N'[dbo].[FK_experienciaeducativa_programaeducativo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[experienciaeducativa] DROP CONSTRAINT [FK_experienciaeducativa_programaeducativo];
GO
IF OBJECT_ID(N'[dbo].[FK_participacion_academico]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[participacion] DROP CONSTRAINT [FK_participacion_academico];
GO
IF OBJECT_ID(N'[dbo].[FK_participacion_periodo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[participacion] DROP CONSTRAINT [FK_participacion_periodo];
GO
IF OBJECT_ID(N'[dbo].[FK_participacion_programa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[participacion] DROP CONSTRAINT [FK_participacion_programa];
GO
IF OBJECT_ID(N'[dbo].[FK_participacion_tipo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[participacion] DROP CONSTRAINT [FK_participacion_tipo];
GO
IF OBJECT_ID(N'[dbo].[FK_producto_academico]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[productoacademico] DROP CONSTRAINT [FK_producto_academico];
GO
IF OBJECT_ID(N'[dbo].[FK_producto_tipoproducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[productoacademico] DROP CONSTRAINT [FK_producto_tipoproducto];
GO
IF OBJECT_ID(N'[dbo].[FK_producto_tipopublicacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[productoacademico] DROP CONSTRAINT [FK_producto_tipopublicacion];
GO
IF OBJECT_ID(N'[dbo].[FK_programaeducativo_area]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[programaeducativo] DROP CONSTRAINT [FK_programaeducativo_area];
GO
IF OBJECT_ID(N'[dbo].[FK_proyectoc_academico]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[proyectocampo] DROP CONSTRAINT [FK_proyectoc_academico];
GO
IF OBJECT_ID(N'[dbo].[FK_proyectoc_periodo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[proyectocampo] DROP CONSTRAINT [FK_proyectoc_periodo];
GO
IF OBJECT_ID(N'[dbo].[FK_proyectoc_rol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[proyectocampo] DROP CONSTRAINT [FK_proyectoc_rol];
GO
IF OBJECT_ID(N'[dbo].[FK_trabajorecepcional_academico]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[trabajorecepcional] DROP CONSTRAINT [FK_trabajorecepcional_academico];
GO
IF OBJECT_ID(N'[dbo].[FK_trabajorecepcional_tipo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[trabajorecepcional] DROP CONSTRAINT [FK_trabajorecepcional_tipo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[academico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[academico];
GO
IF OBJECT_ID(N'[dbo].[areaacademica]', 'U') IS NOT NULL
    DROP TABLE [dbo].[areaacademica];
GO
IF OBJECT_ID(N'[dbo].[constancia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[constancia];
GO
IF OBJECT_ID(N'[dbo].[experienciaeducativa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[experienciaeducativa];
GO
IF OBJECT_ID(N'[dbo].[participacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[participacion];
GO
IF OBJECT_ID(N'[dbo].[periodo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[periodo];
GO
IF OBJECT_ID(N'[dbo].[productoacademico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productoacademico];
GO
IF OBJECT_ID(N'[dbo].[programaeducativo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[programaeducativo];
GO
IF OBJECT_ID(N'[dbo].[proyectocampo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[proyectocampo];
GO
IF OBJECT_ID(N'[dbo].[rolacademico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[rolacademico];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[tipoconstancia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tipoconstancia];
GO
IF OBJECT_ID(N'[dbo].[tipoparticipacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tipoparticipacion];
GO
IF OBJECT_ID(N'[dbo].[tipopersonal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tipopersonal];
GO
IF OBJECT_ID(N'[dbo].[tipoproducto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tipoproducto];
GO
IF OBJECT_ID(N'[dbo].[tipopublicacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tipopublicacion];
GO
IF OBJECT_ID(N'[dbo].[tipotrabajo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tipotrabajo];
GO
IF OBJECT_ID(N'[dbo].[trabajorecepcional]', 'U') IS NOT NULL
    DROP TABLE [dbo].[trabajorecepcional];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'academicoes'
CREATE TABLE [dbo].[academicoes] (
    [idAcademico] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(50)  NULL,
    [numeroPersonal] varchar(10)  NULL,
    [fechaContratacion] varchar(10)  NULL,
    [idAreaAcademica] int  NULL,
    [idTipoPersonal] int  NULL
);
GO

-- Creating table 'areaacademicas'
CREATE TABLE [dbo].[areaacademicas] (
    [idAreaAcademica] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(50)  NULL
);
GO

-- Creating table 'constancias'
CREATE TABLE [dbo].[constancias] (
    [idConstancia] int IDENTITY(1,1) NOT NULL,
    [fechaExpedicion] varchar(10)  NULL,
    [idTipoConstancia] int  NULL,
    [idAcademico] int  NULL,
    [firmaElectronica] nchar(16)  NULL
);
GO

-- Creating table 'experienciaeducativas'
CREATE TABLE [dbo].[experienciaeducativas] (
    [idExperienciaEducativa] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(25)  NULL,
    [sec] varchar(10)  NULL,
    [creditos] int  NULL,
    [horasPracticas] int  NULL,
    [horasTeoricas] int  NULL,
    [idProgramaEducativo] int  NULL
);
GO

-- Creating table 'participacions'
CREATE TABLE [dbo].[participacions] (
    [idProyectoCampo] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(25)  NULL,
    [idPeriodo] int  NULL,
    [idTipoParticipacion] int  NULL,
    [idProgramaEducativo] int  NULL,
    [idAcademico] int  NULL
);
GO

-- Creating table 'periodoes'
CREATE TABLE [dbo].[periodoes] (
    [idPeriodo] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(22)  NULL
);
GO

-- Creating table 'productoacademicoes'
CREATE TABLE [dbo].[productoacademicoes] (
    [idProducto] int IDENTITY(1,1) NOT NULL,
    [fechaPublicacion] varchar(10)  NULL,
    [titulo] varchar(25)  NULL,
    [idTipoProducto] int  NULL,
    [idTipoPublicacion] int  NULL,
    [idAcademico] int  NULL
);
GO

-- Creating table 'programaeducativoes'
CREATE TABLE [dbo].[programaeducativoes] (
    [idProgramaEducativo] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(25)  NULL,
    [fecha] varchar(5)  NULL,
    [idAreaAcademica] int  NULL
);
GO

-- Creating table 'proyectocampoes'
CREATE TABLE [dbo].[proyectocampoes] (
    [idProyectoCampo] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(25)  NULL,
    [lugarRealizacion] varchar(20)  NULL,
    [idPeriodo] int  NULL,
    [idRolAcademico] int  NULL,
    [idAcademico] int  NULL
);
GO

-- Creating table 'rolacademicoes'
CREATE TABLE [dbo].[rolacademicoes] (
    [idRolAcademico] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(30)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'tipoconstancias'
CREATE TABLE [dbo].[tipoconstancias] (
    [idTipoConstancia] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NULL
);
GO

-- Creating table 'tipoparticipacions'
CREATE TABLE [dbo].[tipoparticipacions] (
    [idTipoParticipacion] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NULL
);
GO

-- Creating table 'tipopersonals'
CREATE TABLE [dbo].[tipopersonals] (
    [idTipoPersonal] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(50)  NULL
);
GO

-- Creating table 'tipoproductoes'
CREATE TABLE [dbo].[tipoproductoes] (
    [idTipoProducto] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(30)  NULL
);
GO

-- Creating table 'tipopublicacions'
CREATE TABLE [dbo].[tipopublicacions] (
    [idTipoPublicacion] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(30)  NULL
);
GO

-- Creating table 'tipotrabajoes'
CREATE TABLE [dbo].[tipotrabajoes] (
    [idTipoTrabajo] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NULL
);
GO

-- Creating table 'trabajorecepcionals'
CREATE TABLE [dbo].[trabajorecepcionals] (
    [idTrabajoRecepcional] int IDENTITY(1,1) NOT NULL,
    [nombreEstudiante] varchar(50)  NULL,
    [titulo] varchar(25)  NULL,
    [infraestructuraOpcional] varchar(30)  NULL,
    [idAcademico] int  NULL,
    [idTipoTrabajo] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idAcademico] in table 'academicoes'
ALTER TABLE [dbo].[academicoes]
ADD CONSTRAINT [PK_academicoes]
    PRIMARY KEY CLUSTERED ([idAcademico] ASC);
GO

-- Creating primary key on [idAreaAcademica] in table 'areaacademicas'
ALTER TABLE [dbo].[areaacademicas]
ADD CONSTRAINT [PK_areaacademicas]
    PRIMARY KEY CLUSTERED ([idAreaAcademica] ASC);
GO

-- Creating primary key on [idConstancia] in table 'constancias'
ALTER TABLE [dbo].[constancias]
ADD CONSTRAINT [PK_constancias]
    PRIMARY KEY CLUSTERED ([idConstancia] ASC);
GO

-- Creating primary key on [idExperienciaEducativa] in table 'experienciaeducativas'
ALTER TABLE [dbo].[experienciaeducativas]
ADD CONSTRAINT [PK_experienciaeducativas]
    PRIMARY KEY CLUSTERED ([idExperienciaEducativa] ASC);
GO

-- Creating primary key on [idProyectoCampo] in table 'participacions'
ALTER TABLE [dbo].[participacions]
ADD CONSTRAINT [PK_participacions]
    PRIMARY KEY CLUSTERED ([idProyectoCampo] ASC);
GO

-- Creating primary key on [idPeriodo] in table 'periodoes'
ALTER TABLE [dbo].[periodoes]
ADD CONSTRAINT [PK_periodoes]
    PRIMARY KEY CLUSTERED ([idPeriodo] ASC);
GO

-- Creating primary key on [idProducto] in table 'productoacademicoes'
ALTER TABLE [dbo].[productoacademicoes]
ADD CONSTRAINT [PK_productoacademicoes]
    PRIMARY KEY CLUSTERED ([idProducto] ASC);
GO

-- Creating primary key on [idProgramaEducativo] in table 'programaeducativoes'
ALTER TABLE [dbo].[programaeducativoes]
ADD CONSTRAINT [PK_programaeducativoes]
    PRIMARY KEY CLUSTERED ([idProgramaEducativo] ASC);
GO

-- Creating primary key on [idProyectoCampo] in table 'proyectocampoes'
ALTER TABLE [dbo].[proyectocampoes]
ADD CONSTRAINT [PK_proyectocampoes]
    PRIMARY KEY CLUSTERED ([idProyectoCampo] ASC);
GO

-- Creating primary key on [idRolAcademico] in table 'rolacademicoes'
ALTER TABLE [dbo].[rolacademicoes]
ADD CONSTRAINT [PK_rolacademicoes]
    PRIMARY KEY CLUSTERED ([idRolAcademico] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [idTipoConstancia] in table 'tipoconstancias'
ALTER TABLE [dbo].[tipoconstancias]
ADD CONSTRAINT [PK_tipoconstancias]
    PRIMARY KEY CLUSTERED ([idTipoConstancia] ASC);
GO

-- Creating primary key on [idTipoParticipacion] in table 'tipoparticipacions'
ALTER TABLE [dbo].[tipoparticipacions]
ADD CONSTRAINT [PK_tipoparticipacions]
    PRIMARY KEY CLUSTERED ([idTipoParticipacion] ASC);
GO

-- Creating primary key on [idTipoPersonal] in table 'tipopersonals'
ALTER TABLE [dbo].[tipopersonals]
ADD CONSTRAINT [PK_tipopersonals]
    PRIMARY KEY CLUSTERED ([idTipoPersonal] ASC);
GO

-- Creating primary key on [idTipoProducto] in table 'tipoproductoes'
ALTER TABLE [dbo].[tipoproductoes]
ADD CONSTRAINT [PK_tipoproductoes]
    PRIMARY KEY CLUSTERED ([idTipoProducto] ASC);
GO

-- Creating primary key on [idTipoPublicacion] in table 'tipopublicacions'
ALTER TABLE [dbo].[tipopublicacions]
ADD CONSTRAINT [PK_tipopublicacions]
    PRIMARY KEY CLUSTERED ([idTipoPublicacion] ASC);
GO

-- Creating primary key on [idTipoTrabajo] in table 'tipotrabajoes'
ALTER TABLE [dbo].[tipotrabajoes]
ADD CONSTRAINT [PK_tipotrabajoes]
    PRIMARY KEY CLUSTERED ([idTipoTrabajo] ASC);
GO

-- Creating primary key on [idTrabajoRecepcional] in table 'trabajorecepcionals'
ALTER TABLE [dbo].[trabajorecepcionals]
ADD CONSTRAINT [PK_trabajorecepcionals]
    PRIMARY KEY CLUSTERED ([idTrabajoRecepcional] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idAreaAcademica] in table 'academicoes'
ALTER TABLE [dbo].[academicoes]
ADD CONSTRAINT [FK_academico_area]
    FOREIGN KEY ([idAreaAcademica])
    REFERENCES [dbo].[areaacademicas]
        ([idAreaAcademica])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_academico_area'
CREATE INDEX [IX_FK_academico_area]
ON [dbo].[academicoes]
    ([idAreaAcademica]);
GO

-- Creating foreign key on [idTipoPersonal] in table 'academicoes'
ALTER TABLE [dbo].[academicoes]
ADD CONSTRAINT [FK_academico_tipoPersonal]
    FOREIGN KEY ([idTipoPersonal])
    REFERENCES [dbo].[tipopersonals]
        ([idTipoPersonal])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_academico_tipoPersonal'
CREATE INDEX [IX_FK_academico_tipoPersonal]
ON [dbo].[academicoes]
    ([idTipoPersonal]);
GO

-- Creating foreign key on [idAcademico] in table 'constancias'
ALTER TABLE [dbo].[constancias]
ADD CONSTRAINT [FK_constancia_academico]
    FOREIGN KEY ([idAcademico])
    REFERENCES [dbo].[academicoes]
        ([idAcademico])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_constancia_academico'
CREATE INDEX [IX_FK_constancia_academico]
ON [dbo].[constancias]
    ([idAcademico]);
GO

-- Creating foreign key on [idAcademico] in table 'participacions'
ALTER TABLE [dbo].[participacions]
ADD CONSTRAINT [FK_participacion_academico]
    FOREIGN KEY ([idAcademico])
    REFERENCES [dbo].[academicoes]
        ([idAcademico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_participacion_academico'
CREATE INDEX [IX_FK_participacion_academico]
ON [dbo].[participacions]
    ([idAcademico]);
GO

-- Creating foreign key on [idAcademico] in table 'productoacademicoes'
ALTER TABLE [dbo].[productoacademicoes]
ADD CONSTRAINT [FK_producto_academico]
    FOREIGN KEY ([idAcademico])
    REFERENCES [dbo].[academicoes]
        ([idAcademico])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_producto_academico'
CREATE INDEX [IX_FK_producto_academico]
ON [dbo].[productoacademicoes]
    ([idAcademico]);
GO

-- Creating foreign key on [idAcademico] in table 'proyectocampoes'
ALTER TABLE [dbo].[proyectocampoes]
ADD CONSTRAINT [FK_proyectoc_academico]
    FOREIGN KEY ([idAcademico])
    REFERENCES [dbo].[academicoes]
        ([idAcademico])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_proyectoc_academico'
CREATE INDEX [IX_FK_proyectoc_academico]
ON [dbo].[proyectocampoes]
    ([idAcademico]);
GO

-- Creating foreign key on [idAcademico] in table 'trabajorecepcionals'
ALTER TABLE [dbo].[trabajorecepcionals]
ADD CONSTRAINT [FK_trabajorecepcional_academico]
    FOREIGN KEY ([idAcademico])
    REFERENCES [dbo].[academicoes]
        ([idAcademico])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_trabajorecepcional_academico'
CREATE INDEX [IX_FK_trabajorecepcional_academico]
ON [dbo].[trabajorecepcionals]
    ([idAcademico]);
GO

-- Creating foreign key on [idAreaAcademica] in table 'programaeducativoes'
ALTER TABLE [dbo].[programaeducativoes]
ADD CONSTRAINT [FK_programaeducativo_area]
    FOREIGN KEY ([idAreaAcademica])
    REFERENCES [dbo].[areaacademicas]
        ([idAreaAcademica])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_programaeducativo_area'
CREATE INDEX [IX_FK_programaeducativo_area]
ON [dbo].[programaeducativoes]
    ([idAreaAcademica]);
GO

-- Creating foreign key on [idTipoConstancia] in table 'constancias'
ALTER TABLE [dbo].[constancias]
ADD CONSTRAINT [FK_constancia_tipo]
    FOREIGN KEY ([idTipoConstancia])
    REFERENCES [dbo].[tipoconstancias]
        ([idTipoConstancia])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_constancia_tipo'
CREATE INDEX [IX_FK_constancia_tipo]
ON [dbo].[constancias]
    ([idTipoConstancia]);
GO

-- Creating foreign key on [idProgramaEducativo] in table 'experienciaeducativas'
ALTER TABLE [dbo].[experienciaeducativas]
ADD CONSTRAINT [FK_experienciaeducativa_programaeducativo]
    FOREIGN KEY ([idProgramaEducativo])
    REFERENCES [dbo].[programaeducativoes]
        ([idProgramaEducativo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_experienciaeducativa_programaeducativo'
CREATE INDEX [IX_FK_experienciaeducativa_programaeducativo]
ON [dbo].[experienciaeducativas]
    ([idProgramaEducativo]);
GO

-- Creating foreign key on [idPeriodo] in table 'participacions'
ALTER TABLE [dbo].[participacions]
ADD CONSTRAINT [FK_participacion_periodo]
    FOREIGN KEY ([idPeriodo])
    REFERENCES [dbo].[periodoes]
        ([idPeriodo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_participacion_periodo'
CREATE INDEX [IX_FK_participacion_periodo]
ON [dbo].[participacions]
    ([idPeriodo]);
GO

-- Creating foreign key on [idProgramaEducativo] in table 'participacions'
ALTER TABLE [dbo].[participacions]
ADD CONSTRAINT [FK_participacion_programa]
    FOREIGN KEY ([idProgramaEducativo])
    REFERENCES [dbo].[programaeducativoes]
        ([idProgramaEducativo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_participacion_programa'
CREATE INDEX [IX_FK_participacion_programa]
ON [dbo].[participacions]
    ([idProgramaEducativo]);
GO

-- Creating foreign key on [idTipoParticipacion] in table 'participacions'
ALTER TABLE [dbo].[participacions]
ADD CONSTRAINT [FK_participacion_tipo]
    FOREIGN KEY ([idTipoParticipacion])
    REFERENCES [dbo].[tipoparticipacions]
        ([idTipoParticipacion])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_participacion_tipo'
CREATE INDEX [IX_FK_participacion_tipo]
ON [dbo].[participacions]
    ([idTipoParticipacion]);
GO

-- Creating foreign key on [idPeriodo] in table 'proyectocampoes'
ALTER TABLE [dbo].[proyectocampoes]
ADD CONSTRAINT [FK_proyectoc_periodo]
    FOREIGN KEY ([idPeriodo])
    REFERENCES [dbo].[periodoes]
        ([idPeriodo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_proyectoc_periodo'
CREATE INDEX [IX_FK_proyectoc_periodo]
ON [dbo].[proyectocampoes]
    ([idPeriodo]);
GO

-- Creating foreign key on [idTipoProducto] in table 'productoacademicoes'
ALTER TABLE [dbo].[productoacademicoes]
ADD CONSTRAINT [FK_producto_tipoproducto]
    FOREIGN KEY ([idTipoProducto])
    REFERENCES [dbo].[tipoproductoes]
        ([idTipoProducto])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_producto_tipoproducto'
CREATE INDEX [IX_FK_producto_tipoproducto]
ON [dbo].[productoacademicoes]
    ([idTipoProducto]);
GO

-- Creating foreign key on [idTipoPublicacion] in table 'productoacademicoes'
ALTER TABLE [dbo].[productoacademicoes]
ADD CONSTRAINT [FK_producto_tipopublicacion]
    FOREIGN KEY ([idTipoPublicacion])
    REFERENCES [dbo].[tipopublicacions]
        ([idTipoPublicacion])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_producto_tipopublicacion'
CREATE INDEX [IX_FK_producto_tipopublicacion]
ON [dbo].[productoacademicoes]
    ([idTipoPublicacion]);
GO

-- Creating foreign key on [idRolAcademico] in table 'proyectocampoes'
ALTER TABLE [dbo].[proyectocampoes]
ADD CONSTRAINT [FK_proyectoc_rol]
    FOREIGN KEY ([idRolAcademico])
    REFERENCES [dbo].[rolacademicoes]
        ([idRolAcademico])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_proyectoc_rol'
CREATE INDEX [IX_FK_proyectoc_rol]
ON [dbo].[proyectocampoes]
    ([idRolAcademico]);
GO

-- Creating foreign key on [idTipoTrabajo] in table 'trabajorecepcionals'
ALTER TABLE [dbo].[trabajorecepcionals]
ADD CONSTRAINT [FK_trabajorecepcional_tipo]
    FOREIGN KEY ([idTipoTrabajo])
    REFERENCES [dbo].[tipotrabajoes]
        ([idTipoTrabajo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_trabajorecepcional_tipo'
CREATE INDEX [IX_FK_trabajorecepcional_tipo]
ON [dbo].[trabajorecepcionals]
    ([idTipoTrabajo]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------