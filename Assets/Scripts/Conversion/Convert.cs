
public static class Convert
{
    public const int BALANCE_PER_CURRENCY = 100; // 100 'balance' per currency (euro, pound, whatever)
    public const char CURRENCY_POINT_SEPARATOR = '.';
    public const char CURRENCY_SYMBOL = '€';

    public static string BalanceToCurrency(int balance)
    {
        int wholes = 0;
        while(balance >= BALANCE_PER_CURRENCY)
        {
            // This loop is probably really stupid, but I like it!
            balance -= BALANCE_PER_CURRENCY;
            wholes++;
        }

        int remainder = balance;

        string whole = wholes.ToString();
        string dec = "";
        if(remainder != 0)
        {
            dec = CURRENCY_POINT_SEPARATOR + ((int)((remainder / BALANCE_PER_CURRENCY) * 100)).ToString();
        }
        string sign = " " + CURRENCY_SYMBOL;

        return  whole + dec + sign;
    }
}
