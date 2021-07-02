using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SWYFT_CRM.Utils;
using SWYFT_CRM.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace SWYFT_CRM.Repositories
{
    public class LeadStatusRepository : BaseRepository, ILeadStatusRepository
    {
        public LeadStatusRepository(IConfiguration configuration) : base(configuration) { }

        public List<LeadStatus> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  Id,
                                [Name]
                        FROM LeadStatus
                    ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<LeadStatus> leadstatuses = new List<LeadStatus>();

                    while (reader.Read())
                    {
                        leadstatuses.Add(new LeadStatus()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        });
                    }
                    reader.Close();
                    return leadstatuses;
                }
            }
        }

        public LeadStatus GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT [Name]
                        FROM LeadStatus
                        WHERE Id = @Id;
                    ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    LeadStatus laedStatus = null;
                    while (reader.Read())
                    {
                        laedStatus = new LeadStatus()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                        };
                    }
                    reader.Close();

                    return laedStatus;
                }
            }
        }

        public void Add(LeadStatus leadStatus)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO LeadStatus (Name)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name)";
                    DbUtils.AddParameter(cmd, "@Name", leadStatus.Name);

                    leadStatus.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        //Changes any tag's category associated with the targeted category to avoid deleting posts
        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM LeadStatus
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(LeadStatus leadStatus)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE LeadStatus
                        SET [Name] = @name
                        WHERE Id = @Id
                    ";
                    DbUtils.AddParameter(cmd, "@Id", leadStatus.Id);
                    DbUtils.AddParameter(cmd, "@name", leadStatus.Name);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}