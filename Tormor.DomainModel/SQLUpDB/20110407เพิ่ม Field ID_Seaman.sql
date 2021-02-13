--th20110407 เพิ่ม field ตาม PD18-540102 Req 2
alter table Crews Add IDCardNo nvarchar(MAX) NULL;
alter table Crews Add SeamanCardNo nvarchar(MAX) NULL;
GO

alter table Aliens Add IDCardNo nvarchar(MAX) NULL;
alter table Aliens Add SeamanCardNo nvarchar(MAX) NULL;
GO

