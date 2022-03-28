using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BloodDonor.Model;
using BloodDonor.Repository.Common;

namespace BloodDonor.Repository
{
    public class DonorRepository : IDonorRepository
    {
        static string connectionString = @"Data Source=LEGA-MEGA\SQLEXPRESS;Initial Catalog = BloodDonorsDB;Integrated Security = True";
        //List<DonorModel> donorModels = new List<DonorModel>();
        public async Task<List<DonorModel>> GetDonorAsync()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<DonorModel> donorModels = new List<DonorModel>();
            using (connection)
            {
                DonorModel donorModel = new DonorModel();
                SqlCommand command = new SqlCommand("SELECT * FROM Donor;", connection);
                await connection.OpenAsync();


               SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DonorModel model = new DonorModel();
                        model.Id = (int)reader["Id"];
                        model.FirstName = (string)reader["FirstName"];
                        model.LastName = (string)reader["LastName"];
                        model.Email = (string)reader["Email"];
                        model.DonationNumber = (int)reader["DonNumber"];
                        model.ReferentCode = (Guid)reader["ReferentCode"];

                        donorModels.Add(model);
                    }
                    connection.Close();
                    reader.Close();
                }
            }
            return donorModels;
        }
        public async Task<List<DonorModel>> GetDonorByIdAsync(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<DonorModel> donorModels = new List<DonorModel>();
            using (connection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Donor Where Donor.Id = '{id}';", connection);
                await connection.OpenAsync();
                SqlDataReader reader = await  command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DonorModel model = new DonorModel();
                        model.Id = (int)reader["Id"];
                        model.FirstName = (string)reader["FirstName"];
                        model.LastName = (string)reader["LastName"];
                        model.Email = (string)reader["Email"];
                        model.DonationNumber = (int)reader["DonNumber"];
                        model.ReferentCode = (Guid)reader["ReferentCode"];

                        donorModels.Add(model);
                    }
                    connection.Close();
                    reader.Close();
                }
            }
            return donorModels;
        }
        public async Task IncludeDonorAsync(DonorModel donorModel)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                await connection.OpenAsync();
                string newDonor = $"Insert into Donor(FirstName,LastName,Email,DonNumber,ReferentCode) values " +
                    $"('{donorModel.FirstName}'," +
                    $"'{donorModel.LastName}'," +
                    $"'{donorModel.Email}'," +
                    $"'{donorModel.DonationNumber}'," +
                    $"'{donorModel.ReferentCode}')";

                adapter.InsertCommand = new SqlCommand(newDonor, connection);
                await adapter.InsertCommand.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
        public async Task ChangeDonorByIdAsync(int id, DonorModel upgradedDonor)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                DonorModel currentModel = new DonorModel();
                SqlCommand command = new SqlCommand($"SELECT * FROM Donor Where Donor.Id = '{id}';", connection);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        currentModel.FirstName = (string)reader["FirstName"];
                        currentModel.LastName = (string)reader["LastName"];
                        currentModel.Email = (string)reader["Email"];
                        currentModel.DonationNumber = (int)reader["DonNumber"];
                    }
                    if (upgradedDonor.FirstName != null)
                    { currentModel.FirstName = upgradedDonor.FirstName; }
                    else
                    { upgradedDonor.FirstName = currentModel.FirstName; };
                    if (upgradedDonor.LastName != null)
                    { currentModel.LastName = upgradedDonor.LastName; }
                    else
                    { upgradedDonor.LastName = currentModel.LastName; };
                    if (upgradedDonor.Email != null)
                    { currentModel.Email = upgradedDonor.Email; }
                    else
                    { upgradedDonor.Email = currentModel.Email; };
                    if (upgradedDonor.DonationNumber > 0)
                    { currentModel.DonationNumber = upgradedDonor.DonationNumber; }
                    else
                    { upgradedDonor.DonationNumber = currentModel.DonationNumber; };

                    string upgrade = $"Update Donor set FirstName = '{upgradedDonor.FirstName}'," +
                        $"LastName = '{upgradedDonor.LastName}'," +
                        $"Email = '{upgradedDonor.Email}'," +
                        $"DonNumber = '{upgradedDonor.DonationNumber}'  Where Donor.Id = '{id}';";


                    adapter.UpdateCommand = new SqlCommand(upgrade, connection);
                    reader.Close();
                    await adapter.UpdateCommand.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
        }
        public async Task<bool> DeleteDonorAsync(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            bool idCheck = false;

            using (connection)
            {
                DonorModel currentModel = new DonorModel();
                SqlCommand command1 = new SqlCommand($"SELECT * FROM Donor Where Donor.Id = '{id}';", connection);
                await connection.OpenAsync();
                SqlDataReader reader = await command1.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    SqlCommand command2 = new SqlCommand($"DELETE FROM Donor Where Donor.Id = '{id}';", connection);
                    reader.Close();
                    adapter.DeleteCommand = command2;
                    await adapter.DeleteCommand.ExecuteNonQueryAsync();
                    idCheck = true;
                    connection.Close();
                }
            }
            return idCheck;
        }
    }
}
