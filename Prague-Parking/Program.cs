//Prague parking

string[] parking = new string[100];
int[] slotNr = new int[101];
int menu = 0;

/************** Main program loop ****************/

do
{
    Console.Clear();
    Console.WriteLine("[0] End Program");
    Console.WriteLine("[1] Add veichle");
    Console.WriteLine("[2] Remove veichle");
    Console.WriteLine("[3] Relocate veichle");
    Console.WriteLine("[4] Find veichle");
    Console.WriteLine("[5] Show Parking");

    menu = int.Parse(Console.ReadLine());
    switch (menu)
    {
        case 1:
            addVeichle();
            break;
        case 2:
            removeVeichle();
            break;
        case 3:
            relocateVeichle();
            break;
        case 4:
            findVeichle();
            break;
        case 5:
            showParking();
            break;
        case 0:
            Console.Clear();
            Console.WriteLine("Ending program");
            break;
    }
} while (menu != 0);

/*************** Methods for operations *************/

//Park
void addVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate for the veichle you'd like to add: ");
    string regPlate = Console.ReadLine();

    for (int i = 0; i < parking.Length; i++)
    {
        if (parking[i] != null)
        {
            continue;
        }
        parking[i] = regPlate;
        Console.WriteLine($"{regPlate} has been parked at Slot {i+1}");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        break;
    }
}

//Remove
void removeVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the veichle that you'd like to remove: ");
    string regPlate = Console.ReadLine();
    int plateFound = 0;

    for (int i = 0; i < parking.Length; ++i)
    {
        if (regPlate == parking[i])
        {
            plateFound++;
        }
    }

    if (plateFound != 1)
    {
        Console.WriteLine("None of the cars parked matches the registration plate you typed in");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (regPlate == parking[i])
        {
            parking[i] = null;

        }
    }

    Console.WriteLine("The veichle has been removed");
    Console.Write("\n\nPress random key to continue...");
    Console.ReadKey();
}

//Move
void relocateVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the veichle that you'd like to move: ");
    string regPlate = Console.ReadLine();
    Console.Write("Now type in the number of the spot that you'd like to move it to: ");
    int moveTo = int.Parse(Console.ReadLine());
    int plateFound = 0;

    for (int i =0; i < parking.Length; ++i)
    {
        if (regPlate == parking[i])
        {
            plateFound++;
        }
    }

    if (plateFound != 1)
    {
        Console.WriteLine("None of the cars parked matches the registration plate you typed in");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (regPlate == parking[i])
        {
            parking[i] = null;

        }
        else if (moveTo == slotNr[i])
        {
            parking[i] = regPlate;
        }
    }

    Console.WriteLine("The veichle has been moved");
    Console.Write("\n\nPress random key to continue...");
    Console.ReadKey();
}

//Find
void findVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the veichle that you'd like to find: ");
    string regPlate = Console.ReadLine();

    for (int i = 0; i < parking.Length; i++)
    {
        if (regPlate == parking[i])
        {
            Console.WriteLine($"{regPlate} can be found at Slot {slotNr[i]}");
            Console.Write("\n\nPress random key to return to continue...");
            Console.ReadKey();
            break;
        }
    }
}

//Show
void showParking()
{
    Console.Clear();
    Console.WriteLine("Parking: ");
    Console.WriteLine(" ");
    for (int i = 0; i < parking.Length; i++)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(parking[i] + " ");
        Console.ResetColor();

        slotNr[i] = i + 1;

        if (parking[i] == null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Slot " + slotNr[i]);
            Console.ResetColor();
        }
    }

    Console.Write("\n\nPress random key to continue...");
    Console.ReadKey();
}