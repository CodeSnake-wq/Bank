using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        protected bool _executed;
        protected bool _reversed;
        public DateTime _dateStamp;

        public abstract bool Success
        {
            get;
        }

        public virtual bool Executed
        {
            get
            {
                return _executed;
            }
        }

        public virtual bool Reversed
        {
            get
            {
                return _reversed;
            }
        }

        public DateTime DateStamp
        {
            get
            {
                return _dateStamp;
            }
        }

        public Transaction(decimal amount)
        {
            this._amount = amount;
            _dateStamp = DateTime.Now;
            _executed = true;
        }

        public virtual void Execute()
        {
        }

        public virtual void Rollback()
        {
        }

        public abstract void Print();
    }
}
