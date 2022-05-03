using BloodDonor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BloodDonor.Repository.Common;
using BloodDonor.Common;

namespace BloodDonor.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        static string connectionString = @"Data Source=LEGA-MEGA\SQLEXPRESS;Initial Catalog = BloodDonorsDB;Integrated Security = True";

        public async Task<List<DoctorModel>> GetDoctorAsync(StringFiltering filter, Sorting sorting, Paging paging)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<DoctorModel> doctorModel = new List<DoctorModel>();

            using (connection)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("SELECT * FROM Doctor where IsDeleted = 0 ");


                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.FirstName))
                    { stringBuilder.Append($" and FirstName = '{filter.FirstName}' "); }
                    if (!string.IsNullOrWhiteSpace(filter.LastName))
                    { stringBuilder.Append($"and LastName = '{filter.LastName}' "); }
                    if (!string.IsNullOrWhiteSpace(filter.Specialization))
                    { stringBuilder.Append($"and Specilization = '{filter.Specialization}' "); }
                    if (filter.Licence != 0)
                    { stringBuilder.Append($"and Licence = '{filter.Licence}' "); }
                }

                if (sorting != null)
                {
                    if (!string.IsNullOrWhiteSpace(sorting.SortBy))
                    { stringBuilder.Append($" ORDER BY {sorting.SortBy} "); }

                    if (!string.IsNullOrWhiteSpace(sorting.SortOrder))
                    { stringBuilder.Append($" {sorting.SortOrder}"); }
                }
                else
                {
                    Sorting sort = new Sorting();
                    stringBuilder.Append($"ORDER BY {sort.SortBy} {sort.SortOrder}");
                }

                if (paging != null)
                {
                    stringBuilder.Append($" OFFSET({paging.PageNumber} - 1) * {paging.ItemsByPage} ROWS FETCH  NEXT {paging.ItemsByPage} ROWS ONLY ");
                }
                else
                {
                    Paging page = new Paging();
                    stringBuilder.Append($" OFFSET({page.PageNumber} - 1) * {page.ItemsByPage } " +
                        $"ROWS FETCH  NEXT {page.ItemsByPage } ROWS ONLY ");
                }


                SqlCommand command = new SqlCommand(stringBuilder.ToString(), connection);
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DoctorModel model = new DoctorModel();
                        model.Licence = (int)reader["Licence"];
                        model.FirstName = (string)reader["FirstName"];
                        model.LastName = (string)reader["LastName"];
                        model.Specialization = (string)reader["Specialization"];

                        doctorModel.Add(model);
                    }
                    connection.Close();
                    reader.Close();
                }
                return doctorModel;
            }
        }
        public async Task<List<DoctorModel>> GetDoctorLNAsync(string lastname)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<DoctorModel> doctorModel = new List<DoctorModel>();

            using (connection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Doctor where Doctor.LastName = '{lastname}' and IsDeleted = 0", connection);
                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DoctorModel model = new DoctorModel();
                        model.Licence = (int)reader["Licence"];
                        model.FirstName = (string)reader["FirstName"];
                        model.LastName = (string)reader["LastName"];
                        model.Specialization = (string)reader["Specialization"];

                        doctorModel.Add(model);
                    }
                    connection.Close();
                    reader.Close();
                }

            }
            return doctorModel;
        }
        public async Task< List<DoctorModel>> GetDoctorByLidAsync(int lid)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<DoctorModel> doctorModel = new List<DoctorModel>();

            using (connection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Doctor where Doctor.Licence = {lid} and IsDeleted = 0", connection);
                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DoctorModel model = new DoctorModel();
                        model.Licence = (int)reader["Licence"];
                        model.FirstName = (string)reader["FirstName"];
                        model.LastName = (string)reader["LastName"];
                        model.Specialization = (string)reader["Specialization"];

                        doctorModel.Add(model);
                    }
                    connection.Close();
                    reader.Close();
                }

            }
            return doctorModel;
        }
        public async Task InsertDoctorAsync(DoctorModel doctorModel)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                await connection.OpenAsync();

                string newDoctor = $"Insert into Doctor(FirstName,LastName,Licence,Specialization) values " +
                    $"('{doctorModel.FirstName}'," +
                    $"'{doctorModel.LastName}'," +
                    $"'{doctorModel.Licence}'," +
                    $"'{doctorModel.Specialization}')";
            
                         
                adapter.InsertCommand = new SqlCommand(newDoctor, connection);
                await adapter.InsertCommand.ExecuteNonQueryAsync();
                connection.Close();
            }

        }
        public async Task ChangeDoctorAsync(int lid,DoctorModel doctorModel)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                DoctorModel currentModel = new DoctorModel();
                await connection.OpenAsync();
                string command = $"Update Doctor set FirstName = @FirstName, LastName = @LastName,Specialization = @Specialization where Licence= '{lid}'";
                
                SqlCommand sqlCommand = new SqlCommand(command,connection);

                if (!string.IsNullOrWhiteSpace(doctorModel.FirstName))
                {
                    sqlCommand.Parameters.AddWithValue("@FirstName", doctorModel.FirstName);
                }
                if (!string.IsNullOrWhiteSpace(doctorModel.LastName))
                {
                    sqlCommand.Parameters.AddWithValue("@LastName", doctorModel.LastName);
                }
                if (!string.IsNullOrWhiteSpace(doctorModel.Specialization))
                {
                    sqlCommand.Parameters.AddWithValue("@Specialization", doctorModel.Specialization);
                }

                adapter.UpdateCommand = sqlCommand;
                    await adapter.UpdateCommand.ExecuteNonQueryAsync();
                    connection.Close();
            }

        }
        public async Task<bool> DeleteDoctorAsync(int lid)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            bool lidCheck = false;

            using (connection)
            {
                DoctorModel currentModel = new DoctorModel();
                SqlCommand command1 = new SqlCommand($"SELECT * FROM Doctor Where Doctor.Licence = {lid};", connection);
                await connection.OpenAsync();                
                SqlDataReader reader = await command1.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    string update = $"update Doctor set IsDeleted= 1 Where Doctor.Licence = {lid}";
                    reader.Close();
                    adapter.UpdateCommand = new SqlCommand(update, connection);
                    await adapter.UpdateCommand.ExecuteNonQueryAsync();
                    lidCheck = true;
                    connection.Close();
                }
            }
            return lidCheck;
        }
    }
}
