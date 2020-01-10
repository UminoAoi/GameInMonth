using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    public List<string> texts;
    public List<string> thankTexts;

    bool isActiveQuest = false;
    int currentText = 0;
    int currentThankText = 1;
    Quest activeQuest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActiveQuest) {
            dialogue.SetText(texts[currentText]);
            dialogue.SetDialogueVisible(true);
            dialogue.SetButtonYes(ButtonAction.Next);
        }
        else
        {
            if (!activeQuest.checkIfDone())
            {
                dialogue.SetText(thankTexts[0]);
                dialogue.SetDialogueVisible(true);
                dialogue.SetButtonNo(ButtonAction.Close);
            }
            else
            {
                dialogue.SetText(thankTexts[currentThankText]);
                dialogue.SetDialogueVisible(true);
                dialogue.SetButtonYes(ButtonAction.Next);
            }
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Reset();
        dialogue.SetAllInactive();
    }

    public void NextDialogue()
    {
        dialogue.SetButtonsInactive();
        if (!isActiveQuest)
        {
            if (currentText + 1> texts.Count)
            {
                dialogue.SetText(texts[++currentText]);
                dialogue.SetButtonYes(ButtonAction.Next);
            }
            else
            {
                dialogue.SetText(QuestList.GetNextDescription());
                dialogue.SetButtonYes(ButtonAction.AcceptQuest);
                dialogue.SetButtonNo(ButtonAction.Close);
            }
        }
        else
        {
            if (currentThankText + 1> thankTexts.Count)
            {
                dialogue.SetText(thankTexts[++currentThankText]);
                dialogue.SetButtonYes(ButtonAction.Next);
            }
            else
            {
                FinishQuest();
            }
        }
    }

    public void GiveQuest()
    {
        activeQuest = QuestList.GetNextQuest();
        PlayerStateManager.AddQuest(activeQuest);
        isActiveQuest = true;
    }

    public void FinishQuest()
    {
        dialogue.SetText(thankTexts[++currentThankText]);
        dialogue.SetButtonNo(ButtonAction.Close);
        activeQuest = null;
        isActiveQuest = false;
    }

    public void Reset()
    {
        if (!isActiveQuest)
        {
            Debug.Log("Reseting");
            isActiveQuest = false;
            currentText = 0;
            currentThankText = 1;
            activeQuest = null;
        }
    }
}
