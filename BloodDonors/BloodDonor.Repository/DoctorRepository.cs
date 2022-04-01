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

        public async Task<List<DoctorModel>> GetDoctorAsync(StringFiltering filter, Sorting sorting, Pageing pageing)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<DoctorModel> doctorModel = new List<DoctorModel>();

            using (connection)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("SELECT * FROM Doctor ");


                if (filter != null)
                {
                    stringBuilder.Append("where 1 = 1");
                    if (!string.IsNullOrWhiteSpace(filter.FirstName))
                    { stringBuilder.Append($" and FirstName = '{filter.FirstName}' "); }
                    if (!string.IsNullOrWhiteSpace(filter.LastName))
                    { stringBuilder.Append($"and LastName = '{filter.LastName}' "); }
                    if (!string.IsNullOrWhiteSpace(filter.Specialization))
                    { stringBuilder.Append($"and Specilization = '{filter.Specialization}' "); }
                    if (filter.Licence != 0)
                    { stringBuilder.Append($"and Licence = '{filter.Licence}' "); }
                }

               //stringBuilder = stringBuilder.Append(filter);

                if (sorting != null)
                {
                    if (!string.IsNullOrWhiteSpace(sorting.SortBy))
                    { stringBuilder.Append($" ORDER BY {sorting.SortBy} "); }

                    if (!string.IsNullOrWhiteSpace(sorting.SortOrder))
                    { stringBuilder.Append($" {sorting.SortOrder}"); }
                }

                //stringBuilder.Append(sorting);

                if (pageing != null)
                {
                    stringBuilder.Append($" OFFSET({pageing.PageNumber} - 1) * {pageing.ItemsByPage} ROWS FETCH  NEXT {pageing.ItemsByPage} ROWS ONLY ");
                }

                //stringBuilder.Append(pageing);

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
                SqlCommand command = new SqlCommand($"SELECT * FROM Doctor where Doctor.LastName = '{lastname}'", connection);
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
                SqlCommand command = new SqlCommand($"SELECT * FROM Doctor where Doctor.Licence = {lid}", connection);
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
                SqlCommand command = new SqlCommand($"SELECT * FROM Doctor Where Doctor.Licence = {lid};", connection);

                await connection.OpenAsync(); 
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        currentModel.FirstName = (string)reader["FirstName"];
                        currentModel.LastName = (string)reader["LastName"];
                        currentModel.Specialization = (string)reader["Specialization"];
                    }
                    if (doctorModel.FirstName != null)
                    { currentModel.FirstName = doctorModel.FirstName; }
                    else
                    { doctorModel.FirstName = currentModel.FirstName; };
                    if (doctorModel.LastName != null)
                    { currentModel.LastName = doctorModel.LastName; }
                    else
                    { doctorModel.LastName = currentModel.LastName; };
                    if (doctorModel.Specialization != null)
                    { currentModel.Specialization = doctorModel.Specialization; }
                    else
                    { doctorModel.Specialization = currentModel.Specialization; };


                    string upgrade = $"Update Donor set FirstName = '{doctorModel.FirstName}'," +
                        $"LastName = '{doctorModel.LastName}', " +
                        $"Specialization = '{doctorModel.Specialization}' Where Doctor.Licence = {lid};";


                    adapter.UpdateCommand = new SqlCommand(upgrade, connection);
                    reader.Close();
                    await adapter.UpdateCommand.ExecuteNonQueryAsync();
                    connection.Close();
           
                }
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
                    SqlCommand command2 = new SqlCommand($"DELETE FROM Doctor Where Doctor.Licence = {lid};", connection);
                    reader.Close();
                    adapter.DeleteCommand = command2;
                    await adapter.DeleteCommand.ExecuteNonQueryAsync();
                    lidCheck = true;
                    connection.Close();
                }
            }
            return lidCheck;
        }
    }
}
