/*
*   Reads UI changes and enables/disables components accordingly.
*   
*   Todo : save and read from usersettings
*/

using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class SettingsApplier : MonoBehaviour
{
    [Header("Références Scène")]
    public GameObject leftRayTeleportationInteractor;
    public GameObject rightRayTeleportationInteractor;
    
    ActionBasedContinuousMoveProvider continuousMoveProvider;
    ActionBasedContinuousTurnProvider continuousTurnProvider;
    ActionBasedSnapTurnProvider snapTurnProvider;
    TeleportationProvider teleportationProvider;
    
    void Start()
    {
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        continuousTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
        snapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
        teleportationProvider = GetComponent<TeleportationProvider>();

        SetLocomotionMode(0);
        SetTurnMode(0);
    }

    public void SetLocomotionMode(int value)
    {
        switch (value) {
            case 0:
                continuousMoveProvider.enabled = true;
                teleportationProvider.enabled = false;
                leftRayTeleportationInteractor.SetActive(false);
                rightRayTeleportationInteractor.SetActive(false);
            break;
            case 1:
                teleportationProvider.enabled = true;
                leftRayTeleportationInteractor.SetActive(true);
                rightRayTeleportationInteractor.SetActive(true);
                continuousMoveProvider.enabled = false;
            break;
        }
    }

    public void SetTurnMode(int value)
    {
        switch (value) {
            case 0:
                continuousTurnProvider.enabled = true;
                snapTurnProvider.enabled = false;
            break;
            case 1:
                snapTurnProvider.enabled = true;
                continuousTurnProvider.enabled = false;
            break;
        }
    }
}
