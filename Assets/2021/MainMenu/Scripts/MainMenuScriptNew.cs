using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScriptNew : MonoBehaviour
{
    [SerializeField] private Text[] highscores;

    private void Start()
    {
        if(highscores != null)
        {
            SetHighScore(highscores[0], GS.TilesBreak.Controller.GAME_DATA_STORE_KEY);
            SetHighScore(highscores[1], GS.Fluffy.Controller.GAME_DATA_STORE_KEY);
            SetHighScore(highscores[2], GS.ColorSwitch.Controller.GAME_DATA_STORE_KEY);
            SetHighScore(highscores[3], GS.Stack2D.Spawner.GAME_DATA_STORE_KEY);
        }
    }

    public void LoadScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    private void SetHighScore(Text _textHolder, string _key)
    {
        int _highscore = PlayerPrefs.GetInt(_key, 0);
        _textHolder.text = "HS : " + _highscore.ToString();
    }
}
