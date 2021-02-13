
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/20/2010 21:11:20 by Amornthep L.
-- Extend and manually create unique index, MUST manually run after run the main DDL
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NeoIMOSK];
GO

-- --------------------------------------------------
-- Dropping existing Index And Then Recreate it
-- --------------------------------------------------

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_zz_References_RefTypeId_Code') 
    DROP INDEX [zz_References].[IX_zz_References_RefTypeId_Code];
GO

CREATE UNIQUE INDEX [IX_zz_References_RefTypeId_Code]
ON [dbo].[zz_References]
    ([RefTypeID],[Code]);
GO

-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_zz_References_RefTypeId_RefName') 
    DROP INDEX [zz_References].[IX_zz_References_RefTypeId_RefName];
GO

CREATE UNIQUE INDEX [IX_zz_References_RefTypeId_RefName]
ON [dbo].[zz_References]
    ([RefTypeID],[RefName]);
GO

---- --------------------------------------------------

--IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_zz_References_RefName') 
--    DROP INDEX [zz_References].[IX_zz_References_RefName];
--GO

--CREATE INDEX [IX_zz_References_RefName]
--ON [dbo].[zz_References]
--    ([RefName]);
--GO

-- Visa...
-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_VisaDetails_Code') 
    DROP INDEX [VisaDetails].[IX_VisaDetails_Code];
GO

CREATE INDEX [IX_VisaDetails_Code]
ON [dbo].[VisaDetails]
    ([Code]);
GO

-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_VisaDetails_RequestDate') 
    DROP INDEX [VisaDetails].[IX_VisaDetails_RequestDate];
GO

CREATE INDEX [IX_VisaDetails_RequestDate]
ON [dbo].[VisaDetails]
    ([RequestDate]);
GO

-- Stay...
-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Staying90Days_Code') 
    DROP INDEX [Staying90Days].[IX_Staying90Days_Code];
GO

CREATE INDEX [IX_Staying90Days_Code]
ON [dbo].[Staying90Days]
    ([Code]);
GO

-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Staying90Days_RequestDate') 
    DROP INDEX [Staying90Days].[IX_Staying90Days_RequestDate];
GO

CREATE INDEX [IX_Staying90Days_RequestDate]
ON [dbo].[Staying90Days]
    ([RequestDate]);
GO

-- Stay...
-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ReEntrys_Code') 
    DROP INDEX [ReEntrys].[IX_ReEntrys_Code];
GO

CREATE INDEX [IX_ReEntrys_Code]
ON [dbo].[ReEntrys]
    ([Code]);
GO

-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ReEntrys_RequestDate') 
    DROP INDEX [ReEntrys].[IX_ReEntrys_RequestDate];
GO

CREATE INDEX [IX_ReEntrys_RequestDate]
ON [dbo].[ReEntrys]
    ([RequestDate]);
GO

-- Conveyance
-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Conveyances_Name') 
    DROP INDEX [Conveyances].[IX_Conveyances_Name];
GO

CREATE INDEX [IX_Conveyances_Name]
ON [dbo].[Conveyances]
    ([Name]);
GO

-- ConveyanceInOut...
-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ConveyanceInOuts_Code') 
    DROP INDEX [ConveyanceInOuts].[IX_ConveyanceInOuts_Code];
GO

CREATE INDEX [IX_ConveyanceInOuts_Code]
ON [dbo].[ConveyanceInOuts]
    ([Code]);
GO

-- --------------------------------------------------
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_VisaDetails_RequestDate') 
    DROP INDEX [VisaDetails].[IX_VisaDetails_RequestDate];
GO

CREATE INDEX [IX_VisaDetails_RequestDate]
ON [dbo].[VisaDetails]
    ([RequestDate]);
GO

