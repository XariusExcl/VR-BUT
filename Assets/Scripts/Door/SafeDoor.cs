using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SafeDoor : MonoBehaviour
{
    // Refs
    [Header("Refs")]
    HingeJoint hinge;

    // Variables
    [Header("Variables")]
    public bool isLocked;
    JointLimits doorLimits;
    JointLimits closedDoorLimits;

    // Events
    public UnityEvent DoorUnlock;

    void Start()
    {
        // Cache des components
        hinge = GetComponent<HingeJoint>();

        // Création des contraites physiques "porte ouverte" et "porte fermée"
        doorLimits = new JointLimits();
        doorLimits.max = hinge.limits.max;
        doorLimits.min = hinge.limits.min;;

        closedDoorLimits = new JointLimits();
        closedDoorLimits.min = closedDoorLimits.max = doorLimits.min;

        // Initialisation
        isLocked = true;
        UpdateDoor();
    }

    void UpdateDoor()
    {
        if (isLocked) {
            hinge.limits = closedDoorLimits;
        } else {
            hinge.limits = doorLimits;
        }
    }

    public void UnlockDoor()
    {
        // Todo : sfx?
        isLocked = false;
        DoorUnlock.Invoke();
        UpdateDoor();
    }
}
