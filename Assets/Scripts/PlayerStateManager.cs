using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager
{
    static int playerLife = 3;
    static Dictionary<string, int> inventory = new Dictionary<string, int>();
    static List<Quest> playersQuests = new List<Quest>();

    public static int GetLife()
    {
        return playerLife;
    }

    public static void AddLife()
    {
        playerLife++;
    }

    public static void SubstractLife(int minus)
    {
        playerLife = playerLife - minus;
    }

}
