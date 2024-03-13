﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;
using VehicleProject.Models;
using VehicleProject.Util;

namespace VehicleProject.Repository
{
    public class OwnerRepository
    {
       public async Task<List<Owner>> GetAllOwner()
        {
            var ownerList = new List<Owner>();

            using (var connection = new NpgsqlConnection(VehicleProject.Util.Constants.connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("select \"Owner_id\",\"FirstName\", \"LastName\", \"Address\",\"DateCreated\",\"DateUpdated\" from \"Owner\"", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var newOwner = new Owner()
                            {
                                Owner_id = reader.GetInt32(reader.GetOrdinal("Owner_id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName=reader.GetString(reader.GetOrdinal("LastName")),
                                Address=reader.GetString(reader.GetOrdinal("Address")),
                                DateCreated= reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated= reader.GetDateTime(reader.GetOrdinal("DateCreated"))

                                
                            };

                            ownerList.Add(newOwner);
                        }

                    }
                        
                    
                }
                
            }

            return ownerList;
        }

        public async Task AddNewOwner(Owner newOwner)
        {
            using (var connection = new NpgsqlConnection(Util.Constants.connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("insert into \"Owner\" (\"FirstName\",\"LastName\",\"Address\",\"DateCreated\",\"DateUpdated\" )" +
                    " values (@firstName,@lastName,@address, @dateCreated,@dateUpdated)", connection))
                {
                    command.Parameters.AddWithValue("@firstName", newOwner.FirstName);
                    command.Parameters.AddWithValue("@lastName", newOwner.LastName);
                    command.Parameters.AddWithValue("@address", newOwner.Address);
                    command.Parameters.AddWithValue("@dateCreated", newOwner.DateCreated);
                    command.Parameters.AddWithValue("@dateUpdated", newOwner.DateUpdated);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }

   
}
