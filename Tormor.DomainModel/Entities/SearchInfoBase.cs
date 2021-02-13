// *******************************************************************
//1. ��������� 	: NeoSKIMO
//2. �����к� 		: DomainModel
//3. ���� Unit 	: SearchInfoBase
//4. �����¹�����	: ���෾
//5. �ѹ������ҧ	    : 201012xx
//6. �ش���ʧ��ͧ Unit ��� : Entity �红����Ū�ǧ�ѹ���㹡�ä��� �������ӵ�ʹ�����
// *******************************************************************
// ��䢤��駷�� : 0
// ����ѵԡ�����
// ���駷�� 0 : ����������ҧ unit ����
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using NNS.GeneralHelpers;

namespace Tormor.DomainModel
{
    public class SearchInfoBase
    {
        public SearchInfoBase()
        {
            dtpSelectRange = 0;
            dtpFromDate = DateTime.Today.StartOfTheMonth();
            dtpToDate = DateTime.Today;
        }

        [DisplayName("��ǡ�ͧ�ѹ���")]
        public int dtpSelectRange { get; set; }

        [DisplayName("�ѹ������-�ҡ")]
        public DateTime? dtpFromDate { get; set; }

        [DisplayName("�ѹ������-�֧")]
        public DateTime? dtpToDate { get; set; }
    }
}
