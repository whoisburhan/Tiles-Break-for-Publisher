using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilitySceneScript : MonoBehaviour
{
    public static UtilitySceneScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
