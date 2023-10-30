using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LaptopBehaviour : MonoBehaviour
{
    public GameObject interactableText;
    public Outline outline;
    public CodeInputManager codeInputManager;

    public GameObject lockScreen;
    public GameObject codeInputScreen;

    public enum State { LockScreen, EnteringCode, Unlocked };
    public State state = State.LockScreen;

    public UnityEvent LaptopUnlock;

    void Start()
    {
        interactableText.SetActive(false);
        lockScreen.SetActive(true);
        codeInputScreen.SetActive(false);
        outline.enabled = false;
    }

    public void OnHoveredEnter()
    {
        if (state == State.LockScreen)
        {
            outline.enabled = true;
            interactableText.SetActive(true);
        }
    }

    public void OnHoveredExit()
    {
        if (state == State.LockScreen)
        {
            outline.enabled = false;
            interactableText.SetActive(false);
        }
    }

    public void OnSelected()
    {
        if (state == State.LockScreen)
        {
            state = State.EnteringCode;
            lockScreen.SetActive(false);
            codeInputScreen.SetActive(true);
            interactableText.SetActive(false);
            outline.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;

            codeInputManager.EnableButtons();
        }
    }

    public void UnlockComputer()
    {
        state = State.Unlocked;
        LaptopUnlock.Invoke();
    }

    public void SendDebugMessage()
    {
        Debug.Log(gameObject.name);
    }
}
