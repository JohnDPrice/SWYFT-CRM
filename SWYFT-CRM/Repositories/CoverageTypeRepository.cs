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
    public class CoverageTypeRepository : BaseRepository, ICoverageTypeRepository
    {
        public CoverageTypeRepository(IConfiguration configuration) : base(configuration) { }

        public List<CoverageType> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  Id,
                                [Name]
                        FROM CoverageType
                    ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<CoverageType> coverageTypes = new List<CoverageType>();

                    while (reader.Read())
                    {
                        coverageTypes.Add(new CoverageType()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        });
                    }
                    reader.Close();
                    return coverageTypes;
                }
            }
        }

        public CoverageType GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT [Name]
                        FROM CoverageType
                        WHERE Id = @Id;
                    ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    CoverageType coverageTypes = null;
                    while (reader.Read())
                    {
                        coverageTypes = new CoverageType()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                        };
                    }
                    reader.Close();

                    return coverageTypes;
                }
            }
        }

        public void Add(CoverageType coverageType)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO CoverageType (Name)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name)";
                    DbUtils.AddParameter(cmd, "@Name", coverageType.Name);

                    coverageType.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = @"DELETE FROM CoverageType
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(CoverageType coverageType)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE CoverageType
                        SET [Name] = @name
                        WHERE Id = @Id
                    ";
                    DbUtils.AddParameter(cmd, "@Id", coverageType.Id);
                    DbUtils.AddParameter(cmd, "@name", coverageType.Name);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}