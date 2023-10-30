using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public TMP_Text timerText;

    void Start(){
        TimeSpan t = TimeSpan.FromSeconds(GameManager.completionTime);
        string timeString = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

        timerText.text = "Temps de complétion : " + timeString;
    }

    public void ExitButtonClicked(){
        GameManager.ExitGame();
    }
}
