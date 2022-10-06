using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CustomTeleportation : MonoBehaviour {

    public InputActionProperty initiateTeleportAction;
    public InputActionProperty cancelTeleportAction;
    public XRRayInteractor interactorRay;

    void Start()
    {
        initiateTeleportAction.action.performed += _ => ShowTeleportationRay();
        initiateTeleportAction.action.canceled += _ => HideTeleportationRayDelayed();
        cancelTeleportAction.action.performed += _ => HideTeleportationRay();
    }

    void ShowTeleportationRay()
    {
        interactorRay.enabled = true;
    }

    void HideTeleportationRay()
    {
        interactorRay.enabled = false;
    }

    void HideTeleportationRayDelayed()
    {
        StartCoroutine(DelayHideTeleportation());
    }

    IEnumerator DelayHideTeleportation(){
        yield return new WaitForEndOfFrame();
        HideTeleportationRay();
    }

    void OnDestroy()
    {
        initiateTeleportAction.action.performed -= _ => ShowTeleportationRay();
        initiateTeleportAction.action.canceled -= _ => HideTeleportationRayDelayed();
        cancelTeleportAction.action.performed -= _ => HideTeleportationRay();
    }
}