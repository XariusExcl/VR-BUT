using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class DuckyScript : XRGrabInteractable
{
    [Header("Custom Properties")]
    [Space(10)]
    public TMP_Text text;
    private int frameNumber = 0;

    void Update()
    {
        frameNumber++;
    }

    // IsHoverableBy(XRBaseInteractor interactor);

    // IsSelectableBy(XRBaseInteractor interactor);
    

    protected override void OnHoverEntering(HoverEnterEventArgs args)
    {
        base.OnHoverEntering(args);
        Debug.Log($"HoverEntering {frameNumber}");
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        Debug.Log($"HoverEntered {frameNumber}");
    }

    protected override void OnHoverExiting(HoverExitEventArgs args)
    {
        base.OnHoverExiting(args);
        Debug.Log($"HoverExiting {frameNumber}");
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        Debug.Log($"HoverExited {frameNumber}");
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        Debug.Log($"SelectEntering {frameNumber}");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log($"SelectEntered {frameNumber}");
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        Debug.Log($"SelectExiting {frameNumber}");
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Debug.Log($"SelectExited {frameNumber}");
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        Debug.Log($"Activated {frameNumber}");
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        Debug.Log($"Deactivated {frameNumber}");
    }

}
