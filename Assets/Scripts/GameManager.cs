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
        SceneManager.LoadScene("EndScene");
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
