namespace BankApp
{
    internal class Transaction
    {
        internal decimal Amount { get; set; }
        internal DateTime TimeStamp { get; }
        internal string Description { get; set; }


        internal Transaction(decimal amount, string description)
        {
            Amount = amount;
            TimeStamp = DateTime.Now;
            Description = description;
        }
        public override string ToString()
        {
            return $"{Description} {Amount} {TimeStamp:G} ";
        }
       
    }
}
