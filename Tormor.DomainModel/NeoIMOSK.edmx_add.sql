
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
values (0,'1','ประเภทระยะเวลา Visa','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'2','ระยะเวลา Visa','',1,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'3','เหตุผลการขออยู่ต่อ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'4','เหตุผลย่อย (ขออยู่ต่อ)','',3,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'5','สัญชาติ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'6','จังหวัด','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'7','คำนำหน้าชื่อ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'8','โดยพาหนะ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'9','รหัส Re-Entry','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'10','ครั้ง Re-Entry','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'11','จำนวนเงิน Re-Entry','',10,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'12','ชนิดตม.','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'13','ครั้ง (สลักหลัง)','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'14','จำนวนเงิน (สลักหลัง)','',13,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'15','ท่าเรือ (ในประเทศ)','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'100','ประเภทรายการ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'101','เพศ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'102','ประเภทวีซ่า','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (0,'103','ประเภทเรือเข้า/ออก','',null,null,0,'auto',getdate());

--เนื้อข้อมูล reference จริง ๆ
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (1,'001','ระยะสั้น','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (1,'002','ระยะยาว','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'001','7 วัน','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'002','30 วัน','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'003','60 วัน','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'004','90 วัน','',1,'001',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (2,'101','1 ปี','',1,'002',0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (3,'001','ธุรกิจ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (3,'002','ครอบครัว','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (4,'001','สามีไทย','',3,'002',0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (4,'002','ภรรยาไทย','',3,'002',0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (5,'001','พม่า','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (5,'002','ลาว','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (5,'003','กัมพูชา','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (7,'001','Mr.','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (7,'002','Ms.','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (7,'003','Mrs.','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (8,'001','รถยนต์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (8,'002','เรือ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (8,'003','เครื่องบิน','',null,null,0,'auto',getdate());

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
values (15,'001','รุ่งเจริญผล','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'1','ขออยู่ต่อ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'2','Re-Entry','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'3','สลักหลังถิ่นที่อยู่','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'4','รายงานตัว 90 วัน','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'5','เข้าพร้อมเรือ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'6','ออกพร้อมเรือ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'7','ไปกับเรือ (เพิ่ม)','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (100,'8','มากับเรือ (ลด)','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (101,'M','ชาย','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (101,'F','หญิง','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (102,'1','TOURIST','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (102,'2','NON-IMM.','',null,null,0,'auto',getdate());

insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (103,'1','เข้า','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (103,'2','ออก','',null,null,0,'auto',getdate());

-- จังหวัด...
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'001','กระบี่','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'002','กรุงเทพมหานคร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'003','กาญจนบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'004','กาฬสินธุ์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'005','กำแพงเพชร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'006','ขอนแก่น','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'007','จันทบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'008','ฉะเชิงเทรา','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'009','ชลบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'010','ชัยนาถ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'011','ชัยภูมิ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'012','ชุมพร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'013','เชียงราย','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'014','เชียงใหม่','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'015','ตรัง','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'016','ตราด','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'017','ตาก','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'018','นครนายก','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'019','นครปฐม','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'020','นครพนม','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'021','นครราชสีมา','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'022','นครศรีธรรมราช','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'023','นครสวรรค์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'024','นนทบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'025','นราธิวาส','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'026','น่าน','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'027','บุรีรัมย์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'028','ปทุมธานี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'029','ประจวบคีรีขันธ์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'030','ปราจีนบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'031','ปัตตานี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'032','พระนครศรีอยุธยา','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'033','พะเยา','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'034','พังงา','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'035','พัทลุง','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'036','พิจิตร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'037','พิษณุโลก','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'038','เพชรบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'039','เพชรบูรณ์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'040','แพร่','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'041','ภูเก็ต','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'042','มหาสารคาม','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'043','มุกดาหาร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'044','แม่ฮ่องสอน','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'045','ยโสธร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'046','ยะลา','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'047','ร้อยเอ็ด','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'048','ระนอง','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'049','ระยอง','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'050','ราชบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'051','ลพบุรี  ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'052','ลำปาง','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'053','ลำพูน','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'054','เลย','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'055','ศรีสะเกษ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'056','สกลนคร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'057','สงขลา','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'058','สตูล','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'059','สมุทรปราการ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'060','สมุทรสงคราม','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'061','สมุทรสาคร','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'062','สระแก้ว','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'063','สระบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'064','สิงห์บุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'065','สุโขทัย  ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'066','สุพรรณบุรี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'067','สุราษฎร์ธานี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'068','สุรินทร์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'069','หนองคาย','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'070','หนองบัวลำภู','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'071','อ่างทอง','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'072','อำนาจเจริญ','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'073','อุดรธานี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'074','อุตรดิตถ์','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'075','อุทัยธานี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'076','อุบลราชธานี','',null,null,0,'auto',getdate());
insert into zz_references (RefTypeId, Code, RefName, RefDesc, RefRefTypeId, RefCode, IsCancel, UpdateInfo_AddedBy, UpdateInfo_AddedDate)
values (6,'077','นครบึงกาฬ','',null,null,0,'auto',getdate());

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------