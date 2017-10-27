using System;
using System.Text;

namespace UnitTest_CSharp.NUnit
{
    /// <summary>A simple Money.</summary>
     class Money : IMoney
    {
        private int iAmount;
        private String iCurrency;

        /// <summary>Constructs a money from the given amount and
		/// currency.</summary>
		public Money(int amount, String currency)
        {
            iAmount = amount;
            iCurrency = currency;
        }

        public int Amount
        {
            get { return iAmount; }
        }

        public bool IsZero
        {
            get { return Amount == 0; }
        }

        public IMoney Add(IMoney m)
        {
            return m.AddMoney(this);
        }

        public IMoney AddMoney(Money m)
        {
            if (m.Currency.Equals(Currency))
                return new Money(Amount + m.Amount, Currency);
            return new MoneyBag(this, m);
        }

        public IMoney AddMoneyBag(MoneyBag s)
        {
            return s.AddMoney(this);
        }

        public IMoney Multiply(int factor)
        {
            return new Money(Amount * factor, Currency);
        }

        public IMoney Negate()
        {
            return new Money(-Amount, Currency);
        }

        public IMoney Subtract(IMoney m)
        {
            return Add(m.Negate());
        }

        public string Currency
        {
            get { return iCurrency; }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[" + Amount + " " + Currency + "]");
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (IsZero)
            {
                if (obj is IMoney)
                    return ((IMoney)obj).IsZero;
            }
            if (obj is Money)
            {
                Money money = obj as Money;
                return money.Currency.Equals(Currency)
                    && Amount == money.Amount;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return iCurrency.GetHashCode() + iAmount;
        }
    }
}
