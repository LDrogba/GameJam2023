using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    public string nextSceneName;
    public bool levelFinished;

    public void reloadWithDelay(float delay)
    {
        Invoke("reload", delay);
    }

    private void reload()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetLevelFinished()
    {
        levelFinished = true;
    }

    public void nextLevelWithDelay(float delay)
    {
        if (levelFinished)
        {
            Invoke("loadNext", delay);
        }
    }
    private void loadNext()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
