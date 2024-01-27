using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static TimerManager TimerManager { get; private set; }

    private Manager[] activeManagers;

    private void Awake()
    {
        SingletonSetup();
        ManagerSetup();
    }

    private void SingletonSetup()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void ManagerSetup()
    {
        TimerManager = new TimerManager();

        activeManagers = new Manager[] { 
            TimerManager, 
        };

        foreach (Manager manager in activeManagers)
        {
            manager.Setup();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ReloadCurrentScene()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        foreach (Manager manager in activeManagers)
        {
            manager.OnSceneLoad();
        }
    }

    private void FixedUpdate()
    {
        foreach (Manager manager in activeManagers)
        {
            manager.OnFixedUpdate();
        }
    }
}

