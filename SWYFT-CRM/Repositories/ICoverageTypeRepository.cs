using SWYFT_CRM.Models;
using System.Collections.Generic;

namespace SWYFT_CRM.Repositories
{
    public interface ICoverageTypeRepository
    {
        void Add(CoverageType coverageType);
        void Delete(int id);
        List<CoverageType> GetAll();
        CoverageType GetById(int id);
        void Update(CoverageType coverageType);
    }
}