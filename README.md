# Liskov Bank
This is a C# bank system. Every customer can create a bank account and savings account with different currencies. requested and pay back loans and even insert money and withdraw in their bank accounts.
Customer can handle different transactions. Admin can login, create new customer, change currency rate and unlock locked customer profiles. 
## Classes
### User
Baseclass for Admin and Customer classes.
### Admin
This class can create new customers, change the currency rate and unlock locked customer profiles. 
### Customer
The Customer class represents a bank customer who can log in, own multiple bank accounts, deposit and withdraw money and manage loans. It inherits from the User baseclass and extends it with login tracking, account/loan management, and transaction functionality.
### Bank
The Bank class is where the Log in and Log out method is, also a Run method that you use in the Program.cs so the program can run. It also creates the menues depending if a Customer or Admin is logged in.
### Data
Contains all the data information for the bank application.
### InputUtilities
Receive different kind of input from user and see if it's a valid input or not.
### Loan
Gets all the loan information.
### Menu
Help the user navigate through the bank application.
### Program
This class runs the bank app.
### Transaction
Handles the transaction information.
### UI
The UI-class function is printing different kind of messages for different purposes.
### BankAccount
In this class we add/remove balance from accounts, print transfer details, creates the transactions and also print the transactions on that specific customer.
### SavingsAccount
Print savings account information.
### AccountService
The AccountService class is used for creating and setting up different kinds of bank accounts and adding the bank account to the specific user's bank account list.
### LoanService
This class is used for applying for a loan, creates the loan, adds the loan to the loan-list and the user can also pay back their loan.
### TransactionService 
Handles the transaction process between different accounts.

