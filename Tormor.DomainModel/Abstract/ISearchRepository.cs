using System;
namespace Tormor.DomainModel
{
    public interface ISearchRepository
    {
        Tormor.DomainModel.AlienSearchInfo AlienSearch { get; set; }
        Tormor.DomainModel.ConveyanceSearchInfo ConveyanceSearch { get; set; }
    }
}
