
using UnityEngine;

public static class Convert
{
    public static string BalanceToCurrency(int balance)
    {
        float f = balance / 100f;
        return f + "€";
    }

    public static int CurrencyToBalance(float currency)
    {
        return Mathf.RoundToInt(currency * 100f);
    }
}
