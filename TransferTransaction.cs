using BankSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    // New class for tranferring funds between accounts
    public class TransferTransaction : Transaction
    {
        // Instance variables
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;
        private bool _executed;
        private bool _reversed = false;

        // TransferTransaction constructor
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._withdraw = new WithdrawTransaction(_fromAccount, _amount);
            this._deposit = new DepositTransaction(_toAccount, _amount);
        }

        // Public method to print details of transfer
        public override void Print()
        {
            if (Reversed)
            {
                Console.WriteLine("Reversed Transfer Transaction. Transferred back " + _amount.ToString("C") + " to " + _fromAccount.Name + "'s account from " + _toAccount.Name + "'s account successfully!");
            }
            else if(Success)
            {
                Console.WriteLine("Transferred " + _amount.ToString("C") + " from " + _fromAccount.Name + "'s account to " + _toAccount.Name + "'s account successfully!");
            }
            else
            {
                Console.WriteLine("Transaction Cancelled! Insufficient Funds!");
            }
        }

        // Public method to excute the transfer process
        public override void Execute()
        {
            try
            {
                if (_executed)
                {
                    throw new InvalidOperationException("Transfer transaction already executed!");
                }
                _withdraw.Execute();

                if (!_withdraw.Success)
                {
                    throw new InvalidOperationException("Insufficient Funds!");
                }

                _deposit.Execute();

                if (!_deposit.Success)
                {
                    return;
                }

                _executed = true;
                _success = true;
                Print();
                Console.WriteLine();
            }

            catch (Exception e)
            {
                Console.WriteLine("Tranfer transaction cancelled: " + e.Message);
                _success = false;
            }

        }

        // Public method to reverse the transfer process
        public override void Rollback()
        {
            try
            {
                if (_reversed)
                {
                    throw new InvalidOperationException("Transfer transaction already reversed!");
                }

                if (!_executed)
                {
                    throw new InvalidOperationException("Transfer transaction was not successfull!");
                }

                _reversed = true;
                Console.WriteLine("Transfer transaction reversed!");
                _withdraw.Rollback();
                _deposit.Rollback();
            }

            catch (Exception e)
            {
                _reversed = false;
                Console.WriteLine("Rollback transfer transaction cancelled: " + e.Message);
            }
        }

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