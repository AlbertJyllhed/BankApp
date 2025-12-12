using Newtonsoft.Json;

namespace BankApp
{
    internal class Transaction
    {
        public decimal Amount { get; set; }
        public string Currency { get; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; private set; }
        public string FromAccountID { get; set; }
        public string ToAccountID { get; set; }
        public bool Completed { get; private set; }

        [JsonConstructor]
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
