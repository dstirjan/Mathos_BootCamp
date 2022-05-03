using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BloodDonor.Model;
using BloodDonor.Repository.Common;
using BloodDonor.Common;

namespace BloodDonor.Repository
{
    public class DonorRepository : IDonorRepository
    {
        static string connectionString = @"Data Source=LEGA-MEGA\SQLEXPRESS;Initial Catalog = BloodDonorsDB;Integrated Security = True";
        public async Task<List<DonorModel>> GetDonorAsync(StringFiltering filter, Sorting sorting, Paging paging)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<DonorModel> donorModels = new List<DonorModel>();
            using (connection)
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Select * from Donor where IsDeleted = 0 ");

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.FirstName))
                    { stringBuilder.Append($" and FirstName = '{filter.FirstName}' "); }
                    if (!string.IsNullOrWhiteSpace(filter.LastName))
                    { stringBuilder.Append($" and LastName = '{filter.LastName}' "); }
                    if (filter.DonationNumber != 0)
                    { stringBuilder.Append($" and DonationNumber = '{filter.DonationNumber}' "); }
                }

                if (sorting != null)
                {
                    if (!string.IsNullOrWhiteSpace(sorting.SortBy))
                    { stringBuilder.Append($" ORDER BY {sorting.SortBy} "); }

                    if (!string.IsNullOrWhiteSpace(sorting.SortOrder))
                    { stringBuilder.Append($" {sorting.SortOrder}" ); }
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
                        DonorModel model = new DonorModel();
                        model.Id = (int)reader["Id"];
                        model.FirstName = (string)reader["FirstName"];
                        model.LastName = (string)reader["LastName"];
                        model.Email = (string)reader["Email"];
                        model.DonationNumber = (int)reader["DonationNumber"];
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
                SqlCommand command = new SqlCommand($"SELECT * FROM Donor Where Donor.Id = '{id}' and IsDeleted= 0;", connection);
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
                        model.DonationNumber = (int)reader["DonationNumber"];
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
                string newDonor = $"Insert into Donor(FirstName,LastName,Email,DonationNumber,ReferentCode) values " +
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

                await connection.OpenAsync();
                string command = $"Update Doctor set FirstName = @FirstName, LastName = @LastName,Email = @Email, DonationNumber=@DonationNumber  Where Id = '{id}'";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                if (!string.IsNullOrWhiteSpace(upgradedDonor.FirstName))
                {
                    sqlCommand.Parameters.AddWithValue("@FirstName", upgradedDonor.FirstName);
                }
                if (!string.IsNullOrWhiteSpace(upgradedDonor.LastName))
                {
                    sqlCommand.Parameters.AddWithValue("@LastName", upgradedDonor.LastName);
                }
                if (!string.IsNullOrWhiteSpace(upgradedDonor.Email))
                {
                    sqlCommand.Parameters.AddWithValue("@Email", upgradedDonor.Email);
                }
                if (upgradedDonor.DonationNumber != 0)
                {
                    sqlCommand.Parameters.AddWithValue("@Email", upgradedDonor.DonationNumber);
                }

                adapter.UpdateCommand = sqlCommand;
                await adapter.UpdateCommand.ExecuteNonQueryAsync();
                connection.Close();
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
                    string update = $"Update Doctor set IsDeleted= 1 Where Donor.Id = '{id}'";
                    reader.Close();
                    adapter.UpdateCommand = new SqlCommand(update, connection);
                    await adapter.DeleteCommand.ExecuteNonQueryAsync();
                    idCheck = true;
                    connection.Close();
                }
            }
            return idCheck;
        }
    }
}
