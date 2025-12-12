namespace BankApp
{
    internal class Transaction
    {
        internal decimal Amount { get; set; }
        internal string Currency { get; }
        internal string Description { get; set; }
        internal DateTime TimeStamp { get; private set; }
        internal string FromAccountID { get; set; }
        internal string ToAccountID { get; set; }
        internal bool Completed { get; private set; }

        internal Transaction(decimal amount, string currency, string description, string fromAccountID, string toAccountID)
        {
            Amount = amount;
            Currency = currency;
            Description = description;
            TimeStamp = DateTime.Now;
            FromAccountID = fromAccountID;
            ToAccountID = toAccountID;
            Completed = false;
        }

        // Method to mark the transaction as completed
        internal void SetCompleted()
        {
            TimeStamp = DateTime.Now;
            Completed = true;
        }

        // Override ToString method for transaction representation
        public override string ToString()
        {
            string dateFormat = TimeStamp.ToString("yyyy-MM-dd HH:mm");

            if (string.IsNullOrWhiteSpace(Description))
            {
                return $"{Math.Round(Amount, 2)} {Currency}, {dateFormat}";
            }

            return $"{Math.Round(Amount, 2)} {Currency} {Description}, {dateFormat}";
        }
    }
}
