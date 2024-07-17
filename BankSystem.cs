using BankSystem;
using System;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankSystem
{
    enum MenuOption
    {
        AddAccount = 1,
        Withdraw,
        Deposit,
        Transfer,
        Print,
        PrintTransactionHistory,
        Quit
    }

    public class BankSystem
    {
        // Static method to find account in the bank
        static Account FindAccount(Bank bank)
        {
            Console.Write("Enter name of Account: ");
            string name = Console.ReadLine();
            if (bank.GetAccount(name) == null)
            {
                Console.WriteLine("Account does not exist!");
            }
            return bank.GetAccount(name);
        }

        //Static method to print menu of the options to user and take input
        static MenuOption ReadUserOption()
        {
            int choice;
            do
            {
                Console.WriteLine("Select an option");
                Console.WriteLine("1. Add new Account");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Print");
                Console.WriteLine("6. PrintTransactionHistory");
                Console.WriteLine("7. Quit");
                Console.WriteLine();
                Console.Write("Enter the option: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice < 8)
                {
                    return (MenuOption)choice;
                }
                else
                {
                    Console.WriteLine("Invalid Option! Please try again");
                    Console.WriteLine();
                }
            } while (true);
        }

        //Static method to deposit money in account
        static void DoDeposit(Bank bank)
        {
            Account account;
            do
            {
                Console.WriteLine("Account's Names: ");
                for (int i = 1; i <= bank.NoOfAccounts(); i++)
                {
                    Console.WriteLine(i + ". " + bank.Name(i - 1));
                }
                account = FindAccount(bank);
                Console.WriteLine();
                if (account == null)
                {
                    Console.WriteLine("Try again.");
                    Console.WriteLine();
                }
            } while (account == null);
            decimal amount = -9;
            do
            {
                Console.Write("Enter the amount you want to deposit: ");
                try
                {
                    amount = decimal.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        Console.WriteLine("Please enter a positive number!");
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a positive number!");
                }
            } while (amount <= 0);
            

            Console.WriteLine();
            DepositTransaction transaction = new DepositTransaction(account, amount);
            bank.ExecuteTransaction(transaction);
        }

        //Static method to withdraw money in account
        static void DoWithdraw(Bank bank)
        {
            Account account;
            do
            {
                Console.WriteLine("Account's Names: ");
                for (int i = 1; i <= bank.NoOfAccounts(); i++)
                {
                    Console.WriteLine(i + ". " + bank.Name(i - 1));
                }
                account = FindAccount(bank);
                Console.WriteLine();
                if (account == null)
                {
                    Console.WriteLine("Try again.");
                    Console.WriteLine();
                }
            } while (account == null);
            decimal amount = -9;
            do
            {
                try
                {
                    Console.Write("Enter the amount you want to withdraw: ");
                    amount = decimal.Parse(Console.ReadLine());
                    if(amount <= 0) 
                    {
                        Console.WriteLine("Please enter a positive number");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Please enter a positive number");
                }
            }while(amount <= 0);
            
            Console.WriteLine();
            WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
            bank.ExecuteTransaction(transaction);
        }

        // Static method to transfer money between accounts
        static void DoTransfer(Bank bank)
        {
            Account _fromAccount;
            Account _toAccount;
            string name;
            string name1;
            do
            {
                Console.Write("Enter the name of account from which you want to tranfer money: ");
                name = Console.ReadLine();
                _fromAccount = bank.GetAccount(name);
                if (_fromAccount == null)
                {
                    Console.WriteLine("Invalid Account Name! Try again.");
                    Console.WriteLine();
                }
            } while (_fromAccount == null);
            do
            {
                Console.Write("Enter the name of account you want to tranfer money: ");
                name1 = Console.ReadLine();
                _toAccount = bank.GetAccount(name1);
                if (_toAccount == null)
                {
                    Console.WriteLine("Invalid Account Name! Try again.");
                    Console.WriteLine();
                }
            } while (_toAccount == null);

            decimal amount = -9;
            do
            {
                try
                {
                    Console.Write("Enter the amount you want to withdraw: ");
                    amount = decimal.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please enter a positive number");
                }
            } while (amount <= 0);

            Console.WriteLine();
            TransferTransaction transaction = new TransferTransaction(_fromAccount, _toAccount, amount);
            bank.ExecuteTransaction(transaction);
        }

        // Static method to print details of the account
        static void DoPrint(Bank bank)
        {
            Account account;
            do
            {
                Console.WriteLine("Account's Names: ");
                for (int i = 1; i <= bank.NoOfAccounts(); i++)
                {
                    Console.WriteLine(i + ". " + bank.Name(i - 1));
                }
                account = FindAccount(bank);
                Console.WriteLine();
                if (account == null)
                {
                    Console.WriteLine("Try again.");
                    Console.WriteLine();
                }
            } while (account == null);
            account.Print();
        }

        static void Main(string[] args)
        {
            Bank ANZ = new Bank();
            // New object named aakash under class Account
            Account aakash = new Account("Aakash", 1000);
            Account raj = new Account("Raj", 2000);

            // Add accounts in the ANZ bank
            ANZ.AddAccount(aakash);
            ANZ.AddAccount(raj);

            MenuOption option;
            do
            {
                // Stores the option choosed by user
                option = ReadUserOption();
                switch (option)
                {
                    // If add new account option is choosed
                    case MenuOption.AddAccount:
                        bool pin = false;
                        string name;
                        do
                        {
                            pin = false;
                            Console.Write("Enter name of the account: ");
                            name = Console.ReadLine();
                            if (name.Length == 0)
                            {
                                Console.WriteLine("Invalid name! Please try again");
                                pin = true;
                            }
                        } while (pin);


                        decimal amount;
                        bool flag = true;
                        do
                        {
                            Console.Write("Enter balance of the account: ");
                            if (decimal.TryParse(Console.ReadLine(), out amount) && amount >= 0)
                            {
                                flag = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid amount please try again.");
                            }
                        } while (flag);
                        Account newAccount = new Account(name, amount);
                        ANZ.AddAccount(newAccount);
                        Console.WriteLine();
                        break;

                    // If withdraw option is choosed
                    case MenuOption.Withdraw:
                        DoWithdraw(ANZ);
                        Console.WriteLine();
                        break;

                    // If deposit option is choosed
                    case MenuOption.Deposit:
                        DoDeposit(ANZ);
                        Console.WriteLine();
                        break;

                    // If transfer option is choosed
                    case MenuOption.Transfer:
                        DoTransfer(ANZ);
                        Console.WriteLine();
                        break;

                    // If print option is choosed 
                    case MenuOption.Print:
                        Console.WriteLine();
                        DoPrint(ANZ);
                        Console.WriteLine();
                        break;

                    case MenuOption.PrintTransactionHistory:
                        ANZ.PrintTransactionHistory();
                        string response;
                        if(ANZ.NoOfTransaction() == 0)
                        {
                            Console.WriteLine("There are no transactions to print.");
                        }
                        else
                        {
                            Console.Write("Do you want to rollback a transaction (Yes/No): ");
                            response = Console.ReadLine();
                            while (!((response.ToUpper() == "YES") || (response.ToUpper() == "NO")))
                            {
                                Console.WriteLine("Please enter a valid input yes or no");
                                Console.Write("Do you want to rollback a transaction (Yes/No): ");
                                response = Console.ReadLine();
                            }

                            if(response.ToUpper() == "YES")
                            {
                                int transactionNumber;
                                do
                                {
                                    Console.Write("Enter the transaction number: ");

                                    if (int.TryParse(Console.ReadLine(), out transactionNumber) && transactionNumber > 0 && transactionNumber <= ANZ.NoOfTransaction())
                                    {
                                        ANZ.RollbackTransaction(transactionNumber);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter a valid input");
                                    }
                                } while (!(transactionNumber > 0 && transactionNumber <= ANZ.NoOfTransaction()));
                            }                            
                        }
                        Console.WriteLine();
                        break;


                    // If option is choosed to quit the program
                    case MenuOption.Quit:
                        Console.WriteLine("Exiting the Program!");
                        return;
                }
            } while (option != MenuOption.Quit);
        }
    }
}