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
    Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();

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
            rb.isKinematic = true;
        } else {
            hinge.limits = doorLimits;
            rb.isKinematic = false;
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
