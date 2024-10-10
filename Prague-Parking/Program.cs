//Prague parking

string car = "";
string mc = "";
bool systemOn = true;
string[] carPark = new string[101];

/************** Main program loop ****************/

do
{
    //Menu choices and operations

    string operation = "";
    int choice = 0;

    int Menu(int choice)
    {
        switch (choice)
        {
            case 0:
                operation = "park";
                Park();
                break;
            case 1:
                operation = "search";
                Search();
                break;
            case 2:
                operation = " showAll";
                break;
            case 3:
                operation = "move";
                break;
            case 4:
                operation = "get/remove";
                break;
            case 5:
                operation = "shutdown";
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
    choice = int.Parse(Console.ReadLine());
    Menu(choice);

    /*************** Methods for operations *************/

    //Park
    void Park()
    {
        //Get vehicle registration number and type
        string reg = "";
        string type = "";
        string vehicle = "";

        Console.WriteLine("Please enter registration number:");
        reg = Console.ReadLine();
        Console.WriteLine("Is the vehicle a car or a mc? (type car or mc)");
        type = Console.ReadLine();

        //Concat strings with separator

        vehicle = reg + "#" + type;

        //Find space and park

        for (int i = 0; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                carPark[i] = vehicle;
                break;
            }
        }

        for (int i = 0; i < carPark.Length; i++)
        {
            if (carPark[i] != null)
                Console.WriteLine($"You parked {type} with reg {reg} at space: {carPark[i]}");
            break;
        }


    }

    //Search car

    void Search()
    {

    }

} while (systemOn);



