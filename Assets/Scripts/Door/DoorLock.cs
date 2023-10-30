using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock : MonoBehaviour
{
    XRSocketInteractor socketInteractor;
    DoorBehaviour door;
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
            CheckIfCorrectKey(key.ID);
        else Debug.LogWarning("Object inserted in lock is not a Door Key!");
    }

    public void OnKeyRemoved()
    {
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
