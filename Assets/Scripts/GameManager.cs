using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject amphi;
    public static GameManager instance;

    [System.Flags]
    public enum Level1States
    {
        None = 0,
        ServerRoomDoorUnlocked = 1,
        AmphiDoorUnlocked = 2
    }
    public Level1States level1States;

    public delegate void OnGameStateChangeDelegate(Level1States newState);
    public static event OnGameStateChangeDelegate OnGameStateChange;
    public static float completionTime {get; private set;}

    void Awake()
    {
        // SetFoveationLevel(3);
        Unity.XR.Oculus.Utils.useDynamicFoveatedRendering = true;
        // Unity.XR.Oculus.Utils.GetFoveationLevel();
        
        float[] rates;
        Unity.XR.Oculus.Performance.TryGetAvailableDisplayRefreshRates(out rates);
        
        Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(90);
        
        float rate;
        Unity.XR.Oculus.Performance.TryGetDisplayRefreshRate(out rate);
    }

    void Start()
    {
        if (instance != null)
            Destroy(this.gameObject);
        instance = this;

        amphi.SetActive(false);
        UpdateState();
    }

    public void LoadNextLevel()
    {
        completionTime = Time.timeSinceLevelLoad;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void UpdateState() // Used for loading a specific state when begining play mode
    {
        if(level1States.HasFlag(Level1States.ServerRoomDoorUnlocked))
            ServerRoomUnlockHandler();
        if(level1States.HasFlag(Level1States.AmphiDoorUnlocked))
            LaptopUnlockHandler();
    }

    public void ServerRoomUnlockHandler()
    {
        Debug.Log("ServerRoomUnlockHandler");
        level1States |= Level1States.ServerRoomDoorUnlocked;
        amphi.SetActive(true);
        OnGameStateChange?.Invoke(level1States);
    }

    public void LaptopUnlockHandler()
    {
        Debug.Log("LaptopUnlockHandler");
        level1States |= Level1States.AmphiDoorUnlocked;
        OnGameStateChange?.Invoke(level1States);
    }

    public static void ExitGame()
    {
        Debug.Log("Exiting game!");
        Application.Quit();
    }
}
