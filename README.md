# Liskov Bank
This is a C# bank system. Every customer can create a bank account and savings account with different currencies. requested and pay back loans and even insert money and withdraw in their bank accounts.
Customer can handle different transactions. Admin can login, create new customer, change currency rate and unlock locked customer profiles. 
## Classes

### User
Baseclass for Admin and Customer classes.
### Admin
This class can create new customers, change the currency rate and unlock locked customer profiles. 
### Customer

### Bank

### Data
Contains all the data information for the bank application.
### InputUtilities
Receive different kind of input from user and see if it's a valid input or not.
### Loan
Gets loan information.
### Menu
Help the user navigate through the bank application.
### Program
This class runs the bank app.
### Transaction
Handles the transaction information.
### UI
The UI-class function is printing different kind of messages for different purposes.
### BankAccount

### SavingsAccount
Print savings account information.
### AccountService
The AccountService class is used for creating and setting up different kinds of bank accounts and adding the bank account to the specific user's bank account list.
### LoanService
This class is used for applying for a loan, creates the loan, adds the loan to the loan-list and the user can also pay back their loan.
### TransactionService 
Handles the transaction process between different accounts.

