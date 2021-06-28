using SWYFT_CRM.Models;
using System.Collections.Generic;

namespace SWYFT_CRM.Repositories
{
    public interface ILeadRepository
    {
        Lead Add(Lead lead);
        void Delete(int id);
        List<Lead> GetAllUserLeads();
        Lead GetById(int id);
        Lead UpdateLead(Lead lead);
    }
}