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

        lockedLimits = new JointLimits
        {
            min = -20f,
            max = 0f
        };

        unlockedLimits = new JointLimits
        {
            min = -70f,
            max = 0f
        };
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

    public bool IsAtBottomLimit()
    {
        return hinge.angle <= lockedLimits.min;
    }
}
