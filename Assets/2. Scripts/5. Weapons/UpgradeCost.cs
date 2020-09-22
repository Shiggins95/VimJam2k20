using System.Collections;

public static class UpgradeCost
{
    public static Hashtable GetCosts()
    {
        Hashtable costs = new Hashtable();

        costs.Add(1, 1000);
        costs.Add(2, 2000);
        costs.Add(3, 3000);
        costs.Add(4, 4000);
        costs.Add(5, 5000);
        costs.Add(6, 6000);
        costs.Add(7, 7000);
        costs.Add(8, 8000);
        costs.Add(9, 9000);
        costs.Add(10, 10000);

        return costs;
    }

    public static Hashtable GetStats()
    {
        Hashtable costs = new Hashtable();

        costs.Add(1, 200);
        costs.Add(2, 500);
        costs.Add(3, 1000);
        costs.Add(4, 2000);
        costs.Add(5, 4000);
        costs.Add(6, 8000);
        costs.Add(7, 16000);
        costs.Add(8, 32000);
        costs.Add(9, 128000);
        costs.Add(10, 250000);

        return costs;
    }
}