using SWYFT_CRM.Models;
using System.Collections.Generic;

namespace SWYFT_CRM.Repositories
{
    public interface IClientRepository
    {
        Lead Add(Lead lead);
        void Delete(int id);
        List<Lead> GetAllUserClients(int id);
        Lead GetById(int id);
        Lead UpdateLead(Lead lead);
    }
}