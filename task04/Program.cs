using System.Security.Principal;

namespace task04
{
    public class Program
    {
        static void Main()
        {
            //// Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            ////Savings
            var savAccounts = new List<Account>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            //// Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Deposit(trustAccounts, 4000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();
            Console.ReadKey();
        }
    }

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
        public static double operator +(Account lhs, Account rhs)
        {
            return lhs.Balance + rhs.Balance;
        }

        public override string ToString()
        {
            return $"Name : {Name} , Balance : {Balance}";
        }
    }
    public class SavingsAccount : Account
    {
        public double Rate { get; set; }
        public SavingsAccount(string Name = "Unnamed Account", double Balance = 0.0, double rate = 0.0) : base(Name, Balance)
        {
            this.Rate = rate;
        }

        public override string ToString()
        {
            return $"{base.ToString()} Rate:{Rate}";

        }
       
    }


    public static class AccountUtil
    {
        // Utility helper functions for Account class

        public  static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
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
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }



    }
    public class CheckingAccount : Account
    {
        public const double fee = 1.50;
        public CheckingAccount(string Name = "Unnamed Account", double Balance = 0.0) : base(Name, Balance)
        {
        }
        public override string ToString()
        {
            return $"{base.ToString()} fee : {fee} ";

        }
        public override bool Withdraw(double amount)
        {
            Balance -= fee;
            return base.Withdraw(amount);
        }
    }

    public class TrustAccount : SavingsAccount
    {
        public static int Count=0;
        public int year = DateTime.Now.Year;
   
        public TrustAccount(string Name = "Unnamed Account", double Balance = 0.0, double rate = 0) : base(Name, Balance)
        {
            this.Rate = rate;
        }
        public override bool Deposit(double amount)
        {
            if (amount > 5000)
                Balance += 50;
            return base.Deposit(amount);
        }
        
        public override bool Withdraw(double amount)
        {
            if (year == year + 1)
            {
                Count = 0; return true;
            }
            Count++;
            if (Count > 3)
            {
                Console.WriteLine("sorry you passed the acess you have to wait for the next year");
                return false;
            }else 
                if(amount < (Balance*20/100))
                    return base.Withdraw(amount);
                else return false;
        }

    }
}
