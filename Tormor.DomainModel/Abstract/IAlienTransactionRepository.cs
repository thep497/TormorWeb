using System;
using System.Collections.Generic;

namespace Tormor.DomainModel
{
    public interface IAlienTransactionRepository
    {
        string CurrentUserName { get; set; }
        IList<global::Tormor.DomainModel.AlienTransaction> FindAll(AlienSearchInfo alienSearch);
        global::Tormor.DomainModel.AlienTransaction GetOne(string ttype, int id);
        global::Tormor.DomainModel.AlienTransaction GetOneByCode(string ttype, string code, DateTime visaYear, int id = -1);
    }
}
