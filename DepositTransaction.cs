using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    // New class for deposting money in account
    public class DepositTransaction : Transaction
    {
        private Account _account;
        private bool _executed;
        private bool _reversed = false;

        //DepositTranaction constructor
        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            this._account = account;
        }

        // Public method to print details of deposit
        public override void Print()
        {
            if(Reversed)
            {
                Console.WriteLine("Reversed Deposit Transaction. Withdrew back " + _amount.ToString("C") + " from " + _account.Name + "'s account");
            }
            else if(Success)
            {
                Console.WriteLine("Deposited " + _amount.ToString("C") + " to " + _account.Name + "'s account");
            }
            else
            {
                Console.WriteLine("Transaction Cancelled! Negative Funds!");
            }
        }

        // public method to execute the deposit
        public override void Execute()
        {
            try
            {
                if (_executed)
                {
                    throw new InvalidOperationException("Transaction already excecuted!");
                }

                if (!_account.Deposit(_amount))
                {
                    throw new InvalidOperationException("Negative amount!");
                }

                _executed = true;
                _success = true;
                Console.WriteLine("Deposit Transaction Executed!");
                Print();
                _account.Print();                
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Deposit transaction Cancelled: " + e.Message);
                Console.WriteLine();
                _executed = true;
                _success = false;
            }
        }

        // Public method to reverse the deposit
        public override void Rollback()
        {
            try
            {
                if (_reversed)
                {
                    throw new InvalidOperationException(" Deposit transaction already reversed!");
                }

                if (!_executed)
                {
                    throw new InvalidOperationException("Deposit transaction not executed");
                }

                if (!Success)
                {
                    throw new InvalidOperationException("Deposit transaction was not successfull!");
                }
                _account.Withdraw(_amount);

                Console.WriteLine("Deposit transaction reversed!");
                _account.Print();
                _reversed = true;
            }
            catch (InvalidOperationException e)
            {
                _reversed = false;
                Console.WriteLine("Rollback transaction Cancelled: " + e.Message);
                Console.WriteLine();
            }
        }

        // Property to check whether the transaction successful
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