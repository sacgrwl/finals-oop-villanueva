using System;

class Program
{
    private const decimal SALARY_PER_DAY = 1300m;
    private const decimal OVERTIME_RATE = 54m;
    private const decimal TAX_RATE = 0.10m;

    static void Main()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Password: ");
        string password = ReadPasswordFromConsole();

        if (ValidatePassword(password))
        {
            if (PromptUserToContinue())
            {
                (int daysWorked, int daysAbsent, int overtimeHours) = GetEmployeeDetails();

                decimal totalSalary = CalculateTotalSalary(daysWorked, daysAbsent, overtimeHours);
                decimal netSalary = CalculateNetSalary(totalSalary);

                DisplaySalary(totalSalary, netSalary);

                string bankChoice = ChooseBank();
                string accountNumber = GetAccountDetails();

                if (ConfirmTransfer())
                {
                    TransferSalary(netSalary, accountNumber, bankChoice);
                }
                else
                {
                    Console.WriteLine("Transfer canceled.");
                }
            }
            else
            {
                Console.WriteLine("Operation canceled.");
            }
        }
        else
        {
            Console.WriteLine("Invalid password.");
        }

        LogOut();
    }

    static string ReadPasswordFromConsole()
    {
        string password = "";
        ConsoleKeyInfo info;

        do
        {
            info = Console.ReadKey(true);

            if (info.Key != ConsoleKey.Enter)
            {
                if (info.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        Console.Write("\b \b"); // Backspace and overwrite with space
                        password = password.Substring(0, password.Length - 1);
                    }
                }
                else
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
            }
        }
        while (info.Key != ConsoleKey.Enter);

        return password;
    }

    static bool ValidatePassword(string password)
    {
        // Add your password validation logic here
        // For demonstration purposes, I'm assuming the password is valid if it's not empty
        return !string.IsNullOrEmpty(password);
    }

    static bool PromptUserToContinue()
    {
        Console.WriteLine("Enter 2 to continue or 1 to cancel:");
        return int.TryParse(Console.ReadLine(), out int choice) && choice == 2;
    }

    static (int daysWorked, int daysAbsent, int overtimeHours) GetEmployeeDetails()
    {
        Console.Write("Enter number of days worked: ");
        int daysWorked = int.Parse(Console.ReadLine());

        Console.Write("Enter number of days absent: ");
        int daysAbsent = int.Parse(Console.ReadLine());

        Console.Write("Enter number of overtime hours: ");
        int overtimeHours = int.Parse(Console.ReadLine());

        return (daysWorked, daysAbsent, overtimeHours);
    }

    static decimal CalculateTotalSalary(int daysWorked, int daysAbsent, int overtimeHours)
    {
        return (daysWorked - daysAbsent) * SALARY_PER_DAY + (overtimeHours * OVERTIME_RATE);
    }

    static decimal CalculateNetSalary(decimal totalSalary)
    {
        return totalSalary * (1 - TAX_RATE);
    }

    static void DisplaySalary(decimal totalSalary, decimal netSalary)
    {
        Console.WriteLine($"Total Salary: {totalSalary:C}");
        Console.WriteLine($"Net Salary after {TAX_RATE:P} tax: {netSalary:C}");
    }

    static string ChooseBank()
    {
        Console.WriteLine("Choose your bank:");
        Console.WriteLine("1. BDO");
        Console.WriteLine("2. BPI");
        Console.WriteLine("3. METRO BANK");
        Console.WriteLine("4. PNB");
        Console.WriteLine("5. SECURITY BANK");
        Console.WriteLine("6. UNION BANK");
        Console.WriteLine("7. LAND BANK");

        int bankChoice = int.Parse(Console.ReadLine());

        return bankChoice switch
        {
            1 => "BDO",
            2 => "BPI",
            3 => "METRO BANK",
            4 => "PNB",
            5 => "SECURITY BANK",
            6 => "UNION BANK",
            7 => "LAND BANK",
            _ => "Unknown Bank"
        };
    }

    static string GetAccountDetails()
    {
        Console.Write("Enter bank account number: ");
        return Console.ReadLine();
    }

    static bool ConfirmTransfer()
    {
        Console.WriteLine("Enter 1 to transfer salary or 2 to cancel:");
        return int.TryParse(Console.ReadLine(), out int choice) && choice == 1;
    }

    static void TransferSalary(decimal netSalary, string accountNumber, string bankChoice)
    {
        Console.WriteLine($"Transferring {netSalary:C} to account {accountNumber} at {bankChoice}.");
    }

    static void LogOut()
    {
        Console.WriteLine("Type 0 to log out:");
        int logoutChoice = int.Parse(Console.ReadLine());

        if (logoutChoice == 0)
        {
            Console.WriteLine("Logging out. Goodbye!");
        }
    }
}