using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController>
{
    public void LoadScene(int idx)
    {
        GameManager.Instance.ResumeGame();
        SceneManager.LoadScene(idx);
    }
}
