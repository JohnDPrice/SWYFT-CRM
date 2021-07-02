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
    public class AppointmentRepository : BaseRepository, IAppointmentRepository
    {
        public AppointmentRepository(IConfiguration configuration) : base(configuration) { }

        public List<Appointment> GetAllUserAppointments(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  a.Id, a.Title, a.[Start], a.[End], a.Notes, a.LeadId as LeadId, a.UserProfileId, l.firstName as LeadFirstName, l.lastName AS LeadLastName, u.FirstName as UserFirstName, u.LastName as UserLastName
                        FROM Appointment a
                        LEFT JOIN Lead l on a.LeadId = l.Id
                        LEFT JOIN UserProfile u on a.UserProfileId = u.Id
                        WHERE a.UserProfileId = @Id
                    ";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Appointment> appointments = new List<Appointment>();

                    while (reader.Read())
                    {
                        appointments.Add(new Appointment()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            Start = DbUtils.GetNullableDateTime(reader, "Start"),
                            End = DbUtils.GetNullableDateTime(reader, "End"),
                            Notes = DbUtils.GetString(reader, "Notes"),
                            LeadId = DbUtils.GetInt(reader, "LeadId"),
                            Lead = new Lead()
                            {
                                Id = DbUtils.GetInt(reader, "LeadId"),
                                FirstName = DbUtils.GetString(reader, "LeadFirstName"),
                                LastName = DbUtils.GetString(reader, "LeadLastName")
                            },
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                FirstName = DbUtils.GetString(reader, "UserFirstName"),
                                LastName = DbUtils.GetString(reader, "UserLastName")
                            }
                        });
                    }
                    reader.Close();
                    return appointments;
                }
            }
        }

        public Appointment GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  a.Id, a.Title, a.[Start], a.[End], a.Notes, a.LeadId as LeadId, a.UserProfileId AS UserProfileId, l.firstName as LeadFirstName, l.lastName AS LeadLastName, u.LastName
                        FROM Appointment a
                        LEFT JOIN Lead l on a.LeadId = l.Id
                        LEFT JOIN UserProfile u on a.UserProfileId = u.Id
                        WHERE Id = @Id;
                    ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    Appointment appointment = null;
                    while (reader.Read())
                    {
                        appointment = new Appointment()
                        {
                            Id = id,
                            Title = DbUtils.GetString(reader, "Title"),
                            Start = DbUtils.GetNullableDateTime(reader, "Start"),
                            End = DbUtils.GetNullableDateTime(reader, "End"),
                            Notes = DbUtils.GetString(reader, "Notes"),
                            LeadId = DbUtils.GetInt(reader, "LeadId"),
                            Lead = new Lead()
                            {
                                Id = DbUtils.GetInt(reader, "LeadId"),
                                FirstName = DbUtils.GetString(reader, "LeadFirstName"),
                                LastName = DbUtils.GetString(reader, "LeadLastName")
                            },
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName")
                            }
                        };
                    }
                    reader.Close();

                    return appointment;
                }
            }
        }

        public void Add(Appointment appointment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Appointment Title, (Start), (End), Allday, Notes, LeadId, UserProfileId
                                        OUTPUT INSERTED.ID
                                        VALUES @Title, (@Start), (@End), @AllDay, @Notes, @LeadId, @UserProfileId";
                    DbUtils.AddParameter(cmd, "@Title", appointment.Title);
                    DbUtils.AddParameter(cmd, "@Start", appointment.Start);
                    DbUtils.AddParameter(cmd, "@End", appointment.End);
                    DbUtils.AddParameter(cmd, "@AllDay", appointment.AllDay);
                    DbUtils.AddParameter(cmd, "@Notes", appointment.Notes);
                    DbUtils.AddParameter(cmd, "@LeadId", appointment.LeadId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", appointment.UserProfileId);

                    appointment.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = @"DELETE FROM Appointment
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Appointment appointment)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Appointment
                        SET 
                        Title = @title,
                        [Start] = @start,
                        [End] = @end,
                        AllDay = @allDay,
                        Notes = @notes,
                        LeadId = @leadId,
                        UserProfileId = @userProfileId
                        WHERE Id = @Id
                    ";
                    DbUtils.AddParameter(cmd, "@Id", appointment.Id);
                    DbUtils.AddParameter(cmd, "@title", appointment.Title);
                    DbUtils.AddParameter(cmd, "@start", appointment.Start);
                    DbUtils.AddParameter(cmd, "@end", appointment.End);
                    DbUtils.AddParameter(cmd, "@allDay", appointment.AllDay);
                    DbUtils.AddParameter(cmd, "@notes", appointment.Notes);
                    DbUtils.AddParameter(cmd, "@leadId", appointment.LeadId);
                    DbUtils.AddParameter(cmd, "@userProfileId", appointment.UserProfileId);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}