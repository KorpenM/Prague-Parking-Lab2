//Prague parking

string[] parking = new string[100];
int[] slotNr = new int[101];
int menu = 0;

/************** Main program loop ****************/
//Hell0asdsad

do
{
    Console.Clear();
    Console.WriteLine("[0] End Program");
    Console.WriteLine("[1] Add veichle");
    Console.WriteLine("[2] Show Parking");

    menu = int.Parse(Console.ReadLine());
    switch (menu)
    {
        case 1:
            addVeichle();
            break;
        case 2:
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
    string newPlate = Console.ReadLine();

    for (int i = 0; i < parking.Length; i++)
    {
        if (parking[i] != null)
        {
            continue;
        }
        parking[i] = newPlate;
        break;
    }
}

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
    Console.ReadKey(true);
}