using System;
using System.Collections.Generic;

namespace ConsoleApp12
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            Name = name;
            Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Account: {Name}, Balance: {Balance}";
        }
    }


    // Savings Account
    public class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(
            string name = "Unnamed Savings Account",
            double balance = 0.0,
            double interestRate = 0.0)
            : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (amount < 0)
                return false;

            double interest = amount * InterestRate / 100;
            Balance += amount + interest;

            return true;
        }

        public override string ToString()
        {
            return $"Savings Account: {Name}, Balance: {Balance}, Interest Rate: {InterestRate}%";
        }
    }


    // Checking Account
    public class CheckingAccount : Account
    {
        private const double WithdrawalFee = 1.50;

        public CheckingAccount(
            string name = "Unnamed Checking Account",
            double balance = 0.0)
            : base(name, balance)
        {
        }

        public override bool Withdraw(double amount)
        {
            double totalAmount = amount + WithdrawalFee;

            if (Balance - totalAmount >= 0)
            {
                Balance -= totalAmount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Checking Account: {Name}, Balance: {Balance}";
        }
    }


    // Trust Account
    public class TrustAccount : SavingsAccount
    {
        private int WithdrawalCount = 0;

        public TrustAccount(
            string name = "Unnamed Trust Account",
            double balance = 0.0,
            double interestRate = 0.0)
            : base(name, balance, interestRate)
        {
        }

        public override bool Deposit(double amount)
        {
            if (amount < 0)
                return false;

            double interest = amount * InterestRate / 100;

            Balance += amount + interest;

            if (amount >= 5000)
            {
                Balance += 50;
            }

            return true;
        }

        public override bool Withdraw(double amount)
        {
            if (WithdrawalCount >= 3)
                return false;

            if (amount >= Balance * 0.20)
                return false;

            if (Balance - amount >= 0)
            {
                Balance -= amount;
                WithdrawalCount++;

                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Trust Account: {Name}, Balance: {Balance}, Interest Rate: {InterestRate}%";
        }
    }


    // Utility Class
    public static class AccountUtil
    {
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine(
                "\n=== Depositing to Accounts =================================");

            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine(
                "\n=== Withdrawing from Accounts ==============================");

            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        public static void DepositSavings(
            List<SavingsAccount> accounts,
            double amount)
        {
            Console.WriteLine(
                "\n=== Depositing to Savings Accounts ==========================");

            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawSavings(
            List<SavingsAccount> accounts,
            double amount)
        {
            Console.WriteLine(
                "\n=== Withdrawing from Savings Accounts =======================");

            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        public static void DepositChecking(
            List<CheckingAccount> accounts,
            double amount)
        {
            Console.WriteLine(
                "\n=== Depositing to Checking Accounts =========================");

            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawChecking(
            List<CheckingAccount> accounts,
            double amount)
        {
            Console.WriteLine(
                "\n=== Withdrawing from Checking Accounts ======================");

            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} to {acc}");
            }
        }

        public static void DepositTrust(
            List<TrustAccount> accounts,
            double amount)
        {
            Console.WriteLine(
                "\n=== Depositing to Trust Accounts ============================");

            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawTrust(
            List<TrustAccount> accounts,
            double amount)
        {
            Console.WriteLine(
                "\n=== Withdrawing from Trust Accounts =========================");

            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // Accounts
            var accounts = new List<Account>();

            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);


            // Savings
            var savAccounts = new List<SavingsAccount>();

            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.DepositSavings(savAccounts, 1000);
            AccountUtil.WithdrawSavings(savAccounts, 2000);


            // Checking
            var checAccounts = new List<CheckingAccount>();

            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.DepositChecking(checAccounts, 1000);

            AccountUtil.WithdrawChecking(checAccounts, 2000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);


            // Trust
            var trustAccounts = new List<TrustAccount>();

            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(
                new TrustAccount("Wonderwoman2", 5000, 5.0)
            );

            AccountUtil.DepositTrust(trustAccounts, 1000);
            AccountUtil.DepositTrust(trustAccounts, 6000);

            AccountUtil.WithdrawTrust(trustAccounts, 2000);
            AccountUtil.WithdrawTrust(trustAccounts, 3000);
            AccountUtil.WithdrawTrust(trustAccounts, 500);

            Console.WriteLine();
        }
    }
}