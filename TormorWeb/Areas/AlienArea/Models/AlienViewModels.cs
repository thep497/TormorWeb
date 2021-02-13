// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: WebUI
//3. ชื่อ Unit 	: CrewViewModel
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Class เก็บ ข้อมูลของ Aliens ที่จะใช้แสดงในหน้าจอ
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : เพิ่ม 2 field (ID/Seaman) PD18-540102 Req 2
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.Web.Models
{
    [MetadataType(typeof(Tormor.DomainModel.Alien.Alien_Validation))]
    public class AlienLite
    {
        public int Id { get; set; }
        public PersonName Name { get; set; }
        public IDDocument PassportCard { get; set; }
        public string IDCardNo { get; set; }
        public string SeamanCardNo { get; set; }
        public IDDocument HabitatCard { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public bool IsThai { get; set; }
        public string Nationality { get; set; }
        public Address CurrentAddress { get; set; }
        public LogInfo UpdateInfo { get; set; }
        public bool IsCancel { get; set; }
        public int? Age { get; set; } //th20110804

        public AlienLite()
        {
        }

        public AlienLite(Alien alien)
        {
            this.CopyFrom(alien);
        }

        public void CopyFrom(Alien alien)
        {
            if (alien != null)
            {
                this.Id = alien.Id;
                this.Name = alien.Name;
                this.PassportCard = alien.PassportCard;
                this.IDCardNo = alien.IDCardNo;
                this.SeamanCardNo = alien.SeamanCardNo;
                this.HabitatCard = alien.HabitatCard;
                this.DateOfBirth = alien.DateOfBirth;
                this.Age = alien.Age; //th20110804
                this.Sex = alien.Sex;
                this.IsThai = alien.IsThai;
                this.Nationality = alien.Nationality;
                this.CurrentAddress = alien.CurrentAddress;
                this.UpdateInfo = alien.UpdateInfo;
                this.IsCancel = alien.IsCancel;
            }
        }
    }

    public class AlienViewModel
    {
        public AlienViewModel()
        {
            this.Alien = new AlienLite();
        }
        public AlienViewModel(Alien alien)
        {
            this.Alien = new AlienLite();
            this.Alien.CopyFrom(alien);
        }

        public AlienLite Alien;
    }
}