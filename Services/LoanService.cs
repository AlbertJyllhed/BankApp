using BankApp.BankAccounts;
using BankApp.Users;

namespace BankApp.Services
{
    internal class LoanService
    {
        // Loan creation method
        //TODO: Refactor method to smaller methods
        internal void LoanSetup(Customer customer)
        {
            var bankAccounts = customer.GetBankAccounts();
            //Check if user has any bank accounts, stop method if not.
            if (bankAccounts.Count == 0)
            {
                UI.PrintError("Du har inget bankkonto, vänligen skapa ett innan du gör lån.");
                return;
            }

            //PrintBankAccount to show all user's accounts and amount of money. 
            decimal totalInSEK = 0;
            foreach (var account in bankAccounts)
            {
                decimal balance = account.GetBalance();
                decimal balanceInSEK = Data.ToSEK(balance, account.Currency);
                totalInSEK += balanceInSEK;
            }

            //Own money
            decimal ownMoney = totalInSEK - customer.GetTotalLoanWithoutInterest();

            //Maximum of what user can loan
            decimal maxLoan = ownMoney * 5;

            //What custumer can borrow apart from already borrowed money
            maxLoan -= customer.GetTotalLoanWithoutInterest();

            // Check if the requested loan amount is valid, stop method if not.
            if (maxLoan <= 0)
            {
                UI.PrintError("Du har inga pengar att låna för och kan därför inte skapa lån.");
                return;
            }

            string loanInfo = Loan.GetLoanInfo(totalInSEK - customer.GetTotalLoanWithoutInterest(), maxLoan);

            UI.PrintMessage(loanInfo);

            // Confirm loan creation, stop method if user inputs no.
            if (!InputUtilities.GetYesOrNo())
            {
                UI.PrintColoredMessage("Lånet har blivit avbrutet.", ConsoleColor.Yellow);
                return;
            }

            CreateLoan(maxLoan, bankAccounts);
        }

        internal void CreateLoan(decimal maxLoan, List<BankAccount> bankAccounts)
        {
            UI.PrintMessage("Hur mycket vill du låna?");
            var borrowedAmountSEK = InputUtilities.GetPositiveDecimal();

            while (borrowedAmountSEK > maxLoan || borrowedAmountSEK <= 0)
            {
                UI.PrintColoredMessage($"Du kan ej låna {borrowedAmountSEK}", ConsoleColor.Yellow);
                borrowedAmountSEK = InputUtilities.GetInt();
            }

            Loan newLoan = new Loan(borrowedAmountSEK);
            UI.PrintMessage($"Totala beloppet att betala tillbaka (inklusive ränta): {newLoan.GetTotalLoan()} SEK");

            UI.PrintMessage("Vilket konto vill du låna till? ");
            PrintBankAccounts();
            var chosenIndex = InputUtilities.GetIndex(bankAccounts.Count);
            var account = bankAccounts[chosenIndex];

            Loans.Add(newLoan);

            decimal depositedAmount = Data.FromSEK(borrowedAmountSEK, account.Currency);

            account.AddBalance(depositedAmount);
            UI.PrintMessage(account.GetLatestTransactionInfo());
        }

        internal void PayBackLoan(Customer customer, List<BankAccount> bankAccounts)
        {
            var loans = customer.GetLoans();
            // Check if there are any loans to pay back
            if (loans.Count == 0)
            {
                UI.PrintError("Du har för nuvarande inga lån att betala tillbaka.");
                return;
            }

            // Choose which loan to pay back to
            UI.PrintMessage("--- Dina lån ---");
            UI.PrintList(loans, true);
            UI.PrintLine();
            UI.PrintMessage("Vilket lån vill du betala tillbaka?");
            int loanIndex = InputUtilities.GetIndex(loans.Count);


            Loan selectedLoan = loans[loanIndex];

            decimal remainingLoanDept = selectedLoan.GetTotalLoan();

            // Choose amount to pay back
            UI.PrintMessage($"Din återstående skuld: {remainingLoanDept} SEK\n" +
                $"Hur mycket vill du betala tillbaka av lånet?");


            decimal payBackAmount = InputUtilities.GetPositiveDecimal();

            if (payBackAmount <= 0)
            {
                UI.PrintError("Felaktig summa, belopp måste vara större än 0.");
                return;
            }

            // Adjust pay back amount if it exceeds remaining loan debt
            if (payBackAmount > remainingLoanDept)
            {
                UI.PrintColoredMessage($"Du försöker betala tillbaka mer än din nuvarande skuld ({remainingLoanDept}). Belopp blir justerat",
                    ConsoleColor.Yellow);

                payBackAmount = remainingLoanDept;
            }

            // Choose which account to pay from
            if (bankAccounts.Count == 0)
            {
                UI.PrintError("Du har inga bankkonton att betala ifrån.");
            }

            UI.PrintMessage("Vilket konto vill du använda för att betala lånet?");
            PrintBankAccounts();
            int accountIndex = InputUtilities.GetIndex(bankAccounts.Count);
            BankAccount accountToPayFrom = bankAccounts[accountIndex];

            // Check if there are sufficient funds to pay back the loan
            decimal accountBalanceInSEK = Data.ToSEK(accountToPayFrom.GetBalance(), accountToPayFrom.Currency);

            if (accountBalanceInSEK < payBackAmount)
            {
                UI.PrintError("Du har för lite pengar för att göra en återbetalning på lånet.");
                return;
            }

            decimal withDrawAmount = Data.FromSEK(payBackAmount, accountToPayFrom.Currency);
            withDrawAmount = Math.Round(withDrawAmount, 2);
            accountToPayFrom.RemoveBalance(withDrawAmount);

            selectedLoan.ReduceLoan(payBackAmount);

            if (selectedLoan.GetLoanWithoutInterest() <= 0)
            {
                loans.RemoveAt(loanIndex);
                UI.PrintMessage($"Du har betalat tillbaka {payBackAmount} SEK på ditt lån");
            }

            else
            {
                UI.PrintMessage($"Din återstående skuld är nu: {selectedLoan.GetLoanWithoutInterest()} SEK");
            }

            UI.PrintMessage("Återbetalning genomförd!");
        }
    }
}
