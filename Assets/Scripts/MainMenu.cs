using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "LevelMenu";

    public SceneFader sceneFader;
    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Options()
    {
        Debug.Log("Open options");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
