using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CodeInputManager : MonoBehaviour
{
    public LaptopBehaviour laptop;
    
    public GameObject selector;
    int selectorPosition = 0;
    int[] code = new int[3]{7, 3, 5};
    int[] input = new int[3]{0, 0, 0};

    public TMP_Text[] numberDisplays;

    public Collider plusButton;
    public Collider minusButton;
    public Collider enterButton;

    public void PlusButtonPressed()
    {
        input[selectorPosition] += 1;
        input[selectorPosition] %= 10;
        numberDisplays[selectorPosition].text = input[selectorPosition].ToString();
    }

    public void MinusButtonPressed()
    {
        input[selectorPosition] -= 1;
        if (input[selectorPosition] < 0)
            input[selectorPosition] = 9;
        numberDisplays[selectorPosition].text = input[selectorPosition].ToString();
    }

    public void EnterButtonPressed()
    {
        if(selectorPosition < 2)
        {
            selectorPosition++;
            selector.transform.localPosition += new Vector3(45f, 0, 0);
        }
        else
        {
            if(input[0] == code[0] && input[1] == code[1] && input[2] == code[2])
            {
                ValidCode();
                laptop.UnlockComputer();
            }
            else
            {
                WrongCode();
            }
        }
    }

    public void EnableButtons()
    {
        plusButton.enabled = true;
        minusButton.enabled = true;
        enterButton.enabled = true;

        int i = 0;
        foreach(TMP_Text numberDisplay in numberDisplays)
        {
            numberDisplay.text = input[i].ToString();
            numberDisplay.color = Color.white;
            i++;
        }

        selectorPosition = 0;
        selector.SetActive(true);
        selector.transform.localPosition = new Vector3(-45f, -1.5f, 0);
    }

    public void DisableButtons()
    {
        plusButton.enabled = false;
        minusButton.enabled = false;
        enterButton.enabled = false;
    }

    void WrongCode()
    {
        // idk add a beep sound
        
        DisableButtons();

        selector.SetActive(false);
        foreach(TMP_Text numberDisplay in numberDisplays)
        {
            numberDisplay.text = "X";
            numberDisplay.color = Color.red;
        }
        Invoke("EnableButtons", 5f);
    }

    void ValidCode()
    {
        // add a beep sound

        DisableButtons();
        selector.SetActive(false);
        foreach(TMP_Text numberDisplay in numberDisplays)
        {
            numberDisplay.color = Color.green;
        }
    }
}
