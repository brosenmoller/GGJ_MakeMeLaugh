using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        QuitButton.onClick.AddListener(() => Application.Quit());
    }
}
