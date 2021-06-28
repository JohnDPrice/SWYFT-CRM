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
    public class LeadRepository : BaseRepository, ILeadRepository
    {
        public LeadRepository(IConfiguration configuration) : base(configuration) { }

        public List<Lead> GetAllUserLeads()
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
                    l.DateOfBirth, l.LeadStatusId, l.UserProfileId,
                    l.Client, 
                    l.CoverageTypeId,
                    l.LeadStatusId,
                    l.DateCreated,
                    l.InsuranceCompanyId,
                    u.FirstName, u.LastName,
                    u.Email, 
                    c.[Name],
                    i.[Name],
                    ls.[Name]
                FROM Lead l
                    LEFT JOIN UserProfile u ON l.UserProfileId = u.Id
                    LEFT JOIN CoverageType c on l.CoverageTypeId = c.Id
                    LEFT JOIN InsuranceCompany i on l.InsuranceCompanyId = i.Id
                    LEFT JOIN LeadStatus ls on l.LeadStatusId = ls.Id
                    WHERE UserProfileId = @Id";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Lead> leads = new List<Lead>();
                    while (reader.Read())
                    {
                        leads.Add(new Lead()
                        {
                            Id = DbUtils.GetInt(reader, "PostId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateOfBirth = DbUtils.GetDateTime(reader, "DateOfBirth"),
                            Client = DbUtils.GetBool(reader, "Client"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            LeadStatusId = DbUtils.GetInt(reader, "LeadStatusId"),
                            LeadStatus = new LeadStatus()
                            {
                                Id = DbUtils.GetInt(reader, "LeadStatusId"),
                                Name = DbUtils.GetString(reader, "Name")
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
                                Name = DbUtils.GetString(reader, "Name")
                            },
                            InsuranceCompanyId = DbUtils.GetInt(reader, "InsuranceComapanyId"),
                            InsuranceCompany = new InsuranceCompany()
                            {
                                Id = DbUtils.GetInt(reader, "InsuranceCompanyId"),
                                Name = DbUtils.GetString(reader, "Name")
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
                    l.DateOfBirth, l.LeadStatusId, l.UserProfileId,
                    l.Client, 
                    l.CoverageTypeId,
                    l.LeadStatusId,
                    l.DateCreated
                    l.InsuranceCompanyId,
                    u.FirstName, u.LastName,
                    u.Email, 
                    c.[Name],
                    i.[Name],
                    ls.[Name]
                FROM Lead l
                    LEFT JOIN UserProfile u ON l.UserProfileId = u.Id
                    LEFT JOIN CoverageType c on l.CoverageTypeId = c.Id
                    LEFT JOIN InsuranceCompany i on l.InsuranceCompanyId = i.Id
                    LEFT JOIN LeadStatus ls on l.LeadStatusId = ls.Id
                    WHERE l.Id = @Id
                    ORDER BY p.CreateDateTime";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Lead lead = null;
                    while (reader.Read())
                    {
                        lead = new Lead()
                        {
                            Id = DbUtils.GetInt(reader, "PostId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateOfBirth = DbUtils.GetDateTime(reader, "DateOfBirth"),
                            Client = DbUtils.GetBool(reader, "Client"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            LeadStatusId = DbUtils.GetInt(reader, "LeadStatusId"),
                            LeadStatus = new LeadStatus()
                            {
                                Id = DbUtils.GetInt(reader, "LeadStatusId"),
                                Name = DbUtils.GetString(reader, "Name")
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
                                Name = DbUtils.GetString(reader, "Name")
                            },
                            InsuranceCompanyId = DbUtils.GetInt(reader, "InsuranceComapanyId"),
                            InsuranceCompany = new InsuranceCompany()
                            {
                                Id = DbUtils.GetInt(reader, "InsuranceCompanyId"),
                                Name = DbUtils.GetString(reader, "Name")
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
                        INSERT INTO Lead (FirstName, LastName, Email, DateCreated, DateOfBirth, Client, UserProfileId, LeadStatusId, CoverageTypeId, InsuranceCompanyId)
                        OUTPUT INSERTED.ID
                        VALUES (@FirstName, @LastName, @Email, @DateCreated, @DateOfBirth, @Client, @UserProfileId, @LeadStatusId, @CoverageTypeId, @InsuranceCompanyId)";

                    DbUtils.AddParameter(cmd, "@FirstName", lead.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", lead.LastName);
                    DbUtils.AddParameter(cmd, "@Email", lead.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", lead.DateCreated);
                    DbUtils.AddParameter(cmd, "@DateOfBirth", lead.DateOfBirth);
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
                                DateCreated = @publishDateTime,
                                DateOfBirth = @dateOfBirth,
                                CoverageTypeId = @coverageTypeId,
                                InsuranceCompanyId = @insuranceCompanyId,
                                Client = @client,
                                LeadStatusId = @leadStatusId
                                
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@title", lead.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", lead.LastName);
                    cmd.Parameters.AddWithValue("@Email", lead.Email);
                    cmd.Parameters.AddWithValue("@publishDateTime", lead.DateCreated);
                    cmd.Parameters.AddWithValue("@dateOfBirth", lead.DateOfBirth);
                    cmd.Parameters.AddWithValue("@coverageTypeId", lead.CoverageTypeId);
                    cmd.Parameters.AddWithValue("@insuranceCompanyId", lead.InsuranceCompanyId);
                    cmd.Parameters.AddWithValue("@client", lead.Client);
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

        //public List<Lead> GetUserPosts(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        SELECT p.Id as PostId, p.Title, p.Content, p.CreateDateTime as PostCreateDate, p.PublishDateTime as PostPublishDate, p.IsApproved, p.ImageLocation as PostImageLocation, u.Id as UserId, FireBaseUserId, DisplayName, FirstName, LastName, Email, u.CreateDateTime as UserCreateDateTime, u.ImageLocation as UserImageLocation, UserTypeId, c.Id as CategoryId, Name
        //            FROM Post p JOIN Category c on p.CategoryId = c.Id
        //            JOIN UserProfile u on p.UserProfileId = u.Id
        //            WHERE UserProfileId = @Id
        //            ORDER BY p.CreateDateTime";
        //            DbUtils.AddParameter(cmd, "@Id", id);
        //            var reader = cmd.ExecuteReader();
        //            var posts = new List<Lead>();
        //            while (reader.Read())
        //            {
        //                posts.Add(new Lead()
        //                {
        //                    Id = DbUtils.GetInt(reader, "PostId"),
        //                    Title = DbUtils.GetString(reader, "Title"),
        //                    Content = DbUtils.GetString(reader, "Content"),
        //                    CreateDateTime = DbUtils.GetDateTime(reader, "PostCreateDate"),
        //                    PublishDateTime = DbUtils.GetDateTime(reader, "PostPublishDate"),
        //                    ImageLocation = DbUtils.GetString(reader, "PostImageLocation"),
        //                    UserProfile = new UserProfile()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "UserId"),
        //                        DisplayName = DbUtils.GetString(reader, "DisplayName"),
        //                        FirstName = DbUtils.GetString(reader, "FirstName"),
        //                        LastName = DbUtils.GetString(reader, "LastName"),
        //                        Email = DbUtils.GetString(reader, "Email"),
        //                        CreateDateTime = DbUtils.GetDateTime(reader, "UserCreateDateTime"),
        //                        ImageLocation = DbUtils.GetString(reader, "UserImageLocation"),
        //                        UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
        //                    },
        //                    Category = new Category()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "CategoryId"),
        //                        Name = DbUtils.GetString(reader, "Name"),
        //                    }
        //                });
        //            }
        //            reader.Close();
        //            return posts;
        //        }
        //    }
        //}
    }
}