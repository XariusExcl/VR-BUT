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
    [Header("Références IU")]
    public TMP_Dropdown rotationMode;
    public TMP_Dropdown movementMode;
    
    [Header("Références Scène")]
    public TeleportationProvider teleportationProvider;
    public GameObject leftRayTeleportationInteractor;
    public GameObject rightRayTeleportationInteractor;
    
    [Space(10)]
    
    public ActionBasedContinuousMoveProvider continuousMoveProvider;
    public ActionBasedContinuousTurnProvider continuousTurnProvider;
    public ActionBasedSnapTurnProvider snapTurnProvider;
    
    void Start()
    {
        ApplySettingsToObjects();
    }

    public void ApplySettingsToObjects()
    {
        Debug.Log($"RotationMode {rotationMode.value}, MovementMode {movementMode.value}");
        
            // Movement mode 0 = Continu, 1 = Téléportation
        teleportationProvider.enabled = movementMode.value == 1;
        leftRayTeleportationInteractor.SetActive(movementMode.value == 1);
        rightRayTeleportationInteractor.SetActive(movementMode.value == 1);
        continuousMoveProvider.enabled = movementMode.value == 0;
    
            // Rotation mode 0 = Continu, 1 = à-coups
        continuousTurnProvider.enabled = rotationMode.value == 0;
        snapTurnProvider.enabled = rotationMode.value == 1;
    
        Debug.Log("Settings Applied!");
    }
}
