using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock : MonoBehaviour
{
    XRSocketInteractor socketInteractor;
    DoorBehaviour door;

    void Start()
    {
        door = GetComponentInParent<DoorBehaviour>();
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    public void SendKeyIdToDoor()
    {
        if (socketInteractor.selectTarget.TryGetComponent<DoorKey>(out DoorKey key))
            door.CheckIfCorrectKey(key.ID);
        else Debug.LogWarning("Object inserted in lock is not a Door Key!");
    }
}
