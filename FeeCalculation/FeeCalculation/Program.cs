using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

class Program
{
    static JObject feeStructure;

    static string[] courseOptions = { "Medical", "Dental", "Ayurveda" };
    static string[] LevelOptions = { "UG", "PG", "DIPLOMA", "Ph.D" };

    static void Main(string[] args)
    {
        string jsonString = File.ReadAllText("C:\\Users\\HARI\\source\\repos\\FeeCalculation\\FeeCalculation\\FeeStructure.json");
        feeStructure = JObject.Parse(jsonString);
        Console.WriteLine("Select a fee:");
        string selectedFee = SelectOption(feeStructure);

        JObject nationalityObject = (JObject)feeStructure[selectedFee];
        Console.WriteLine("Select a nationality:");
        string selectedNationality = SelectOption(nationalityObject);

        JObject courseObject = (JObject)nationalityObject[selectedNationality];
        Console.WriteLine("Select a course:");

        string selectedCourse = SelectOption(courseObject);

        JObject levelObject = (JObject)courseObject[selectedCourse];
        Console.WriteLine("Select a level:");
        string selectedLevel = SelectOption(levelObject);
        JObject amountObject= (JObject)levelObject[selectedLevel];
        int feeAmount = (int)amountObject["amount"];
        Console.WriteLine($"The fee amount is {feeAmount}.");
        Console.ReadLine();
    }

    static string SelectOption(JObject options)
    {
        int idx = 1;
        foreach (var option in options)
        {
            string txt = option.Key;
            bool res = txt.ToString().Trim().Contains("ALL_COURSES​");
            bool res1= txt.ToString().Trim().Contains("ALL_LEVEL​");
            if (res1)
            {
                SelectLevelOption();
                return "ALL_LEVEL​";

            }
            if (res)
            {
                SelectCourseOption();
                return "ALL_COURSES​";

            }
            Console.WriteLine($"{idx}. {option.Key}");
            idx++;
           
        }

        while (true)
        {
            try
            {
                int choice = int.Parse(Console.ReadLine());
                if (choice >= 1 && choice <= options.Count)
                {
                    return options.Properties().ElementAt(choice - 1).Name;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static string SelectCourseOption()
    {
        int idx = 1;
        foreach (string option in courseOptions)
        {
            Console.WriteLine($"{idx}. {option}");
            idx++;

        }

        while (true)
        {
            try
            {
                int choice = int.Parse(Console.ReadLine());
                if (choice >= 1 && choice <= courseOptions.Length)
                {
                    return "ALL_COURSES​";
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
    static string SelectLevelOption()
    {
        int idx = 1;
        foreach (string option in LevelOptions)
        {
            Console.WriteLine($"{idx}. {option}");
            idx++;

        }

        while (true)
        {
            try
            {
                int choice = int.Parse(Console.ReadLine());
                if (choice >= 1 && choice <= LevelOptions.Length)
                {
                    return "ALL_LEVEL​​";
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}
