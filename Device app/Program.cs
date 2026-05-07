using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Xml.Linq;
using System.Xml.XPath;

class Program
{
    //global variables
    static List<string> DEVICECATEGORIES = new List<string> { "Laptop", "Desktop", "Other"};
    static List<string> ERRORS = new List<string>() { "Error: please enter a valid decimal number.", "Error Please Use Vaid Catergory", "Error: please use Y or N", };
    static List<string> VALID = new List<string>() { "Please input a quantity", "Please input device price", "Please input device catergory", "Please Enter Y or N" };
    static int laptopCounter = 0;
    static int desktopCounter = 0;
    static int otherCounter = 0;
    static string mostExpensiveDevice = "";
    static decimal mostExpensiveCost = -1;
    static void Main(string[] args)
    {
        //local variables 
        char Nextdevice = 'y';
        DEVICECATEGORIES.AsReadOnly();
        ERRORS.AsReadOnly();
        VALID.AsReadOnly();

        // Display app titile
        Console.WriteLine("██████╗ ███████╗██╗   ██╗██╗ ██████╗███████╗     █████╗ ██████╗ ██████╗ \r\n██╔══██╗██╔════╝██║   ██║██║██╔════╝██╔════╝    ██╔══██╗██╔══██╗██╔══██╗\r\n██║  ██║█████╗  ██║   ██║██║██║     █████╗      ███████║██████╔╝██████╔╝\r\n██║  ██║██╔══╝  ╚██╗ ██╔╝██║██║     ██╔══╝      ██╔══██║██╔═══╝ ██╔═══╝ \r\n██████╔╝███████╗ ╚████╔╝ ██║╚██████╗███████╗    ██║  ██║██║     ██║     \r\n╚═════╝ ╚══════╝  ╚═══╝  ╚═╝ ╚═════╝╚══════╝    ╚═╝  ╚═╝╚═╝     ╚═╝     ");
        // Display app description
        Console.WriteLine("\n\nA simple and easy-to-use app designed for schools to calculate the cost of insuring student devices. Staff can enter\n details such as device type, value, and coverage options to get an estimated insurance cost, helping schools manage budgets and protect their equipment more effectively.");
        Console.WriteLine("\n\nPress <ENTER> to continue...");
        Console.ReadLine();

        Console.Clear();

        // Loop untill all devices have been inpuuted
        while (Nextdevice.Equals('y'))
        {

            // call OneDeivce method
            OneDevice();

            Nextdevice = CheckProceed();

            Console.Clear();
        }
        //display total device summary
        string totalDeviceSummary = "$$$$$$$$$ Final Devices Summary $$$$$$$$$\n";
        totalDeviceSummary += $"Total Number Of Laptops: {laptopCounter}\n";
        totalDeviceSummary += $"Total Number Of Desktops: {desktopCounter}\n";
        totalDeviceSummary += $"Total Number Of Others: {otherCounter}\n";
        totalDeviceSummary += $"Most Expensive Device Name: {mostExpensiveDevice}\n";
        totalDeviceSummary += $"Most Expensive Device Cost: {mostExpensiveCost}\n";
        Console.WriteLine(totalDeviceSummary);

    }
    static void OneDevice()
    {
        //local variables 
        int quantityOfDevice = -1;
        decimal devicePrice = -1m;
        decimal insuranceAmount = -1m;
        string deviceCatergory = "";
        string deviceName = "";
        string deprecationSummary = "";
        decimal deprecationValue;
        string deviceSummary = "";
        const decimal DISCOUNT = 0.9m;
        Console.WriteLine("---------- Log Device ----------]\n");

        //user input the device name
        Console.WriteLine("Please input device name:");
        deviceName = Console.ReadLine();

        //user inputs device quantity amount
        quantityOfDevice = CheckQuantityOfDevice();

        //device price 
        devicePrice = CheckDevicePrice();

        //device catergory
        deviceCatergory = CheckDevicecatergory();

        //increase device counters
        if (deviceCatergory == "Laptop")
        {
         laptopCounter += quantityOfDevice;
        }
        else
        if (deviceCatergory == "Desktop")
        {
         desktopCounter += quantityOfDevice;
        }
        else
        if (deviceCatergory == "Other")
        {
         otherCounter += quantityOfDevice;
        }

                //calculate insurance amount
                if (quantityOfDevice > 5)
        {
            // first five will be insured at full cost
            insuranceAmount = 5 * devicePrice;
            insuranceAmount += (quantityOfDevice - 5) * DISCOUNT * devicePrice;

            //remaining devices will be insured at 10% less of cost
        }
        else
        {
            insuranceAmount = devicePrice * quantityOfDevice;
        }

        //determine if the device is most expensive device
        if (insuranceAmount > mostExpensiveCost)
        {
            mostExpensiveDevice = $"{deviceName}";
            mostExpensiveCost = insuranceAmount;
        }

        //calculate 5% Deprecation Over 6 Months
        deprecationValue = devicePrice;
        for (int monthCount = 1; monthCount < 7; monthCount++)
        {
            deprecationValue = deprecationValue * 0.95m;
            deprecationSummary += $"Month {monthCount}:\t\t{deprecationValue:C}\n";
        }
        //create device summary
        deviceSummary += $"{deviceName}\nTotal cost for {quantityOfDevice}  {deviceName} devices is = to {devicePrice:C}\nMonth\t\t\tvalue Loss\n{deprecationSummary} {deviceCatergory}: {devicePrice:C}";
        Console.WriteLine(deviceSummary);
        
    }

    static char CheckProceed()
    {
        while (true)
        {
            string proceed;
            Console.WriteLine(VALID[3]);
            proceed = Console.ReadLine();

            if (Regex.IsMatch(proceed, @"^[YNyn]+$") && !string.IsNullOrEmpty(proceed) && proceed.Length == 1)
            {
                return char.Parse(proceed[0].ToString().ToLower());
            }
            else
            {
                Console.WriteLine(ERRORS[2]);
            }

        }
    }

    static string CheckDevicecatergory()
    {
        while (true)
        {
            string Devicecatergory;
            Console.WriteLine(VALID[2]);
            Devicecatergory = Console.ReadLine();
            if (DEVICECATEGORIES.Contains(Devicecatergory) && !string.IsNullOrEmpty(Devicecatergory))
            {
                return Devicecatergory;
            }
            else
            {
                Console.WriteLine(ERRORS[1]);
            }
        }
    }
       static decimal CheckDevicePrice()
        {
            while (true)
            {
            Console.WriteLine(VALID[1]);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal price) && price >= 0)
                {
                  return price;
                }
                else
                {
                Console.WriteLine(ERRORS[0]);
                }


            }

        }
    static int CheckQuantityOfDevice()
    {
        while (true)
        {
            Console.WriteLine(VALID[0]);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int price) && price >= 0)
            {
                return price;
            }
            else
            {
                Console.WriteLine(ERRORS[0]);
            }
        }

    }


}



