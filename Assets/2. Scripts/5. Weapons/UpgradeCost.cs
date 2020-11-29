using System.Collections;

public static class UpgradeCost
{
    public static Hashtable GetCosts()
    {
        Hashtable costs = new Hashtable();

        costs.Add(1, 500);
        costs.Add(2, 1000);
        costs.Add(3, 1200);
        costs.Add(4, 1500);
        costs.Add(5, 1700);
        costs.Add(6, 2000);
        costs.Add(7, 2500);
        costs.Add(8, 3000);
        costs.Add(9, 4000);
        costs.Add(10, 5000);

        return costs;
    }

    public static Hashtable GetStats()
    {
        Hashtable costs = new Hashtable();

        costs.Add(1, 150);
        costs.Add(2, 200);
        costs.Add(3, 350);
        costs.Add(4, 400);
        costs.Add(5, 500);
        costs.Add(6, 600);
        costs.Add(7, 750);
        costs.Add(8, 800);
        costs.Add(9, 900);
        costs.Add(10, 1000);

        return costs;
    }

    public static Hashtable GetDefenceUpgrades()
    {
        Hashtable costs = new Hashtable();

        costs.Add(1, 500);
        costs.Add(2, 600);
        costs.Add(3, 700);
        costs.Add(4, 800);
        costs.Add(5, 900);
        costs.Add(6, 1000);
        costs.Add(7, 2000);
        costs.Add(8, 3000);
        costs.Add(9, 4000);
        costs.Add(10, 5000);

        return costs;
    }

    public static Hashtable GetDefenceStats()
    {
        Hashtable costs = new Hashtable();

        costs.Add(1, 110);
        costs.Add(2, 120);
        costs.Add(3, 130);
        costs.Add(4, 140);
        costs.Add(5, 150);
        costs.Add(6, 160);
        costs.Add(7, 170);
        costs.Add(8, 180);
        costs.Add(9, 190);
        costs.Add(10, 200);

        return costs;
    }
}