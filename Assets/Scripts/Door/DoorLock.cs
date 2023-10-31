using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock : MonoBehaviour
{
    XRSocketInteractor socketInteractor;
    DoorBehaviour door;
    DoorKey lastKeyInserted;
    public bool isKeyInLock;
    public bool isCorrectKey;

    void Start()
    {
        door = GetComponentInParent<DoorBehaviour>();
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    public void OnKeyInserted()
    {
        if (socketInteractor.selectTarget.TryGetComponent<DoorKey>(out DoorKey key))
        {
            lastKeyInserted = key;
            lastKeyInserted.ToggleCollider(false);
            CheckIfCorrectKey(key.ID);
        }
        else Debug.LogWarning("Object inserted in lock is not a Door Key!");
    }

    public void OnKeyRemoved()
    {
        lastKeyInserted.ToggleCollider(true);
        lastKeyInserted = null;
        isKeyInLock = false;
    }

    public void CheckIfCorrectKey(float id)
    {
        isKeyInLock = true;
        if (door.lockID == id)
        {
            isCorrectKey = true;
            door.UnlockDoor();
        }
        else
            isCorrectKey = false;
    }
}
