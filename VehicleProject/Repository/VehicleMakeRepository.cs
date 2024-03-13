using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;
using VehicleProject.Util;
using Constants = VehicleProject.Util.Constants;

namespace VehicleProject.Repository
{
    public class VehicleMakeRepository
    {
        public async Task<List<VehicleMake>> GetVehicleMakes()
        {
            var vehicleMakes = new List<VehicleMake>();

            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {

                await connection.OpenAsync();


                using (var command = new NpgsqlCommand("SELECT \"VehicleMake_id\",\"Name\",\"Abrv\", \"DateCreated\",\"DateUpdated\" FROM \"VehicleMake\"", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var vehicleMake = new VehicleMake
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                                
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated"))
                            };

                            vehicleMakes.Add(vehicleMake);
                        }
                    }
             
                }   

            }

            return vehicleMakes;
           
        }

        public async Task AddVehicleMake(VehicleMake newVehicleMake)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();
              
                using (var command = new NpgsqlCommand("Insert into \"VehicleMake\" (\"Name\",\"Abrv\",\"DateCreated\", \"DateUpdated\") values (@Name,@Abrv,@DateCreated, @DateUpdated)", connection))
                {
                    command.Parameters.AddWithValue("@Name", newVehicleMake.Name);
                    command.Parameters.AddWithValue("@Abrv", newVehicleMake.Abrv);
                    command.Parameters.AddWithValue("@DateCreated", newVehicleMake.DateCreated);
                    command.Parameters.AddWithValue("@DateUpdated", newVehicleMake.DateUpdated);
                    

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteVehicleMake(int vehicleId)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("delete from \"VehicleMake\" where \"VehicleMake_id\" = @vehicleId", connection))
                {
                    command.Parameters.AddWithValue("@vehicleId", vehicleId);
                    await command.ExecuteNonQueryAsync();

                }
            }

        }

        public async Task<bool> UpdateVehicleMake(VehicleMake updatedVehicleMake)
        {
           
            using(var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("update \"VehicleMake\" set \"Name\" = @Name, \"Abrv\" = @Abrv, \"DateUpdated\" = @date  where \"VehicleMake_id\" = @vehicleId", connection))
                {
                    command.Parameters.AddWithValue("@vehicleId", updatedVehicleMake.Id);
                    command.Parameters.AddWithValue("@Name", updatedVehicleMake.Name);
                    command.Parameters.AddWithValue("@Abrv", updatedVehicleMake.Abrv);
                    command.Parameters.AddWithValue("@date", updatedVehicleMake.DateUpdated);
                
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }

        public async Task<List<VehicleMake>> SearchVehicleMakes(string searchParam)
        {

            var vehicleMakes= new List<VehicleMake>();

            using (var connection = new NpgsqlConnection (Constants.connectionString))
            {
                await connection.OpenAsync();

                using (var command  = new NpgsqlCommand("SELECT * FROM \"VehicleMake\" where \"Name\" ilike @searchParam or \"Abrv\" ilike @searchParam", connection))
                {
                    command.Parameters.AddWithValue("@searchParam",$"%{searchParam}%" );

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var vehicleMake = new VehicleMake()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                                
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated"))
                            };

                            vehicleMakes.Add(vehicleMake);
                        }
                        
                    }
                }

                return vehicleMakes;
            }
        }

        public async Task<VehicleMake> GetVehicleMakeId (int vehicleMakeId)
        {
            

            using(var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();

                using(var command = new NpgsqlCommand("select * from \"VehicleMake\" where \"VehicleMake_id\" = @vehicleMakeId",connection))
                {
                    command.Parameters.AddWithValue("@vehicleMakeId", vehicleMakeId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                     
                        while (await reader.ReadAsync())
                        {                   

                           var vehicleMake = new VehicleMake()
                            
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateUpdated"))
                           };
                           
                            return vehicleMake;

                        } 
                        return null;

                        
                    }

                }
                
            }

        }

    }
}
