using BankApp.BankAccounts;
using BankApp.Users;

namespace BankApp.Services
{
    internal static class LoanService
    {
        private static Customer? _customer;

        internal static void SetCustomer(Customer customer)
        {
            _customer = customer;
        }

        // Loan creation method
        internal static void LoanSetup()
        {
            if (_customer == null)
            {
                UI.PrintError("Ingen kund hittades.");
                UI.PrintResetMessage();
                return;
            }

            var bankAccounts = _customer.GetBankAccounts();
            //Check if user has any bank accounts, stop method if not.
            if (bankAccounts.Count == 0)
            {
                UI.PrintError("Vänligen skapa ett konto innan du ansöker om ett lån.");
                UI.PrintResetMessage();
                return;
            }

            //PrintBankAccount to show all user's accounts and amount of money. 
            decimal totalInSEK = 0;
            foreach (var account in bankAccounts)
            {
                decimal balance = account.Balance;
                decimal balanceInSEK = Data.ToSEK(balance, account.Currency);
                totalInSEK += balanceInSEK;
            }

            //Own money
            decimal ownMoney = totalInSEK - _customer.GetTotalLoanWithoutInterest();

            //Maximum of what user can loan
            decimal maxLoan = ownMoney * 5;

            //What custumer can borrow apart from already borrowed money
            maxLoan -= _customer.GetTotalLoanWithoutInterest();

            // Check if the requested loan amount is valid, stop method if not.
            if (maxLoan <= 0)
            {
                UI.PrintError("Du har inte tillräckligt med pengar för att låna.");
                UI.PrintResetMessage();
                return;
            }

            string loanInfo = Loan.GetLoanInfo(totalInSEK - _customer.GetTotalLoanWithoutInterest(), maxLoan);

            UI.PrintMessage(loanInfo);

            // Confirm loan creation, stop method if user inputs no.
            if (!InputUtilities.GetYesOrNo())
            {
                UI.PrintWarning("Lån avbrutet.");
                UI.PrintResetMessage();
                return;
            }

            CreateLoan(maxLoan);
        }

        internal static void CreateLoan(decimal maxLoan)
        {
            if (_customer == null)
            {
                return;
            }

            UI.PrintMessage("\nHur mycket vill du låna?");
            var borrowedAmountSEK = InputUtilities.GetPositiveDecimal();

            // Validate loan amount
            while (borrowedAmountSEK > maxLoan || borrowedAmountSEK <= 0)
            {
                UI.PrintColoredMessage($"Du kan ej låna {borrowedAmountSEK}", ConsoleColor.Yellow);
                borrowedAmountSEK = InputUtilities.GetPositiveDecimal();
            }

            Loan newLoan = new Loan(borrowedAmountSEK);
            UI.PrintWarning($"\nDin totala skuld inklusive ränta: {newLoan.GetTotalLoan()} SEK");

            // Choose account to deposit loan into
            UI.PrintMessage("Vilket konto vill du låna till?", 1);
            UI.PrintList(_customer.GetBankAccounts(), true);
            var bankAccounts = _customer.GetBankAccounts();
            var chosenIndex = InputUtilities.GetIndex(bankAccounts.Count);
            var account = bankAccounts[chosenIndex];

            _customer.AddLoan(newLoan);
            decimal depositedAmount = Data.FromSEK(borrowedAmountSEK, account.Currency);

            account.AddBalance(Math.Round(depositedAmount, 2));

            var latestTransaction = account.GetLatestTransaction();
            UI.PrintSuccess($"\n{latestTransaction.ToString()}", 1);
            UI.PrintResetMessage();
        }

        internal static void PayBackLoan()
        {
            if (_customer == null)
            {
                return;
            }

            var loans = _customer.GetLoans();
            // Check if there are any loans to pay back
            if (loans.Count == 0)
            {
                UI.PrintError("Du har inga lån att betala tillbaka.");
                UI.PrintResetMessage();
                return;
            }

            // Choose which loan to pay back to
            UI.PrintMessage("--- Mina lån ---", 1);
            UI.PrintList(loans, true);
            UI.PrintMessage("Vilket lån vill du betala tillbaka?");
            int loanIndex = InputUtilities.GetIndex(loans.Count);

            // Get remaining loan debt
            Loan selectedLoan = loans[loanIndex];
            decimal remainingDebt = selectedLoan.GetTotalLoan();
            var bankAccounts = _customer.GetBankAccounts();

            // Choose amount to pay back
            UI.PrintColoredMessage($"\nDin återstående skuld: {Math.Round(remainingDebt, 2)} SEK", ConsoleColor.Yellow);
            UI.PrintMessage("Hur mycket vill du betala tillbaka av lånet?");

            // Validate pay back amount
            decimal payBackAmount = InputUtilities.GetPositiveDecimal();
            if (payBackAmount <= 0)
            {
                UI.PrintError("\nFelaktig summa, belopp måste vara större än 0.");
            }

            // Adjust pay back amount if it exceeds remaining loan debt
            if (payBackAmount > remainingDebt)
            {
                payBackAmount = remainingDebt;
                UI.PrintColoredMessage($"\nDu försöker betala tillbaka mer än din nuvarande skuld på: ({remainingDebt} SEK).\n" +
                    $"Beloppet justeras till {remainingDebt} SEK ", ConsoleColor.Yellow);
            }

            // Choose account to pay from
            var accountToPayFrom = ChooseAccountToPayLoanFrom(bankAccounts);

            // Check if there are sufficient funds to pay back the loan
            decimal accountBalanceInSEK = Data.ToSEK(accountToPayFrom.Balance, accountToPayFrom.Currency);

            if (accountBalanceInSEK < payBackAmount)
            {
                UI.PrintError("\nDu har inte tillräckligt med pengar för att återbetala lånet.");
                UI.PrintResetMessage();
                return;
            }

            // Process the loan pay back
            decimal withDrawAmount = Data.FromSEK(payBackAmount, accountToPayFrom.Currency);
            accountToPayFrom.RemoveBalance(withDrawAmount);

            selectedLoan.ReduceLoan(withDrawAmount);

            if (selectedLoan.GetLoanWithoutInterest() <= 0)
            {
                loans.RemoveAt(loanIndex);
                UI.PrintMessage($"\nDu har betalat tillbaka {withDrawAmount} SEK på lånet");
            }
            else
            {
                UI.PrintMessage($"\nDin återstående skuld är: {selectedLoan.GetLoanWithoutInterest()} SEK");
            }

            UI.PrintSuccess("Återbetalning genomförd!", 1);
            UI.PrintResetMessage();
        }

        private static BankAccount ChooseAccountToPayLoanFrom(List<BankAccount> bankAccounts)
        {
            // Choose account to pay from
            UI.PrintMessage("\nVilket konto vill du använda för att betala lånet?");
            UI.PrintList(bankAccounts, true);
            int accountIndex = InputUtilities.GetIndex(bankAccounts.Count);
            return bankAccounts[accountIndex];
        }
    }
}
