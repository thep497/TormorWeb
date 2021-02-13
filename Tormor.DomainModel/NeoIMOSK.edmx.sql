
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/07/2011 08:56:18
-- Generated from EDMX file: D:\Programming\Tormor\Programming\TormorWeb\Tormor.DomainModel\NeoIMOSK.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NeoIMOSK];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AlienVisaDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisaDetails] DROP CONSTRAINT [FK_AlienVisaDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_AlienReEntryDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReEntrys] DROP CONSTRAINT [FK_AlienReEntryDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_AlienStaying90Day]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Staying90Days] DROP CONSTRAINT [FK_AlienStaying90Day];
GO
IF OBJECT_ID(N'[dbo].[FK_AlienCheckOut]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Endorses] DROP CONSTRAINT [FK_AlienCheckOut];
GO
IF OBJECT_ID(N'[dbo].[FK_ConveyanceConveyancenOut]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConveyanceInOuts] DROP CONSTRAINT [FK_ConveyanceConveyancenOut];
GO
IF OBJECT_ID(N'[dbo].[FK_ConveyanceInOutPassenger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Crews] DROP CONSTRAINT [FK_ConveyanceInOutPassenger];
GO
IF OBJECT_ID(N'[dbo].[FK_AlienPassenger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Crews] DROP CONSTRAINT [FK_AlienPassenger];
GO
IF OBJECT_ID(N'[dbo].[FK_EndorseEndorseStamp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EndorseStamps] DROP CONSTRAINT [FK_EndorseEndorseStamp];
GO
IF OBJECT_ID(N'[dbo].[FK_AlienAddRemoveCrew]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AddRemoveCrews] DROP CONSTRAINT [FK_AlienAddRemoveCrew];
GO
IF OBJECT_ID(N'[dbo].[FK_AlienPassportHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PassportHistories] DROP CONSTRAINT [FK_AlienPassportHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_ConveyanceInOutAddRemoveCrew]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AddRemoveCrews] DROP CONSTRAINT [FK_ConveyanceInOutAddRemoveCrew];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[VisaDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VisaDetails];
GO
IF OBJECT_ID(N'[dbo].[ReEntrys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReEntrys];
GO
IF OBJECT_ID(N'[dbo].[Aliens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Aliens];
GO
IF OBJECT_ID(N'[dbo].[Staying90Days]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staying90Days];
GO
IF OBJECT_ID(N'[dbo].[Endorses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Endorses];
GO
IF OBJECT_ID(N'[dbo].[zz_Environs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[zz_Environs];
GO
IF OBJECT_ID(N'[dbo].[Conveyances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conveyances];
GO
IF OBJECT_ID(N'[dbo].[ConveyanceInOuts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConveyanceInOuts];
GO
IF OBJECT_ID(N'[dbo].[Crews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Crews];
GO
IF OBJECT_ID(N'[dbo].[zz_References]', 'U') IS NOT NULL
    DROP TABLE [dbo].[zz_References];
GO
IF OBJECT_ID(N'[dbo].[EndorseStamps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EndorseStamps];
GO
IF OBJECT_ID(N'[dbo].[AddRemoveCrews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddRemoveCrews];
GO
IF OBJECT_ID(N'[dbo].[PassportHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PassportHistories];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'VisaDetails'
CREATE TABLE [dbo].[VisaDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AlienId] int  NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [RequestDate] datetime  NOT NULL,
    [ResultAppointmentDate] datetime  NULL,
    [StayType] nvarchar(50)  NOT NULL,
    [StayPeriod] nvarchar(50)  NOT NULL,
    [StayReason] nvarchar(50)  NOT NULL,
    [StayReasonDetail] nvarchar(max)  NULL,
    [PassportCard_DocNo] nvarchar(50)  NULL,
    [PassportCard_DateIssued] datetime  NULL,
    [PassportCard_DateExpired] datetime  NULL,
    [PassportCard_IssuedFrom] nvarchar(max)  NULL,
    [DateArrive] datetime  NULL,
    [IsPermit] bit  NULL,
    [PermitToDate] datetime  NULL,
    [Invoice_InvoiceNo] nvarchar(50)  NULL,
    [Invoice_Charge] decimal(18,0)  NULL,
    [CurrentAddress_AddrNo] nvarchar(max)  NULL,
    [CurrentAddress_Road] nvarchar(max)  NULL,
    [CurrentAddress_Tumbol] nvarchar(max)  NULL,
    [CurrentAddress_Amphur] nvarchar(max)  NULL,
    [CurrentAddress_Province] nvarchar(max)  NULL,
    [CurrentAddress_Phone] nvarchar(max)  NULL,
    [CurrentAddress_Postcode] nvarchar(max)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'ReEntrys'
CREATE TABLE [dbo].[ReEntrys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AlienId] int  NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [RequestDate] datetime  NOT NULL,
    [PassportCard_DocNo] nvarchar(50)  NULL,
    [PassportCard_DateIssued] datetime  NULL,
    [PassportCard_DateExpired] datetime  NULL,
    [PassportCard_IssuedFrom] nvarchar(max)  NULL,
    [ReEntryCode] nvarchar(max)  NOT NULL,
    [SMTime] nvarchar(max)  NOT NULL,
    [Invoice_InvoiceNo] nvarchar(50)  NULL,
    [Invoice_Charge] decimal(18,0)  NULL,
    [PermitToDate] datetime  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'Aliens'
CREATE TABLE [dbo].[Aliens] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name_Title] nvarchar(50)  NULL,
    [Name_FirstName] nvarchar(max)  NULL,
    [Name_MiddleName] nvarchar(max)  NULL,
    [Name_LastName] nvarchar(max)  NULL,
    [DateOfBirth] datetime  NULL,
    [IsThai] bit  NOT NULL,
    [Nationality] nvarchar(max)  NULL,
    [Sex] nvarchar(max)  NULL,
    [DataInType] int  NULL,
    [CurrentAddress_AddrNo] nvarchar(max)  NULL,
    [CurrentAddress_Road] nvarchar(max)  NULL,
    [CurrentAddress_Tumbol] nvarchar(max)  NULL,
    [CurrentAddress_Amphur] nvarchar(max)  NULL,
    [CurrentAddress_Province] nvarchar(max)  NULL,
    [CurrentAddress_Phone] nvarchar(max)  NULL,
    [CurrentAddress_Postcode] nvarchar(max)  NULL,
    [PassportCard_DocNo] nvarchar(50)  NULL,
    [PassportCard_DateIssued] datetime  NULL,
    [PassportCard_DateExpired] datetime  NULL,
    [PassportCard_IssuedFrom] nvarchar(max)  NULL,
    [IDCardNo] nvarchar(max)  NULL,
    [SeamanCardNo] nvarchar(max)  NULL,
    [HabitatCard_DocNo] nvarchar(50)  NULL,
    [HabitatCard_DateIssued] datetime  NULL,
    [HabitatCard_DateExpired] datetime  NULL,
    [HabitatCard_IssuedFrom] nvarchar(max)  NULL,
    [Remark] nvarchar(max)  NULL,
    [Photo_FPicture] varbinary(max)  NULL,
    [Photo_ContentType] nvarchar(max)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'Staying90Days'
CREATE TABLE [dbo].[Staying90Days] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AlienId] int  NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [RequestDate] datetime  NOT NULL,
    [ArrivalDate] datetime  NULL,
    [ArrivalBy] nvarchar(max)  NULL,
    [VisaType] int  NULL,
    [PassportCard_DocNo] nvarchar(50)  NULL,
    [PassportCard_DateIssued] datetime  NULL,
    [PassportCard_DateExpired] datetime  NULL,
    [PassportCard_IssuedFrom] nvarchar(max)  NULL,
    [ArrivalCard_DocNo] nvarchar(50)  NULL,
    [ArrivalCard_DateIssued] datetime  NULL,
    [ArrivalCard_DateExpired] datetime  NULL,
    [ArrivalCard_IssuedFrom] nvarchar(max)  NULL,
    [CurrentAddress_AddrNo] nvarchar(max)  NULL,
    [CurrentAddress_Road] nvarchar(max)  NULL,
    [CurrentAddress_Tumbol] nvarchar(max)  NULL,
    [CurrentAddress_Amphur] nvarchar(max)  NULL,
    [CurrentAddress_Province] nvarchar(max)  NULL,
    [CurrentAddress_Phone] nvarchar(max)  NULL,
    [CurrentAddress_Postcode] nvarchar(max)  NULL,
    [Invoice_InvoiceNo] nvarchar(50)  NULL,
    [Invoice_Charge] decimal(18,0)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'Endorses'
CREATE TABLE [dbo].[Endorses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AlienId] int  NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [TMType] nvarchar(max)  NOT NULL,
    [HabitatCard_DocNo] nvarchar(50)  NULL,
    [HabitatCard_DateIssued] datetime  NULL,
    [HabitatCard_DateExpired] datetime  NULL,
    [HabitatCard_IssuedFrom] nvarchar(max)  NULL,
    [PassportCard_DocNo] nvarchar(50)  NULL,
    [PassportCard_DateIssued] datetime  NULL,
    [PassportCard_DateExpired] datetime  NULL,
    [PassportCard_IssuedFrom] nvarchar(max)  NULL,
    [OutDetail_Destination] nvarchar(max)  NULL,
    [OutDetail_ByVehicle] nvarchar(max)  NULL,
    [OutDetail_OutDate] datetime  NULL,
    [RequestDate] datetime  NOT NULL,
    [ExpiredDate] datetime  NULL,
    [Invoice_InvoiceNo] nvarchar(50)  NULL,
    [Invoice_Charge] decimal(18,0)  NULL,
    [CurrentAddress_AddrNo] nvarchar(max)  NULL,
    [CurrentAddress_Road] nvarchar(max)  NULL,
    [CurrentAddress_Tumbol] nvarchar(max)  NULL,
    [CurrentAddress_Amphur] nvarchar(max)  NULL,
    [CurrentAddress_Province] nvarchar(max)  NULL,
    [CurrentAddress_Phone] nvarchar(max)  NULL,
    [CurrentAddress_Postcode] nvarchar(max)  NULL,
    [Remark] nvarchar(max)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'zz_Environs'
CREATE TABLE [dbo].[zz_Environs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OfficeName] nvarchar(max)  NOT NULL,
    [OfficeAddress_AddrNo] nvarchar(max)  NULL,
    [OfficeAddress_Road] nvarchar(max)  NULL,
    [OfficeAddress_Tumbol] nvarchar(max)  NULL,
    [OfficeAddress_Amphur] nvarchar(max)  NULL,
    [OfficeAddress_Province] nvarchar(max)  NULL,
    [OfficeAddress_Phone] nvarchar(max)  NULL,
    [OfficeAddress_Postcode] nvarchar(max)  NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL
);
GO

-- Creating table 'Conveyances'
CREATE TABLE [dbo].[Conveyances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NULL,
    [RegistrationNo] nvarchar(200)  NULL,
    [OwnerName] nvarchar(200)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'ConveyanceInOuts'
CREATE TABLE [dbo].[ConveyanceInOuts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [ConveyanceId] int  NOT NULL,
    [InOutType] nvarchar(max)  NOT NULL,
    [RequestDate] datetime  NOT NULL,
    [InOutDate] datetime  NOT NULL,
    [InOutTime] datetime  NULL,
    [PortInFrom_PortName] nvarchar(max)  NULL,
    [PortInFrom_Country] nvarchar(max)  NULL,
    [PortInTo_PortName] nvarchar(max)  NULL,
    [PortInTo_Country] nvarchar(max)  NULL,
    [PortOutFrom_PortName] nvarchar(max)  NULL,
    [PortOutFrom_Country] nvarchar(max)  NULL,
    [PortOutTo_PortName] nvarchar(max)  NULL,
    [PortOutTo_Country] nvarchar(max)  NULL,
    [NumCrew] int  NULL,
    [NumPassenger] int  NULL,
    [AgencyName] nvarchar(max)  NULL,
    [InspectOfficer] nvarchar(max)  NULL,
    [Invoice_InvoiceNo] nvarchar(50)  NULL,
    [Invoice_Charge] decimal(18,0)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'Crews'
CREATE TABLE [dbo].[Crews] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ConveyanceInOutId] int  NOT NULL,
    [AlienId] int  NOT NULL,
    [IsCrew] bit  NULL,
    [Code] nvarchar(50)  NOT NULL,
    [PassportCard_DocNo] nvarchar(50)  NULL,
    [PassportCard_DateIssued] datetime  NULL,
    [PassportCard_DateExpired] datetime  NULL,
    [PassportCard_IssuedFrom] nvarchar(max)  NULL,
    [IDCardNo] nvarchar(max)  NULL,
    [SeamanCardNo] nvarchar(max)  NULL,
    [Remark] nvarchar(max)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'zz_References'
CREATE TABLE [dbo].[zz_References] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefTypeId] int  NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [RefName] nvarchar(250)  NOT NULL,
    [RefDesc] nvarchar(250)  NULL,
    [RefRefTypeId] int  NULL,
    [RefCode] nvarchar(50)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL
);
GO

-- Creating table 'EndorseStamps'
CREATE TABLE [dbo].[EndorseStamps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EndorseId] int  NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [StampDate] datetime  NOT NULL,
    [StampExpiredDate] datetime  NULL,
    [Invoice_InvoiceNo] nvarchar(50)  NULL,
    [Invoice_Charge] decimal(18,0)  NULL,
    [SMTime] nvarchar(max)  NOT NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'AddRemoveCrews'
CREATE TABLE [dbo].[AddRemoveCrews] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AlienId] int  NOT NULL,
    [ConveyanceInOutId] int  NULL,
    [AddRemoveType] int  NOT NULL,
    [Code] nvarchar(50)  NOT NULL,
    [SubCode] nvarchar(max)  NULL,
    [RequestDate] datetime  NOT NULL,
    [Company] nvarchar(max)  NULL,
    [InDetail_InDate] datetime  NULL,
    [InDetail_InMethod] nvarchar(max)  NULL,
    [InDetail_InWay] nvarchar(max)  NULL,
    [OutDetail_InDate] datetime  NULL,
    [OutDetail_InMethod] nvarchar(max)  NULL,
    [OutDetail_InWay] nvarchar(max)  NULL,
    [Invoice_InvoiceNo] nvarchar(50)  NULL,
    [Invoice_Charge] decimal(18,0)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- Creating table 'PassportHistories'
CREATE TABLE [dbo].[PassportHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsPassport] bit  NOT NULL,
    [AlienId] int  NOT NULL,
    [PassportCard_DocNo] nvarchar(50)  NULL,
    [PassportCard_DateIssued] datetime  NULL,
    [PassportCard_DateExpired] datetime  NULL,
    [PassportCard_IssuedFrom] nvarchar(max)  NULL,
    [IsCancel] bit  NOT NULL,
    [UpdateInfo_AddedBy] nvarchar(50)  NULL,
    [UpdateInfo_AddedDate] datetime  NULL,
    [UpdateInfo_UpdatedBy] nvarchar(50)  NULL,
    [UpdateInfo_UpdatedDate] datetime  NULL,
    [ExtendedData_Custom1] nvarchar(max)  NULL,
    [ExtendedData_Custom2] nvarchar(max)  NULL,
    [ExtendedData_Custom3] nvarchar(max)  NULL,
    [ExtendedData_Custom4] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'VisaDetails'
ALTER TABLE [dbo].[VisaDetails]
ADD CONSTRAINT [PK_VisaDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReEntrys'
ALTER TABLE [dbo].[ReEntrys]
ADD CONSTRAINT [PK_ReEntrys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Aliens'
ALTER TABLE [dbo].[Aliens]
ADD CONSTRAINT [PK_Aliens]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Staying90Days'
ALTER TABLE [dbo].[Staying90Days]
ADD CONSTRAINT [PK_Staying90Days]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Endorses'
ALTER TABLE [dbo].[Endorses]
ADD CONSTRAINT [PK_Endorses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'zz_Environs'
ALTER TABLE [dbo].[zz_Environs]
ADD CONSTRAINT [PK_zz_Environs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Conveyances'
ALTER TABLE [dbo].[Conveyances]
ADD CONSTRAINT [PK_Conveyances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConveyanceInOuts'
ALTER TABLE [dbo].[ConveyanceInOuts]
ADD CONSTRAINT [PK_ConveyanceInOuts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Crews'
ALTER TABLE [dbo].[Crews]
ADD CONSTRAINT [PK_Crews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'zz_References'
ALTER TABLE [dbo].[zz_References]
ADD CONSTRAINT [PK_zz_References]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EndorseStamps'
ALTER TABLE [dbo].[EndorseStamps]
ADD CONSTRAINT [PK_EndorseStamps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AddRemoveCrews'
ALTER TABLE [dbo].[AddRemoveCrews]
ADD CONSTRAINT [PK_AddRemoveCrews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PassportHistories'
ALTER TABLE [dbo].[PassportHistories]
ADD CONSTRAINT [PK_PassportHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AlienId] in table 'VisaDetails'
ALTER TABLE [dbo].[VisaDetails]
ADD CONSTRAINT [FK_AlienVisaDetail]
    FOREIGN KEY ([AlienId])
    REFERENCES [dbo].[Aliens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlienVisaDetail'
CREATE INDEX [IX_FK_AlienVisaDetail]
ON [dbo].[VisaDetails]
    ([AlienId]);
GO

-- Creating foreign key on [AlienId] in table 'ReEntrys'
ALTER TABLE [dbo].[ReEntrys]
ADD CONSTRAINT [FK_AlienReEntryDetail]
    FOREIGN KEY ([AlienId])
    REFERENCES [dbo].[Aliens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlienReEntryDetail'
CREATE INDEX [IX_FK_AlienReEntryDetail]
ON [dbo].[ReEntrys]
    ([AlienId]);
GO

-- Creating foreign key on [AlienId] in table 'Staying90Days'
ALTER TABLE [dbo].[Staying90Days]
ADD CONSTRAINT [FK_AlienStaying90Day]
    FOREIGN KEY ([AlienId])
    REFERENCES [dbo].[Aliens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlienStaying90Day'
CREATE INDEX [IX_FK_AlienStaying90Day]
ON [dbo].[Staying90Days]
    ([AlienId]);
GO

-- Creating foreign key on [AlienId] in table 'Endorses'
ALTER TABLE [dbo].[Endorses]
ADD CONSTRAINT [FK_AlienCheckOut]
    FOREIGN KEY ([AlienId])
    REFERENCES [dbo].[Aliens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlienCheckOut'
CREATE INDEX [IX_FK_AlienCheckOut]
ON [dbo].[Endorses]
    ([AlienId]);
GO

-- Creating foreign key on [ConveyanceId] in table 'ConveyanceInOuts'
ALTER TABLE [dbo].[ConveyanceInOuts]
ADD CONSTRAINT [FK_ConveyanceConveyancenOut]
    FOREIGN KEY ([ConveyanceId])
    REFERENCES [dbo].[Conveyances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConveyanceConveyancenOut'
CREATE INDEX [IX_FK_ConveyanceConveyancenOut]
ON [dbo].[ConveyanceInOuts]
    ([ConveyanceId]);
GO

-- Creating foreign key on [ConveyanceInOutId] in table 'Crews'
ALTER TABLE [dbo].[Crews]
ADD CONSTRAINT [FK_ConveyanceInOutPassenger]
    FOREIGN KEY ([ConveyanceInOutId])
    REFERENCES [dbo].[ConveyanceInOuts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConveyanceInOutPassenger'
CREATE INDEX [IX_FK_ConveyanceInOutPassenger]
ON [dbo].[Crews]
    ([ConveyanceInOutId]);
GO

-- Creating foreign key on [AlienId] in table 'Crews'
ALTER TABLE [dbo].[Crews]
ADD CONSTRAINT [FK_AlienPassenger]
    FOREIGN KEY ([AlienId])
    REFERENCES [dbo].[Aliens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlienPassenger'
CREATE INDEX [IX_FK_AlienPassenger]
ON [dbo].[Crews]
    ([AlienId]);
GO

-- Creating foreign key on [EndorseId] in table 'EndorseStamps'
ALTER TABLE [dbo].[EndorseStamps]
ADD CONSTRAINT [FK_EndorseEndorseStamp]
    FOREIGN KEY ([EndorseId])
    REFERENCES [dbo].[Endorses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EndorseEndorseStamp'
CREATE INDEX [IX_FK_EndorseEndorseStamp]
ON [dbo].[EndorseStamps]
    ([EndorseId]);
GO

-- Creating foreign key on [AlienId] in table 'AddRemoveCrews'
ALTER TABLE [dbo].[AddRemoveCrews]
ADD CONSTRAINT [FK_AlienAddRemoveCrew]
    FOREIGN KEY ([AlienId])
    REFERENCES [dbo].[Aliens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlienAddRemoveCrew'
CREATE INDEX [IX_FK_AlienAddRemoveCrew]
ON [dbo].[AddRemoveCrews]
    ([AlienId]);
GO

-- Creating foreign key on [AlienId] in table 'PassportHistories'
ALTER TABLE [dbo].[PassportHistories]
ADD CONSTRAINT [FK_AlienPassportHistory]
    FOREIGN KEY ([AlienId])
    REFERENCES [dbo].[Aliens]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlienPassportHistory'
CREATE INDEX [IX_FK_AlienPassportHistory]
ON [dbo].[PassportHistories]
    ([AlienId]);
GO

-- Creating foreign key on [ConveyanceInOutId] in table 'AddRemoveCrews'
ALTER TABLE [dbo].[AddRemoveCrews]
ADD CONSTRAINT [FK_ConveyanceInOutAddRemoveCrew]
    FOREIGN KEY ([ConveyanceInOutId])
    REFERENCES [dbo].[ConveyanceInOuts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConveyanceInOutAddRemoveCrew'
CREATE INDEX [IX_FK_ConveyanceInOutAddRemoveCrew]
ON [dbo].[AddRemoveCrews]
    ([ConveyanceInOutId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------