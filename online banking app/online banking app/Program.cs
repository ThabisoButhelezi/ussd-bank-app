using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

class CardHolder
{
    // data member of the class
    string cardNumber;
    int Pin;
    double accBalance;
    ArrayList cardholders;

    CardHolder()
    {
        cardholders = new ArrayList();
        cardholders.Add(new CardHolder("12345", 1234, 10000)); // Accountnumber,PIN, BALANCE
        cardholders.Add(new CardHolder("36987", 4678, 1000));
    }

    public ArrayList getList()
    {
        return cardholders;
    }

    public CardHolder(string cardNumber, int Pin, double balance)// declaring constructor
    {
        // initialized data members with
        this.cardNumber = cardNumber;
        this.Pin = Pin;
        this.accBalance = balance;

    }
    public String getCardnumber()
    {
        return cardNumber;
    }
    public void setPin(int newPin)
    {
        Pin = newPin;
    }
    public void setBalance(double newBalance)
    {
        accBalance = newBalance;
    }
    public double getBalance()
    {
        return accBalance;
    }
    public int getPin()
    {
        return Pin;
    }
    public CardHolder login()/// login function
    {
        Console.WriteLine("Enter your [account number]: ");
        String accountnum = Console.ReadLine();
        Console.WriteLine("Enter your [Pin]: ");
        int pin = int.Parse(Console.ReadLine());

        foreach (CardHolder h in getList())
        {
            if (h.getCardnumber().Equals(accountnum) && h.getPin() == pin)
            {
                return h;
            }
        }
        return null;
    }

    public void transfer(CardHolder currentuser)
    {
        Console.WriteLine("Recepient: ");
        String targetAccount = Console.ReadLine();

        Console.WriteLine("Please enter an amount you want transfer: R");
        double transfermoney = double.Parse(Console.ReadLine());

        if (currentuser.getBalance() > transfermoney && transfermoney != 0)
        {
            foreach (CardHolder acc in getList())
            {
                if (acc.getCardnumber().Equals(targetAccount))
                {
                    currentuser.setBalance(currentuser.getBalance() - transfermoney);
                    acc.setBalance(acc.getBalance() + transfermoney);

                    Console.WriteLine("you transferring R" + transfermoney + " sent to: " + acc.getCardnumber());
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("You cannot transfer that amount as you do not have sufficient balance in your account!");
        }
    }

    public void deposit(CardHolder currentUser)
    {
        Console.WriteLine(" please enter an amount you want to deposit");
        double deposit = double.Parse(Console.ReadLine());
        currentUser.setBalance(currentUser.getBalance() + deposit);
        Console.WriteLine(" thank you for depositing: your current balance is " + currentUser.getBalance());
    }

    public void withdrawal(CardHolder currentuser)
    {
        Console.WriteLine(" please enter an amount you want to withdraw");
        double withdrawal = double.Parse(Console.ReadLine());
        /// checks if the user have enough money
        if (currentuser.getBalance() > withdrawal)
        {
            currentuser.setBalance(currentuser.getBalance() - withdrawal);
            Console.WriteLine("your total balance is " + currentuser.getBalance());
            Console.WriteLine("thank you for using this machine ");
        }
        else
        {
            Console.WriteLine("insufffient funds");
        }
    }
    public void displayOptions()
    {
        try
        {

            Console.WriteLine("please choose from the menu");
            Console.WriteLine("1: show balance");
            Console.WriteLine("2: send Money");
            Console.WriteLine("3: deposit");
            Console.WriteLine("4: withdrawal");
            Console.WriteLine("5: exit");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void balance(CardHolder currentuser)
    {
        Console.WriteLine("your total balance is " + currentuser.getBalance());
        Console.ReadLine();
    }

    public static void Main(String[] args)
    {
        //transfer money
        CardHolder ch = new CardHolder();
        CardHolder holder;

        // PROMPT USER
        Console.WriteLine(" welcome to our Banking APP ");

        if ((holder = ch.login()) != null)
        {
            while (holder != null)
            {
                ch.displayOptions();
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        ch.balance(holder);
                        break;
                    case 2:
                        ch.transfer(holder);
                        foreach (CardHolder n in ch.getList())
                        {
                            Console.WriteLine(n.getCardnumber() + " <=> " + n.getBalance());
                        }
                        break;
                    case 3:
                        ch.deposit(holder);
                        break;
                    case 4:
                        ch.withdrawal(holder);
                        break;
                    case 5:
                        holder = null;
                        Console.WriteLine("thank you for using this machine...");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid login details....");
        }

    }
}
