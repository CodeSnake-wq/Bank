using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    // New class for withdrawing money from account
    public class WithdrawTransaction : Transaction
    {
        private Account _account;
        private bool _executed;
        private bool _reversed = false;

        //WithdrawTransaction constructor
        public WithdrawTransaction(Account account, decimal amount) : base (amount)
        {
            this._account = account;
        }

        // Public method to print detail of withdraw
        public override void Print()
        {
            if (Reversed)
            {
                Console.WriteLine("Reversed Withdraw Transaction. Transferred back " + _amount.ToString("C") + " to " + _account.Name + "'s account");
            }
            else if (Success)
            {
                Console.WriteLine("Withdrew " + _amount.ToString("C") + " from " + _account.Name + "'s account");
            }
            else
            {
                Console.WriteLine("Transaction Cancelled! Insufficient Funds!");
            }
        }

        // public method to execute the withdraw
        public override void Execute()
        {
            try
            {
                if (_executed)
                {
                    throw new InvalidOperationException("Transaction already excecuted!");
                }

                if (!_account.Withdraw(_amount))
                {
                    throw new InvalidOperationException("Insufficient funds!");
                }

                _executed = true;
                _success = true;
                Console.WriteLine("Withdraw Transaction Executed!");
                Print();
                _account.Print();
                
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Withdraw transaction Cancelled: " + e.Message);
                Console.WriteLine();
                _executed = true;
                _success = false;
            }
        }

        // Public method to reverse the withdraw
        public override void Rollback()
        {
            try
            {
                if (_reversed)
                {
                    throw new InvalidOperationException("Withdraw transaction already reversed!");
                }

                if (!_executed)
                {
                    throw new InvalidOperationException("Withdraw transaction not executed");
                }

                if (!Success)
                {
                    throw new InvalidOperationException("Withdraw transaction was not sucessfull!");
                }
                _account.Deposit(_amount);

                _reversed = true;
                Console.WriteLine("Withdraw transaction reversed!");
                _account.Print();
                
            }
            catch (InvalidOperationException e)
            {
                _reversed = false;
                Console.WriteLine("Rollback transaction Cancelled: " + e.Message);
                Console.WriteLine();
            }
        }

        // Property to check whether the transaction is successful
        public override bool Success
        {
            get
            {
                return _success;
            }
        }

        public override bool Reversed
        {
            get
            {
                return _reversed;
            }
        }
    }
}