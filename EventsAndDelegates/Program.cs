using System;
using System.Collections.Generic;

public delegate void TableOpenDelegate(object sender, TableEventArgs e);
public delegate void MealChangeDelegate(object sender, MealChangeEventArgs e);

public class TableEventArgs : EventArgs
{
    public TableEventArgs()
    {

    }
}

public class MealChangeEventArgs : EventArgs
{
    public Customer customer;

    public MealChangeEventArgs(Customer c)
    {
        this.customer = c;
    }
}


//•	Meal (use an enum - public enum Meals { none, appetizer, main, desert, done })
public enum Meal
{
    none, appetizer, main, desert, done
}


//Create a table class that has the following properties
public class Table
{
    //•	An event that will notify when the table is open and write "Table is open!" to the console
    public event TableOpenDelegate TableOpenEvent;

    public void OpenTable()
    {
        Console.WriteLine("Table is open!");

        if (TableOpenEvent != null)
        {
            TableOpenEvent(this, new TableEventArgs());
        }

    }
}


//Create a customer class that has the following properties:
public class Customer
{
    public event MealChangeDelegate MealChangeEvent;

    //•	First Name
    public string FirstName { get; set; }

    //•	Last Name
    public string LastName { get; set; }

    //•	Meal
    public Meal meal { get; private set; }


    //•	An event that fires when the customer changes from 1 meal to the next (e.g. from none to appetizer …)
    public void nextMeal(Meal m)
    {
        switch (m)
        {
            case Meal.none:
                this.meal = Meal.appetizer;
                if (this.MealChangeEvent != null)
                {
                    //fire the event.
                    MealChangeEvent(this, new MealChangeEventArgs(this));
                }
                break;
            case Meal.appetizer:
                this.meal = Meal.main;
                if (this.MealChangeEvent != null)
                {
                    //fire the event.
                    MealChangeEvent(this, new MealChangeEventArgs(this));
                }
                break;
            case Meal.main:
                this.meal = Meal.desert;
                if (this.MealChangeEvent != null)
                {
                    //fire the event.
                    MealChangeEvent(this, new MealChangeEventArgs(this));
                }
                break;
            case Meal.desert:
                this.meal = Meal.done;
                if (this.MealChangeEvent != null)
                {
                    //fire the event.
                    MealChangeEvent(this, new MealChangeEventArgs(this));
                }
                break;
        }
    }


    public Customer()
    {
    }

    //•	A method to listen for a table open event.  This method should write "{0} {1} got a table.", 
    public void TableOpenEventHandler(object sender, TableEventArgs e)
    {
        Console.WriteLine("{0} {1} got a table", this.FirstName, this.LastName);
    }
}

public class Program
{
    //•	Create a method that writes: {0} {1} is having {2}.", e.customer.firstName, e.customer.lastName, e.customer.meal to the console when a customer’s meal changes
    public static void mealChangeEventHandler(object sender, MealChangeEventArgs e)
    {
        Console.WriteLine("{0} {1} is having {2}", e.customer.FirstName, e.customer.LastName, e.customer.meal);
    }

    public static void Main(string[] args)
    {
        // •	Create a table object
        Table table = new Table();


        // In your Main method:
        // •	Create a queue of customers(at least 5).

        Queue<Customer> customers = new Queue<Customer>();
        Customer c1 = new Customer();
        Customer c2 = new Customer();
        Customer c3 = new Customer();
        Customer c4 = new Customer();
        Customer c5 = new Customer();
        Customer c6 = new Customer();


        c1.FirstName = "Jeo";
        c1.LastName = "Smith";
        c1.MealChangeEvent += mealChangeEventHandler;

        c2.FirstName = "Jane";
        c2.LastName = "Jones";
        c2.MealChangeEvent += mealChangeEventHandler;

        c3.FirstName = "Jack";
        c3.LastName = "Jump";
        c3.MealChangeEvent += mealChangeEventHandler;

        c4.FirstName = "Jeff";
        c4.LastName = "Run";
        c4.MealChangeEvent += mealChangeEventHandler;

        c5.FirstName = "Jill";
        c5.LastName = "Hill";
        c5.MealChangeEvent += mealChangeEventHandler;

        c6.FirstName = "John";
        c6.LastName = "Winstone";
        c6.MealChangeEvent += mealChangeEventHandler;


        customers.Enqueue(c1);
        customers.Enqueue(c2);
        customers.Enqueue(c3);
        customers.Enqueue(c4);
        customers.Enqueue(c5);
        customers.Enqueue(c6);


        //•	Loop through each of the customers and have them go through all meals for the customer
        foreach (Customer cust in customers)
        {
            table.TableOpenEvent += cust.TableOpenEventHandler;
            table.OpenTable();
            while (cust.meal != Meal.done)
            {
                cust.nextMeal(cust.meal);
            }
            table.TableOpenEvent -= cust.TableOpenEventHandler;
        }

        //•	When all customers have completed their meals the program should write "Everyone is Full!" to the console.
        Console.WriteLine("Everyone is full!");


        Console.ReadLine();
    }


}
