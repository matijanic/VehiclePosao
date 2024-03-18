using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using VehicleProject.Models;
using VehicleProject.Util;

namespace VehicleProject.Repository
{
    public class ModelOwnerRepository
    {


        public async Task<bool> AddVehicleMakeOwner(VehicleModelOwner newVehicleModelOwner)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {

                var sqlFunction = "Select add_vehiclemodelowner(@VehicleModel_id, @Owner_id, @dateCreated, @dateUpdated)";

                var result = await connection.QueryFirstOrDefaultAsync<bool>(sqlFunction, new
                {
                    VehicleModel_id = newVehicleModelOwner.VehicleModel_id,
                    Owner_id = newVehicleModelOwner.Owner_id,
                    dateCreated= newVehicleModelOwner.DateCreated,
                    dateUpdated = newVehicleModelOwner.DateUpdated

                });

                return result;

                
                //await connection.OpenAsync();

                //using (var command = new NpgsqlCommand("Select add_vehiclemodelowner(@VehicleModel_id, @Owner_id, @dateCreated, @dateUpdated)", connection))
                //{


                //    {
                //        command.Parameters.AddWithValue("@VehicleModel_id", newVehicleModelOwner.VehicleModel_id);
                //        command.Parameters.AddWithValue("@Owner_id", newVehicleModelOwner.Owner_id);
                //        command.Parameters.AddWithValue("@dateCreated", newVehicleModelOwner.DateCreated);
                //        command.Parameters.AddWithValue("@dateUpdated", newVehicleModelOwner.DateUpdated);

                //        var result = await command.ExecuteScalarAsync();
                //        return (bool)result;


                //    }
                //}
            }
        }
       
    }
}
