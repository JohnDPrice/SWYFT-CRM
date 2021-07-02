using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWYFT_CRM.Models;
using SWYFT_CRM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWYFT_CRM.Repositories
{
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(IConfiguration configuration) : base(configuration) { }

        public List<Lead> GetAllUserClients(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT l.Id AS LeadId, 
                    l.FirstName, l.LastName, 
                    l.Email,
                    l.LeadStatusId, l.UserProfileId,
                    l.Client,
                    l.Sold,
                    l.SoldDate,
                    l.CoverageTypeId,
                    l.LeadStatusId,
                    l.DateCreated,
                    l.InsuranceCompanyId,
                    u.FirstName, 
                    u.LastName,
                    u.CreateDateTime as UserProfileDateCreated,
                    u.Email, 
                    c.[Name] as CoverageTypeName,
                    i.[Name] as InsuranceCompanyName,
                    ls.[Name] as LeadStatusName
                FROM Lead l
                    LEFT JOIN UserProfile u ON l.UserProfileId = u.Id
                    LEFT JOIN CoverageType c on l.CoverageTypeId = c.Id
                    LEFT JOIN InsuranceCompany i on l.InsuranceCompanyId = i.Id
                    LEFT JOIN LeadStatus ls on l.LeadStatusId = ls.Id
                    WHERE UserProfileId = @Id
                    AND Client = 1";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Lead> leads = new List<Lead>();
                    while (reader.Read())
                    {
                        leads.Add(new Lead()
                        {
                            Id = DbUtils.GetInt(reader, "LeadId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Client = DbUtils.GetBool(reader, "Client"),
                            Sold = DbUtils.GetNullableBool(reader, "Sold"),
                            SoldDate = DbUtils.GetNullableDateTime(reader, "SoldDate"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            LeadStatusId = DbUtils.GetInt(reader, "LeadStatusId"),
                            LeadStatus = new LeadStatus()
                            {
                                Id = DbUtils.GetInt(reader, "LeadStatusId"),
                                Name = DbUtils.GetString(reader, "LeadStatusName")
                            },
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "UserProfileDateCreated")
                            },
                            CoverageTypeId = DbUtils.GetInt(reader, "CoverageTypeId"),
                            CoverageType = new CoverageType()
                            {
                                Id = DbUtils.GetInt(reader, "CoverageTypeId"),
                                Name = DbUtils.GetString(reader, "CoverageTypeName")
                            },
                            InsuranceCompanyId = DbUtils.GetInt(reader, "InsuranceCompanyId"),
                            InsuranceCompany = new InsuranceCompany()
                            {
                                Id = DbUtils.GetInt(reader, "InsuranceCompanyId"),
                                Name = DbUtils.GetString(reader, "InsuranceCompanyName")
                            }
                        });
                    }
                    reader.Close();
                    return leads;
                }
            }
        }

        public Lead GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT l.Id AS LeadId, 
                    l.FirstName, l.LastName, 
                    l.Email,
                    l.LeadStatusId, l.UserProfileId,
                    l.Client, 
                    l.Sold,
                    l.SoldDate,
                    l.CoverageTypeId,
                    l.LeadStatusId,
                    l.DateCreated,
                    l.InsuranceCompanyId,
                    u.FirstName, u.LastName,
                    u.Email, 
                    c.[Name] as CoverageTypeName,
                    i.[Name] as InsuranceCompanyName,
                    ls.[Name] as LeadStatusName
                FROM Lead l
                    LEFT JOIN UserProfile u ON l.UserProfileId = u.Id
                    LEFT JOIN CoverageType c on l.CoverageTypeId = c.Id
                    LEFT JOIN InsuranceCompany i on l.InsuranceCompanyId = i.Id
                    LEFT JOIN LeadStatus ls on l.LeadStatusId = ls.Id
                    WHERE l.Id = @Id AND Client = 1";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Lead lead = null;
                    while (reader.Read())
                    {
                        lead = new Lead()
                        {
                            Id = DbUtils.GetInt(reader, "LeadId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Client = DbUtils.GetBool(reader, "Client"),
                            Sold = DbUtils.GetNullableBool(reader, "Sold"),
                            SoldDate = DbUtils.GetNullableDateTime(reader, "SoldDate"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            LeadStatusId = DbUtils.GetInt(reader, "LeadStatusId"),
                            LeadStatus = new LeadStatus()
                            {
                                Id = DbUtils.GetInt(reader, "LeadStatusId"),
                                Name = DbUtils.GetString(reader, "LeadStatusName")
                            },
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Email = DbUtils.GetString(reader, "Email"),
                            },
                            CoverageTypeId = DbUtils.GetInt(reader, "CoverageTypeId"),
                            CoverageType = new CoverageType()
                            {
                                Id = DbUtils.GetInt(reader, "CoverageTypeId"),
                                Name = DbUtils.GetString(reader, "CoverageTypeName")
                            },
                            InsuranceCompanyId = DbUtils.GetInt(reader, "InsuranceCompanyId"),
                            InsuranceCompany = new InsuranceCompany()
                            {
                                Id = DbUtils.GetInt(reader, "InsuranceCompanyId"),
                                Name = DbUtils.GetString(reader, "InsuranceCompanyName")
                            }
                        };
                    }
                    reader.Close();

                    return lead;
                }
            }
        }

        public Lead Add(Lead lead)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Lead (FirstName, LastName, Email, DateCreated, Client, UserProfileId, LeadStatusId, CoverageTypeId, InsuranceCompanyId)
                        OUTPUT INSERTED.ID
                        VALUES (@FirstName, @LastName, @Email, @DateCreated, @DateOfBirth, @Client, @UserProfileId, @LeadStatusId, @CoverageTypeId, @InsuranceCompanyId)";

                    DbUtils.AddParameter(cmd, "@FirstName", lead.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", lead.LastName);
                    DbUtils.AddParameter(cmd, "@Email", lead.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", lead.DateCreated);
                    DbUtils.AddParameter(cmd, "@Client", lead.Client);
                    DbUtils.AddParameter(cmd, "@UserProfileId", lead.UserProfileId);
                    DbUtils.AddParameter(cmd, "@LeadStatusId", lead.LeadStatusId);
                    DbUtils.AddParameter(cmd, "@CoverageTypeId", lead.CoverageTypeId);
                    DbUtils.AddParameter(cmd, "@InsuranceCompanyId", lead.InsuranceCompanyId);

                    lead.Id = (int)cmd.ExecuteScalar();
                }
            }

            return lead;
        }

        public Lead UpdateLead(Lead lead)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Lead
                            SET 
                                FirstName = @firstName,
                                LastName = @lastName,
                                Email = @Email,
                                DateCreated = @publishDateTime,,
                                CoverageTypeId = @coverageTypeId,
                                InsuranceCompanyId = @insuranceCompanyId,
                                Client = @client,
                                Sold = @sold,
                                SoldDate = @soldDate,
                                LeadStatusId = @leadStatusId
                                
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@firstName", lead.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", lead.LastName);
                    cmd.Parameters.AddWithValue("@Email", lead.Email);
                    cmd.Parameters.AddWithValue("@publishDateTime", lead.DateCreated);
                    cmd.Parameters.AddWithValue("@coverageTypeId", lead.CoverageTypeId);
                    cmd.Parameters.AddWithValue("@insuranceCompanyId", lead.InsuranceCompanyId);
                    cmd.Parameters.AddWithValue("@client", lead.Client);
                    cmd.Parameters.AddWithValue("@sold", lead.Sold);
                    cmd.Parameters.AddWithValue("@soldDate", lead.SoldDate);
                    cmd.Parameters.AddWithValue("@leadStatusId", lead.LeadStatusId);
                    cmd.Parameters.AddWithValue("@id", lead.Id);

                    cmd.ExecuteNonQuery();
                }
            }

            return lead;
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Lead WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
