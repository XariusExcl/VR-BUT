using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecretSettingsMenuBehaviour : MonoBehaviour
{
    public InputActionProperty toggleMenuAction;
    public InputActionProperty validateAction;
    
    [Space(10)]

    public GameObject xrOrigin;
    public GameObject locomotionSystem;
    public GameObject colliders;

    [Space(10)]

    public GameObject settingsMenu;
    public GameObject trackingSettingsMenu;
    public GameObject calibrationMenu;
    bool _isMenuOpened = false;
    bool _isCalibrating = false;
    public GameObject calibrationCylinder;

    void Start()
    {
        toggleMenuAction.action.performed += ctx => ToggleMenuAction();
        validateAction.action.performed += ctx => ValidateAction();
    }

    void ToggleMenuAction()
    {
        if (!_isCalibrating) {
            _isMenuOpened = !_isMenuOpened;
            settingsMenu.SetActive(_isMenuOpened);
        }
    }

    public void SetStaticMovementMode()
    {
        colliders.SetActive(true);
        locomotionSystem.SetActive(true);
    }

    public void StartCalibration()
    {
        Debug.Log("Calibration started");
        _isCalibrating = true;

        trackingSettingsMenu.SetActive(false);
        calibrationMenu.SetActive(true);
        colliders.SetActive(false);
        locomotionSystem.SetActive(false);
        calibrationCylinder.SetActive(true);
    }

    public void CancelCalibration()
    {
        Debug.LogWarning("Calibration cancelled!");
        _isCalibrating = false;

        trackingSettingsMenu.SetActive(true);
        calibrationMenu.SetActive(false);

        SetStaticMovementMode();
    }

    void ValidateAction()
    {
        if (_isCalibrating)
        {
            _isCalibrating = false;

            xrOrigin.transform.position = calibrationCylinder.transform.position;
            xrOrigin.transform.rotation = Quaternion.Euler(0f, -Camera.main.transform.localRotation.eulerAngles.y, 0f);
            
            trackingSettingsMenu.SetActive(true);
            calibrationMenu.SetActive(false);
            calibrationCylinder.SetActive(false);
        }
    }
}
