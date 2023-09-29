using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public DoorBehaviour serverRoomDoor;
    public GameObject amphi;

    [System.Flags]
    public enum Level1States
    {
        None = 0,
        ServerRoomDoorUnlocked = 1,
        AmphiDoorUnlocked = 2,
    }
    public Level1States level1States;

    void Awake()
    {
        // SetFoveationLevel(3);
        Unity.XR.Oculus.Utils.EnableDynamicFFR(true);
        // Unity.XR.Oculus.Utils.GetFoveationLevel();
        
        float[] rates;
        Unity.XR.Oculus.Performance.TryGetAvailableDisplayRefreshRates(out rates);
        
        Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(90);
        
        float rate;
        Unity.XR.Oculus.Performance.TryGetDisplayRefreshRate(out rate);
    }

    void Start()
    {
        amphi.SetActive(false);
        serverRoomDoor.OnDoorUnlock += ServerRoomUnlockHandler;
        UpdateState();
    }

    void UpdateState()
    {
        switch(level1States)
        {
            case Level1States.ServerRoomDoorUnlocked:
                // Load Amphi
                amphi.SetActive(true);
                break;
            case Level1States.AmphiDoorUnlocked:
                // Load Level 2
                break;
        }
    }

    void ServerRoomUnlockHandler()
    {
        // Load Amphi
        amphi.SetActive(true);
        level1States |= Level1States.ServerRoomDoorUnlocked;
    }
}
