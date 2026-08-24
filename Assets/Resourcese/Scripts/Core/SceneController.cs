using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController>
{
    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }
}
