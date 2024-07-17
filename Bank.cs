using BankSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankSystem
{
    // Public class bank
    public class Bank
    {
        // New private list of accounts in a bank
        private List<Account> _accounts = new List<Account>();
        private List<Transaction> _transactions = new List<Transaction>();

        //Bank constructor
        public Bank()
        {
        }

        // Public method to add account in the bank
        public void AddAccount(Account account)
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i].Name == account.Name)
                {
                    Console.WriteLine("Account Name already exist write another name");
                    Console.WriteLine();
                    return;
                }
            }
            _accounts.Add(account);
        }

        // Public method to get information of account
        public Account GetAccount(string name)
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i].Name == name)
                {
                    return _accounts[i];
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }
        
        public void RollbackTransaction(int transactionNumber)
        {
            int i;
            for(i = 0; i < _transactions.Count; i++)
            {
                if (_transactions[i].Reversed) 
                {
                }

                else
                {
                    break;
                }
            }

            if(i == _transactions.Count)
            {
                Console.WriteLine("All transactions are already reversed!");
                return;
            }

            if (_transactions[transactionNumber-1].Reversed)
            {
                Console.WriteLine("Transaction already reversed! Enter another transaction number");
                do
                {
                    Console.Write("Enter the transaction number: ");

                    if (int.TryParse(Console.ReadLine(), out transactionNumber) && transactionNumber > 0 && transactionNumber <= NoOfTransaction())
                    {
                        RollbackTransaction(transactionNumber);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid input");
                    }
                } while (!(transactionNumber > 0 && transactionNumber <= NoOfTransaction()));
            }
            else
            {
                _transactions[transactionNumber - 1].Rollback();
            }
        }

        // Public method to get total number of accounts in the bank
        public int NoOfAccounts()
        {
            return _accounts.Count();
        }

        // Public method to get name of the account holder at index i
        public string Name(int i)
        {
            return _accounts[i].Name;
        }

        public void PrintTransactionHistory()
        {
            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine("Transaction " + (i + 1));
                Console.WriteLine("Transaction Time: " + _transactions[i].DateStamp);

                if (_transactions[i].Reversed)
                {
                    Console.WriteLine("Status: Reversed");
                }

                else if (_transactions[i].Success)
                {
                    Console.WriteLine("Status: Successfull");
                }

                else
                {
                    Console.WriteLine("Status: Unsuccessfull");
                }
                
                Console.Write("Details: ");
                _transactions[i].Print();
                Console.WriteLine();
            }
        }

        public int NoOfTransaction()
        {
            return _transactions.Count();
        }
    }
}