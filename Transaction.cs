namespace BankApp
{
    internal class Transaction
    {
        //private List<Transaction> _transactions = [];
        internal string FromUser { get; set; }
        internal string ToUser { get; set; }
        internal decimal Amount { get; set; }
        internal DateTime TimeStamp { get; }
        internal string Description { get; set; }


        internal Transaction(string fromUser, string toUser, decimal amount, string description)
        {
            FromUser = fromUser;
            ToUser = toUser;
            Amount = amount;
            TimeStamp = DateTime.Now;
            Description = description;
        }
        public override string ToString()
        {
            return $"{TimeStamp:G} - {Description} ({Amount})";
        }


    }
}
