//Prague parking

bool systemOn = true;
string[] carPark = new string[101];

/************** Main program loop ****************/

do
{
    //Menu choices and operations
    int choice = 0;
    int Menu(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("Park");
                Park();
                break;
            case 2:
                Console.WriteLine("Search");
                Search();
                break;
            case 3:
                Console.WriteLine("Show All");
                ShowAll();
                break;
            case 4:
                Console.WriteLine("Move");
                Move();
                break;
            case 5:
                Console.WriteLine("Get / remove");
                Remove();
                break;
            case 6:
                Console.WriteLine("Shutdown");
                systemOn = false;
                Console.WriteLine("Good bye!");
                break;
            default:
                Console.WriteLine("nothing");
                break;
        }
        return choice;
    }

    Console.WriteLine("Please choose an option (1 - 6)");
    Console.WriteLine("1. Park");
    Console.WriteLine("2. Search");
    Console.WriteLine("3. Show all");
    Console.WriteLine("4. Move");
    Console.WriteLine("5. Remove");
    Console.WriteLine("6. Shut down");

    choice = int.Parse(Console.ReadLine());
    Menu(choice);

    /*************** Methods for operations *************/

    //Park
    void Park(string reg = "", string type = "", string vehicle = "")
    {
        //Get vehicle registration number and type
        Console.WriteLine("Please enter registration number:");
        reg = Console.ReadLine();
        Console.WriteLine("Is the vehicle a car or a mc? (type car or mc)");
        type = Console.ReadLine();

        //Concat strings with separator
        vehicle = reg + "#" + type;

        //Find space and park
        for (int i = 1; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                carPark[i] = vehicle;
           
                Console.WriteLine($"You parked {type} with reg {reg} at space: {i}");
                break;
            }
        }
    }

    //Search vehicle
    void Search(int space = 0, string reg = "")
    {
        //Find vehicle by reg number
        Console.WriteLine("Enter registration number of the vehicle you wish to find: ");
        reg = Console.ReadLine();
        space = getSpace(reg);

        if (space > 0)
        {
            Console.WriteLine($"Found vehicle with registration {reg} at space {space}");
        }
        else
        {
            Console.WriteLine($"Vehicle with registration {reg} can not be found..");
        }
    }

    //Show all vehicles
    void ShowAll()
    {
        for(int i = 1; i < carPark.Length; i++)
        {
            Console.WriteLine($"Vechile: {carPark[i]} | Space: {i}");
        }
    }

    //Move vehicle
    void Move(string reg = "", int currentSpace = 0, int newSpace = 0)
    {
        Console.WriteLine("Enter registration number to move vehicle:");
        reg = Console.ReadLine();

        currentSpace = getSpace(reg);
        Console.WriteLine("CURRENT SPACE IS " + currentSpace);
        Console.WriteLine("Please choose a new space (1 - 100)");
        newSpace = int.Parse(Console.ReadLine()) ;

        if (carPark[newSpace] != null)
        {
            Console.WriteLine("This space is taken, please choose another space...");
        }
        else
        {
            carPark[newSpace] = carPark[currentSpace];
            carPark[currentSpace] = null;
        }
    }

    //Remove vehicle
    void Remove(int space = 0, string reg = "")
    {
        Console.WriteLine("Please enter registration number to remove vehicle from parking:");
        reg = Console.ReadLine();
        space = getSpace(reg);
        carPark[space] = null;
        Console.WriteLine($"You removed car with registration number {reg} from space {space}");
    }


    //Get space helper method
    int getSpace(string reg = "", int space = 0)
    {
        for (int i = 1; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                continue;
            }
            else
            {
                if (carPark[i].Contains(reg))
                {
                    space = i;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        return space;
    }

} while (systemOn) ;


