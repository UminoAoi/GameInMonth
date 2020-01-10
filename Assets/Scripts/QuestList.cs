using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList
{
    public static int currentQuest = 0;
    static Quest[] quests = {
        new Quest("Yellow balls are pretty difficult to find. Please find 5 of them.", new Dictionary<CollectableNames, int>(){
                                                                                                { CollectableNames.YellowBall, 5}
                                                                                        }),
        new Quest("Green balls are really stinky. Please find 3 of them.", new Dictionary<CollectableNames, int>(){
                                                                                                { CollectableNames.GreenBall, 3}
                                                                                        }),
    };

    public static string GetNextDescription()
    {
        return quests[currentQuest].GetDescription();
    }
    
    public static Quest GetNextQuest()
    {
        Quest newQuest = quests[currentQuest];

        if (currentQuest + 1 > quests.Length)
            currentQuest = 0;
        else
            currentQuest++;

        return newQuest;
    }
}

public enum CollectableNames
{
    YellowBall,
    GreenBall
}
