using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class OnDoorUnlockEventArgs : EventArgs
{
    public bool isKeyInLock;
}

public class DoorBehaviour : MonoBehaviour
{
    // Refs
    [Header("Refs")]
    public DoorHandle doorHandle;
    public DoorHandle secondDoorHandle;
    public DoorLock[] doorLocks;
    Rigidbody rb;
    HingeJoint hinge;

    // Variables
    [Header("Variables")]
    float doorAngle;
    public bool isLocked;
    bool isOpened;
    public float doorAngleThreshold;
    public float handleAngleThreshold;
    public int lockID;
    JointLimits doorLimits;
    JointLimits closedDoorLimits;

    // Evente
    public UnityEvent DoorUnlock;

    public static event EventHandler<OnDoorUnlockEventArgs> TriedOpeningDoorEvent;

    void Start()
    {
        // Cache des components
        rb = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();

        // Création des contraites physiques "porte ouverte" et "porte fermée"
        doorLimits = new JointLimits();
        doorLimits.max = hinge.limits.max;
        doorLimits.min = hinge.limits.min;

        closedDoorLimits = new JointLimits();
        closedDoorLimits.min = closedDoorLimits.max = (doorLimits.min + doorLimits.max) / 2f;

        // Initialisation
        isOpened = false;
        UpdateDoor();
    }

    void Update()
    {
        // Lire l'angle absolu (sans signe) de la porte, de la poignée
        doorAngle = Mathf.Abs(hinge.angle);

        // Debug.Log($"doorAngle {doorAngle}");
        // Debug.Log($"handleAngle {handleAngle}");

        // Si la porte n'est pas verrouillée
        if (!isLocked)
        {
            // Si la porte est fermée
            if (!isOpened) {
                if (HandlesBelowThreshold()) // Si l'angle de nos poignées passent notre limite
                {
                    // Ouvrir la porte
                    isOpened = true;
                    UpdateDoor();
                }
            } else { // Si la porte est ouverte
                if (!HandlesHeld() && doorAngle < doorAngleThreshold) // Si la porte n'est pas tenue, et est proche de son angle "fermé"
                {
                    // Fermer la porte
                    isOpened = false;
                    UpdateDoor();
                }
            }
        } else {
            if (HandlesHeld() && (doorHandle.IsAtBottomLimit() || (secondDoorHandle != null && secondDoorHandle.IsAtBottomLimit())))
            {
                TriedOpeningDoorEvent?.Invoke(this, new OnDoorUnlockEventArgs() { isKeyInLock = IsKeyInLock() });
            }
        }
    }

    bool HandlesBelowThreshold()
    {
        // Vérifier l'angle de la seconde poignée que si elle est définie
        return doorHandle.GetAngle() > handleAngleThreshold || (secondDoorHandle != null && secondDoorHandle.GetAngle() > handleAngleThreshold);
    }

    bool HandlesHeld()
    {
        return doorHandle.IsHeld() || (secondDoorHandle != null && secondDoorHandle.IsHeld());
    }

    void UpdateDoor()
    {
        if (isOpened) {
            hinge.limits = doorLimits;
        } else {
            hinge.limits = closedDoorLimits;
        }

        doorHandle.UpdateHandle(isOpened);
    }

    public void CheckIfCorrectKey(int ID)
    {
        if (ID == lockID) {
            UnlockDoor();
        }
    }

    public void UnlockDoor()
    {
        // Todo : sfx?
        isLocked = false;
        DoorUnlock.Invoke();
    }

    public bool IsKeyInLock()
    {
        foreach (DoorLock doorLock in doorLocks)
        {
            if (doorLock.isKeyInLock)
                return true;
        }
        return false;
    }
}