-- --------------------------------------------------
-- Create References
-- --------------------------------------------------
delete zz_references;
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'0','Reference of references','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'1','�������������� Visa','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'2','�������� Visa','',1,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'3','�˵ؼš�â�������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'4','�˵ؼ����� (��������)','',3,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'5','�ѭ�ҵ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'6','�ѧ��Ѵ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'7','�ӹ�˹�Ҫ���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'8','�¾�˹�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'9','���� Re-Entry','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'10','���� Re-Entry','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'11','�ӹǹ�Թ Re-Entry','',10,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'12','��Դ��.','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'13','���� (��ѡ��ѧ)','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'14','�ӹǹ�Թ (��ѡ��ѧ)','',13,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'15','������� (㹻����)','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'100','��������¡��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'101','��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'102','�������ի��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'103','�������������/�͡','',null,null,0,'auto',getdate());

--���͢����� reference ��ԧ �
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (1,'001','�������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (1,'002','�������','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'001','7 �ѹ','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'002','30 �ѹ','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'003','60 �ѹ','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'004','90 �ѹ','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'101','1 ��','',1,'002',0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (3,'001','��áԨ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (3,'002','��ͺ����','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (4,'001','������','',3,'002',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (4,'002','�������','',3,'002',0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (5,'001','����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (5,'002','���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (5,'003','����٪�','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (7,'001','Mr.','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (7,'002','Ms.','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (7,'003','Mrs.','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (8,'001','ö¹��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (8,'002','����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (8,'003','����ͧ�Թ','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (9,'001','B','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (9,'002','O','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (9,'003','LA','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (9,'004','ED','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (9,'005','OA','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (9,'006','TR','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (10,'1','S','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (10,'2','M','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (11,'1','1000','',10,'1',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (11,'2','3800','',10,'2',0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (12,'001','16','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (12,'002','17','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (13,'1','S','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (13,'2','M','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (14,'1','1900','',13,'1',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (14,'2','3800','',13,'2',0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (15,'001','�����ԭ��','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'1','��������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'2','Re-Entry','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'3','��ѡ��ѧ��蹷������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'4','��§ҹ��� 90 �ѹ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'5','��Ҿ��������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'6','�͡���������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'7','仡Ѻ���� (����)','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'8','�ҡѺ���� (Ŵ)','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (101,'M','���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (101,'F','˭ԧ','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (102,'1','TOURIST','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (102,'2','NON-IMM.','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (103,'1','���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (103,'2','�͡','',null,null,0,'auto',getdate());

-- �ѧ��Ѵ...
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'001','��к��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'002','��ا෾��ҹ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'003','�ҭ������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'004','����Թ���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'005','��ᾧྪ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'006','�͹��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'007','�ѹ�����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'008','���ԧ���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'009','�ź���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'010','��¹Ҷ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'011','�������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'012','�����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'013','��§���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'014','��§����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'015','��ѧ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'016','��Ҵ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'017','�ҡ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'018','��ù�¡','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'019','��û��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'020','��þ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'021','����Ҫ����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'022','�����ո����Ҫ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'023','������ä�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'024','�������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'025','��Ҹ����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'026','��ҹ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'027','���������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'028','�����ҹ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'029','��ШǺ���բѹ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'030','��Ҩչ����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'031','�ѵ�ҹ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'032','��й�������ظ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'033','�����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'034','�ѧ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'035','�ѷ�ا','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'036','�ԨԵ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'037','��ɳ��š','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'038','ྪú���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'039','ྪú�ó�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'040','���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'041','����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'042','�����ä��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'043','�ء�����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'044','�����ͧ�͹','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'045','��ʸ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'046','����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'047','�������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'048','�йͧ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'049','���ͧ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'050','�Ҫ����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'051','ž����  ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'052','�ӻҧ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'053','�Ӿٹ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'054','���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'055','�������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'056','ʡŹ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'057','ʧ���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'058','ʵ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'059','��طû�ҡ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'060','��ط�ʧ����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'061','��ط��Ҥ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'062','������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'063','��к���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'064','�ԧ�����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'065','��⢷��  ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'066','�ؾ�ó����','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'067','����ɮ��ҹ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'068','���Թ���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'069','˹ͧ���','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'070','˹ͧ�������','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'071','��ҧ�ͧ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'072','�ӹҨ��ԭ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'073','�شøҹ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'074','�صôԵ��','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'075','�ط�¸ҹ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'076','�غ��Ҫ�ҹ�','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'077','��ú֧���','',null,null,0,'auto',getdate());

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------