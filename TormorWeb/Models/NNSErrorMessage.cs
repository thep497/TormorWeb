// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: Web Model
//3. ชื่อ Unit 	: NNSErrorMessage
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Model เก็บและแสดง Error Message
// *******************************************************************
// แก้ไขครั้งที่ : 0
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tormor.Web.Models
{
    public class NNSErrorMessage
    {
        public NNSErrorMessage(string source, string msg)
        {
            Source = source;
            Message = msg;
        }

        public string Source { get; set; }
        public string Message { get; set; }

        public string ShowMessage { get { return String.Format("<h2>{0}</h2><br>{1}", Source, Message); } }
    }
}