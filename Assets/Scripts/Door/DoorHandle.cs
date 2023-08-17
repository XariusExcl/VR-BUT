using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : MonoBehaviour
{
    HingeJoint hinge;
    XRGrabInteractable grabInteractable;
    JointLimits lockedLimits;
    JointLimits unlockedLimits;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        lockedLimits = new JointLimits();
        lockedLimits.min = -20f;
        lockedLimits.max = 0f;

        unlockedLimits = new JointLimits();
        unlockedLimits.min = -70f;
        unlockedLimits.max = 0f;
    }

    public void UpdateHandle(bool isOpened)
    {
        if (isOpened)
            hinge.limits = unlockedLimits;
        else
            hinge.limits = lockedLimits;
    }

    public float GetAngle()
    {
        return Mathf.Abs(hinge.angle);
    }

    public bool IsHeld()
    {
        return grabInteractable.isSelected;
    }
}
