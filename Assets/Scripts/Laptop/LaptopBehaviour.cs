using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaptopBehaviour : MonoBehaviour
{
    public TMP_Text interactableText;
    public Outline outline;
    public CodeInputManager codeInputManager;
    public DoorBehaviour leftDoor;
    public DoorBehaviour rightDoor;

    public enum State { LockScreen, EnteringCode, Unlocked };
    public State state = State.LockScreen;

    void Start()
    {
        interactableText.text = "Entrer le code";
        outline.enabled = false;
    }

    public void OnHoveredEnter()
    {
        if (state == State.LockScreen)
        {
            outline.enabled = true;
            interactableText.gameObject.SetActive(true);
        }
    }

    public void OnHoveredExit()
    {
        if (state == State.LockScreen)
        {
            outline.enabled = false;
            interactableText.gameObject.SetActive(false);
        }
    }

    public void OnSelected()
    {
        if (state == State.LockScreen)
        {
            state = State.EnteringCode;
            outline.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;

            codeInputManager.EnableButtons();
        }
    }

    public void UnlockComputer()
    {
        state = State.Unlocked;
        leftDoor.UnlockDoor();
        rightDoor.UnlockDoor();
    }

    public void SendDebugMessage()
    {
        Debug.Log(gameObject.name);
    }
}
