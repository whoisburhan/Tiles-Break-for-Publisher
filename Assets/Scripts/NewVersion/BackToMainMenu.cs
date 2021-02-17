using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public void MainMenu(int _index)
    {
        SceneManager.LoadScene(_index);
    }
}
