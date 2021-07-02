using SWYFT_CRM.Models;
using System.Collections.Generic;

namespace SWYFT_CRM.Repositories
{
    public interface ILeadStatusRepository
    {
        void Add(LeadStatus leadStatus);
        void Delete(int id);
        List<LeadStatus> GetAll();
        LeadStatus GetById(int id);
        void Update(LeadStatus leadStatus);
    }
}