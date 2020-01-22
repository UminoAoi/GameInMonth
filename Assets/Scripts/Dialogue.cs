using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour
{
    public Button buttonYes;
    public Button buttonNo;
    public TextMeshProUGUI text;
    public NPC npc;

    // Start is called before the first frame update
    void Start()
    {
        SetAllInactive();
    }

    public void SetAllInactive()
    {
        buttonNo.gameObject.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        SetDialogueVisible(false);
    }

    public void SetButtonsInactive()
    {
        buttonNo.gameObject.SetActive(false);
        buttonYes.gameObject.SetActive(false);
    }

    public void SetDialogueVisible(bool set)
    {
        gameObject.SetActive(set);
    }

    public void SetText(string textToWrite)
    {
        text.text = textToWrite;
    }

    public void SetButtonYes(ButtonAction action)
    {
        buttonYes.onClick.RemoveAllListeners();
        buttonYes.gameObject.SetActive(true);
        switch (action)
        {
            case ButtonAction.Next:
                buttonYes.onClick.AddListener(delegate {
                    npc.NextDialogue();
                });
                break;
            case ButtonAction.AcceptQuest:
                buttonYes.onClick.AddListener(delegate {
                    npc.GiveQuest();
                    SetAllInactive();
                });
                break;
        }
    }

    public void SetButtonNo(ButtonAction action)
    {
        buttonNo.onClick.RemoveAllListeners();
        buttonNo.gameObject.SetActive(true);
        switch (action)
        {
            case ButtonAction.Close:
                buttonNo.onClick.AddListener(delegate {
                    npc.Reset();
                    SetAllInactive();
                });
                break;
        }
    }
}

public enum ButtonAction
{
    Next,
    AcceptQuest,
    Close
}