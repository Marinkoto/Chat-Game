using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyManager
{
    public static void RecieveCurrency(int amount, PlayerData playerData)
    {
        playerData.currency += amount;
    }
    public static void RemoveCurrency(int amount,PlayerData playerData)
    {
        playerData.currency -= amount;
    }
    public static bool HasCurrency(PlayerData playerData,int amount)
    {
        if (playerData.currency < amount)
        {
            return false;
        }
        return true;
    }
}
