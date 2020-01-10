using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager
{
    static int playerLife = 3;
    static Dictionary<CollectableNames, int> inventory = new Dictionary<CollectableNames, int>();
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

    public static void AddQuest(Quest quest)
    {
        Debug.Log("Added Quest");
        playersQuests.Add(quest);
    }

    public static void AddItems(CollectableNames objectThing, int number)
    {
        if (inventory.ContainsKey(objectThing))
        {
            inventory[objectThing] = inventory[objectThing] + number;
        }
        else
        {
            inventory.Add(objectThing, number);
        }
        Debug.Log("Added item");
    }

    public static bool CheckInventory(CollectableNames objectThing, int number)
    {
        if (inventory.ContainsKey(objectThing) && inventory[objectThing] == number)
            return true;
        else
            return false;
    }

    public static int GetInventorySize()
    {
        return inventory.Count;
    }

    public static Dictionary<CollectableNames, int> GetInventory()
    {
        return inventory;
    }

}
