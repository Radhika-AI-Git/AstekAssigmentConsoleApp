GIC banking Transaction management System
This project GICBankingApp where users can enter transaction details for accounts,
which includes deposits and withdrawals. The system processes the transactions and ensures
that certain constraints (e.g., first transaction, no negative balance, valid amounts) are respected.
Menu Features
  Welcome to AwesomeGIC Bank! What would you like to do?
		[T] Input transactions 
		[I] Define interest rules
		[P] Print statement
		[Q] Quit
Project Setup
Prerequisites
Make sure you have the following installed on your machine:

.NET SDK (version 6.0 or higher) - Download .NET SDK
Visual Studio (or any other C# compatible IDE like JetBrains Rider, Visual Studio Code, etc.)
NUnit for unit tests.

Clone the repository
First, clone the repository to your local machine using the following command:

bash
Copy
git clone https://github.com/Radhika-AI-Git/AstekAssigmentConsoleApp
Navigate into the project directory:

bash
Copy
cd transaction-management
Restore Dependencies
To restore the project dependencies, run the following command:

bash
Copy
dotnet restore
Build the Project
To build the project and ensure everything is compiled correctly, run:

dotnet build

Run the Program
To run the application, use the following command:

dotnet run
This will start the program and prompt you to input transaction details. The program will guide you through entering transactions and displaying account balances.

Unit Testing
This project GICBankingAppTest includes unit tests written using NUnit. To run the unit tests, follow these steps:

Ensure you have the NUnit test runner installed.
Navigate to the TransactionApp.Tests directory (if necessary).
Run the tests using the following command:

dotnet test