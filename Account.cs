using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    // Public class account
    public class Account
    {
        // Instance variables
        private string _name;
        private decimal _balance;

        // Account constructor
        public Account(string name, decimal balance)
        {
            this._name = name;
            this._balance = balance;
        }

        // Method for depositing money in account
        public bool Deposit(decimal amount)
        {
            // if depositing amount is greater than 0, then only money will be deposited
            if (amount > 0)
            {
                this._balance += amount;
                return true; // Deposit successful
            }
            else
            {
                return false; // Deposit failed
            }
        }

        // Method withdrawing money from account
        public bool Withdraw(decimal amount)
        {
            // Money is withdrawn only if amount is greater than 0 and less than or equal to account balance
            if (amount > 0 && amount <= this._balance)
            {
                this._balance -= amount;
                return true; // Withdraw successful
            }
            else
            {
                return false; // Withdraw failed
            }
        }

        //Prints informations of account i.e., account holder name and balance of account
        public void Print()
        {
            Console.WriteLine("Account Holder Name: " + this._name);
            Console.WriteLine("Account Balance: " + this._balance.ToString("C"));
            Console.WriteLine();
        }

        // Prints account holder name
        public string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}