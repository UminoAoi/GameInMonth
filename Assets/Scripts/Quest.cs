using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    string description;
    Dictionary<CollectableNames, int> goals;

    public Quest(string description, Dictionary<CollectableNames, int> goals)
    {
        this.description = description;
        this.goals = goals;
    }

    public bool checkIfDone()
    {
        if (PlayerStateManager.GetInventorySize() == 0)
            return false;

        foreach(KeyValuePair<CollectableNames,int> pair in goals)
        {
            if (!PlayerStateManager.CheckInventory(pair.Key, pair.Value))
                return false;
        }
        return true;
    }

    public string GetDescription()
    {
        return description;
    }
}
