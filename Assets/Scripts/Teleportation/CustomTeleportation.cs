using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CustomTeleportation : MonoBehaviour {

    public InputActionProperty initiateTeleportAction;
    public InputActionProperty cancelTeleportAction;
    public XRRayInteractor interactorRay;
    public GameObject reticle;

    void Start()
    {
        initiateTeleportAction.action.performed += _ => ShowTeleportationRay();
        initiateTeleportAction.action.canceled += _ => HideTeleportationRayDelayed();
        cancelTeleportAction.action.performed += _ => HideTeleportationRay();
    }

    void ShowTeleportationRay()
    {
        if (gameObject.activeSelf)
        {
            interactorRay.enabled = true;
            reticle.SetActive(true);
        }
    }

    void HideTeleportationRay()
    {
        if (gameObject.activeSelf)
        {
            interactorRay.enabled = false;
            reticle.SetActive(false);
        }
    }

    void HideTeleportationRayDelayed()
    {
        if (gameObject.activeSelf)
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