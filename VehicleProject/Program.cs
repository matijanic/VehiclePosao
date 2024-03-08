using DbUp;
using Npgsql;
using System.Reflection;
using System.Runtime.CompilerServices;
using VehicleProject.Models;
using VehicleProject.Repository;
using VehicleProject.Services;
using VehicleProject.Util;

var upgrader =
        DeployChanges.To
            .PostgresqlDatabase(Constants.connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

if (upgrader.IsUpgradeRequired())
{
    var result = upgrader.PerformUpgrade();
    if (!result.Successful)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(result.Error);
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();      
    }

}

var _vehicleMakeService = new VehicleMakeService();
var _vehicleModelService = new VehicleModelService();

int enter;





Console.WriteLine("");
Console.WriteLine("1. Get All Makes");
Console.WriteLine("2. Add Vehicle Make");
Console.WriteLine("3. Delete Vehicle Make");
Console.WriteLine("4. Update Vehicle Make");
Console.WriteLine("5. Get Vehicle Make by Id");
Console.WriteLine("6. Search Vehicle Make");

Console.WriteLine("-----------------------");
Console.WriteLine("7. Get All Models");
Console.WriteLine("8. Add Vehicle Model");
Console.WriteLine("9. Delete Vehicle Model");
Console.WriteLine("10. Update Vehicle Model");
Console.WriteLine("11. Get Vehicle Model by Id");
Console.WriteLine("12. Search Vehicle Model");

Console.WriteLine("0. End");
do
{
    Console.WriteLine("");
    Console.Write("Enter choice: ");
    var input = Console.ReadLine();
    enter = int.Parse(input);


    switch (enter)
    {
        case 1: await _vehicleMakeService.ListAllVehicleMakes();
            break;
        case 2:
            await _vehicleMakeService.AddNewVehicleMake();
            break;
        case 3:
            await _vehicleMakeService.DeleteVehicleMake();
            break;
        case 4: await _vehicleMakeService.UpdateVehicleMake();
            break;
        case 5: await _vehicleMakeService.GetVehicleMakeById();
            break;
        case 6: await _vehicleMakeService.SearchVehicleMakes();
            break;
        case 7: await _vehicleModelService.ListAllVehicleModels();
            return;
        
        case 8:  await _vehicleModelService.AddNewVehicleModel();
            break;
        case 9: await _vehicleModelService.DeleteVehicleModel();
            break;
        case 10: await _vehicleModelService.UpdateVehicleModel();
            break;
        case 11: await _vehicleModelService.GetModelById();
            break;

        case 12: await _vehicleModelService.SearchVehicleModels();
            break;

        case 0:
            Console.WriteLine("End");
            break;
        default: Console.WriteLine("Wrong choice!");
            break;

    }


}while(enter != 0);






