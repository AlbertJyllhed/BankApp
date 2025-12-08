# Liskov Bank
This is a C# bank system. Every customer can create a bank account and savings account with different currencies. requested and pay back loans and even insert money and withdraw in their bank accounts.
Customer can handle different transactions. Admin can login, create new customer, change currency rate and unlock locked customer profiles. 
## Classes
### User
Base class for Admin and Customer classes.
### Admin
This class can create new customers, change the currency rate and unlock locked customer profiles. 
### Customer
The Customer class represents a bank customer who can log in, own multiple bank accounts, deposit and withdraw money and manage loans.
It inherits from the User base class and extends it with login tracking, account/loan management, and transaction functionality.
### Bank
The Bank class is where the users log in and log out.
Also contains a run method that is used in the Program.cs class so the program can run.
Also creates the menus depending on whether a Customer or Admin is logged in.
### Data
Static class which contains all the data information for the bank application.
### InputUtilities
Receive different kind of input from user and see if it's a valid input or not.
### Loan
The loan class is a info container class for loans.
### Menu
The menu class helps the user navigate through the bank application.
### Program
This class is where the application starts. It simply runs the bank app.
### Transaction
Data container class for transaction information.
### UI
The UI-class is a multipurpose class used to print different kinds of messages.
### BankAccount
In this class we add/remove balance from accounts, print transfer details, creates the transactions and also print the transactions on that specific customer.
### SavingsAccount
Print savings account information.
### AccountService
The AccountService class is used for creating and setting up different kinds of bank accounts and adding the bank account to the specific user's bank account list.
Can create standard bank accounts and savings accounts.
### LoanService
This class is used for applying for a loan, creating loans and adding them to a customers' loan-list.
Also used for paying back customer loans.
### TransactionService 
Handles the transaction process between different accounts.
Also used to insert and withdraw money from accounts.

### Contributors
■ [AlbertJyllhed](https://github.com/AlbertJyllhed)  
■ [CajsaMartensson](https://github.com/CajsaMartensson)  
■ [SaraPPAndersson](https://github.com/SaraPPAndersson)  
■ [VISORKILLEN](https://github.com/VISORKILLEN)  

