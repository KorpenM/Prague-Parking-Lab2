// Prague Parking //

string[] parkingSpots = new string[101];
Random random = new Random();

void Main()
{
    ShowMenu();
}

void ShowMenu() // Metoden för menyhantering
{
    bool running = true;
    while (running)
    {
        Console.Clear();

        Console.WriteLine("======================");
        Console.WriteLine("   PRAGUE PARKING");
        Console.WriteLine("======================\n");

        Console.WriteLine("Välj ett alternativ:");
        Console.WriteLine("1. Parkera:");
        Console.WriteLine("2. Hämta ut ditt fordon:");
        Console.WriteLine("3. Flytta till annan P-plats:");
        Console.WriteLine("4. Sök:");
        Console.WriteLine("5. Visa parkeringsplatser:");
        Console.WriteLine("6. Visa alla registrerade fordon:");
        Console.WriteLine("7. Avsluta");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Ange regnummer:");
                string regNumber = Console.ReadLine();
                Console.WriteLine("Ange fordonstyp (MC/Bil):");
                string type = Console.ReadLine();
                ParkVehicle(regNumber, type); // Parkera fordonet
               break;

            /*case "1": // Random parkering
                Console.WriteLine("Ange regnummer:");
                string regNumber = Console.ReadLine();
                Console.WriteLine("Ange fordonstyp (MC/Bil):");
                string type = Console.ReadLine();
                ParkRandom(regNumber, type); // Parkera fordonet
                break;*/

            case "2":
                Console.WriteLine("Ange regnummer:");
                string regNumberRemove = Console.ReadLine();
                RemoveVehicle(regNumberRemove);
                break;
            case "3":
                Console.WriteLine("Ange startplats:");
                int fromSpot = int.Parse(Console.ReadLine());
                Console.WriteLine("Ange slutplats:");
                int toSpot = int.Parse(Console.ReadLine());
                MoveVehicle(fromSpot, toSpot);
                break;
            case "4":
                Console.WriteLine("Ange regnummer:");
                string searchRegNumber = Console.ReadLine();
                FindVehicle(searchRegNumber);
                break;
            case "5":
                ShowParkingSpots(); // Visa både lediga och upptagna parkeringsplatser
                break;
            case "6":
                ShowRegisteredVehicles(); // Visa alla registrerade regnummer
                break;
            case "7":
                running = false; // Avsluta programmet
                break;
            default:
                Console.WriteLine("Ogiltligt val.");
                break;
        }

        Console.WriteLine("\nTryck på Enter för att fortsätta...");
        Console.ReadKey();
    }
}

void ParkVehicle(string regNumber, string type)
{
    if (regNumber.Length > 10)
    {
        Console.WriteLine("Regnummer kan inte vara längre än 10 tecken.");
        return;
    }

    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] == null)
        {
            parkingSpots[i] = $"{regNumber} ({type})";
            Console.WriteLine($"Fordon {regNumber} parkerad på plats {i + 1}");
            return;
        }
        else if (type.ToLower() == "mc" && parkingSpots[i].Contains("mc") && !parkingSpots[i].Contains(","))
        {
            parkingSpots[i] += $", {regNumber} ({type})";
            Console.WriteLine($"MC {regNumber} parkerad på plats {i + 1}");
            return;
        }
    }
    Console.WriteLine("Alla P-platser är upptagna.");
}

// Extra metod om vi vill slumpa P-plats vid parkering
void ParkRandom(string regNumber, string type)
{
    if (regNumber.Length > 10)
    {
        Console.WriteLine("Regnummer kan inte vara längre än 10 tecken.");
        return;
    }

    bool isParked = false; // Flagga för att se om fordonet har parketats
    int attempts = 0; // För att hålla på antalet försök

    while (!isParked && attempts < 101)
    {
        int randomSpot = random.Next(1, parkingSpots.Length);

        if (parkingSpots[randomSpot] == null)
        {
            parkingSpots[randomSpot] = $"{regNumber} ({type})";
            Console.WriteLine($"{regNumber} parkerad på {randomSpot + 1}.");
            isParked = true;
            return;
        }
        else if (type.ToLower() == "mc" && parkingSpots[randomSpot].Contains("mc") && !parkingSpots[randomSpot].Contains(","))
        {
            parkingSpots[randomSpot] += $", {regNumber} ({type})";
            Console.WriteLine($"MC {regNumber} parkerad på {randomSpot + 1}.");
            isParked = true;
            return;
        }
        attempts++;
    }

    if (!isParked)
    {
        Console.WriteLine("Alla P-platser är upptagna.");
    }
}

void RemoveVehicle(string regNumber)
{
    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] != null && parkingSpots[i].Contains(regNumber))
        {
            // Om det är en MC och det finns två MC på platsen, ta bort bara den valda MC
            if (parkingSpots[i].Contains(","))
            {
                parkingSpots[i] = parkingSpots[i].Replace($"{regNumber} (mc)", "").Trim().Trim(',');
            }
            else
            {
                parkingSpots[i] = null;
                Console.WriteLine($"{regNumber} har hämtats ut från: P-plats {i + 1}");
            }
            return;
        }
    }
    Console.WriteLine($"Fordon med regnummer {regNumber} hittas ej.");
}

void MoveVehicle(int fromSpot, int toSpot)
{
    // Justerar så att index börjar på 1 för användarvänlighet
    fromSpot--;
    toSpot--;

    if (fromSpot < 0 || fromSpot >= parkingSpots.Length || toSpot < 0 || toSpot >= parkingSpots.Length)
    {
        Console.WriteLine("Ogiltligt platsnummer");
        return;
    }

    if (parkingSpots[fromSpot] == null)
    {
        Console.WriteLine("Det finns inget fordon på angiven plats.");
        return;
    }

    if (parkingSpots[toSpot] == null)
    {
        // Flyttar fordon till ny plats
        parkingSpots[toSpot] = parkingSpots[fromSpot];
        parkingSpots[fromSpot] = null;
        Console.WriteLine($"Fordon flyttat från plats {fromSpot + 1} till plats {toSpot + 1}.");
    }
    else
    {
        Console.WriteLine("Angiven plats är upptagen.");
    }
}


void FindVehicle(string regNumber)
{
    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] != null && parkingSpots[i].Contains(regNumber))
        {
            Console.WriteLine($"{regNumber} hittat: P-plats {i + 1}.");
            return;
        }
    }
    Console.WriteLine($"{regNumber} hittas ej.");
}

void ShowParkingSpots()
{
    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] == null)
        {
            Console.WriteLine($"Plats {i + 1}: Ledig");
        }
        else
        {
            Console.WriteLine($"Plats {i + 1}: {parkingSpots[i]}");
        }
    }
}

void ShowRegisteredVehicles()
{
    Console.WriteLine("Aktiva parkeringar: ");
    bool foundAnyVehicle = false; // Flagga för att kontrollera om vi hittar fordon

    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] != null)
        {
            Console.WriteLine(parkingSpots[i]);
            foundAnyVehicle = true;
        }
    }

    if (!foundAnyVehicle)
    {
        Console.WriteLine("Inget fordon är registrerat.");
    }
}