// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: ModelConst
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 20110409
//6. จุดประสงค์ของ Unit นี้ : Class เก็บค่าคงที่ใช้ในโปรแกรม
// *******************************************************************
// แก้ไขครั้งที่ : 0
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.ModelHelpers
{
    public static class ErrorMessage
    {
        public const string DatabaseError = "ปัญหาจากฐานข้อมูล :";
        public const string CodeGenError = "ขออภัย :มีปัญหาขณะสร้าง Code กรุณากด Save อีกครั้งหนึ่ง";
    }
    public static class Const
    {
        public const int TWO_BILLION = 2000000000;
    }
    public static class ModelConst
    {
        //refofref (RefTypeId)
        public const int REF_CONVINOUT = 103; //ref 103 ประเภทเรือเข้าออก

        //ref 103 ประเภทเรือเข้าออก
        public const string CONVINOUT_IN  = "1"; //ref 1 เข้า
        public const string CONVINOUT_OUT = "2"; //ref 2 ออก

        //คนเพิ่ม/ลด
        public const int ADDREMOVETYPE_NONE = 0; //ref 1 เพิ่ม
        public const int ADDREMOVETYPE_ADD = 1; //ref 1 เพิ่ม
        public const int ADDREMOVETYPE_RMV = 2; //ref 2 ลด
    }
}
