using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;
using VehicleProject.Repository;

namespace VehicleProject.Services
{
    public class ModelOwnerService
    {

        private VehicleModelService _vehicleModelService = new VehicleModelService();
        private OwnerService _ownerService = new OwnerService();
        private ModelOwnerRepository _modelOwnerRepository = new ModelOwnerRepository();

        public async Task AddNewModelForOwner()
        {
            Console.WriteLine("All models");
            await _vehicleModelService.ListAllVehicleModels();
            Console.WriteLine();

            Console.WriteLine("All all owners");
            await _ownerService.GetAllOwner();



            Console.WriteLine("---------------");
            Console.WriteLine("Enter model id for input");
            int modelInput;

            if (!int.TryParse(Console.ReadLine(), out modelInput))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.WriteLine("Enter owner id for input");
            int ownerInput;

            if (!int.TryParse(Console.ReadLine(), out ownerInput))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            var vehicleModelOwner = new VehicleModelOwner()
            {
                VehicleModel_id = modelInput,
                Owner_id = ownerInput,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.MinValue

            };

            var result=  await _modelOwnerRepository.AddVehicleMakeOwner(vehicleModelOwner);
            if (result)
            {
                Console.WriteLine("Success added!");
            }
            else
            {
                Console.WriteLine("Not added! Check Model, Owner or existing record!");
            }
        }
    


        }
    }

