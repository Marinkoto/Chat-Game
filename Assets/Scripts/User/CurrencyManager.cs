using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyManager
{
    public static void RecieveCurrency(int amount, UserData playerData)
    {
        playerData.currency += amount;
    }
    public static void RemoveCurrency(int amount,UserData playerData)
    {
        playerData.currency -= amount;
    }
    public static bool HasCurrency(UserData playerData,int amount)
    {
        if (playerData.currency < amount)
        {
            return false;
        }
        return true;
    }
}
