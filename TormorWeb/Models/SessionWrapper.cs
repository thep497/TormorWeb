// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: Web Model
//3. ชื่อ Unit 	: SessionWrapper
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Model เพื่อเก็บข้อมูลเอาไว้ใน Session ถ้ามี Class ไหน Inherite จาก class นี้
//                          class ที่ inherite จะมี static member ชื่อ Current เป็น session อัตโนมัติ
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
    public class SessionWrapper<T>
        where T : SessionWrapper<T>, new()
    {
        private static string Key
        {
            get { return typeof(SessionWrapper<T>).FullName; }
        }

        private static T Value
        {
            get { return (T)HttpContext.Current.Session[Key]; }
            set { HttpContext.Current.Session[Key] = value; }
        }

        public static T Current
        {
            get
            {
                var instance = Value;
                if (instance == null)
                    lock (typeof(T)) // not ideal to lock on a type -- but it'll work
                    {
                        // standard lock double-check
                        instance = Value;
                        if (instance == null)
                            Value = instance = new T();
                    }
                return instance;
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session[Key] = null;
        }
    }
}