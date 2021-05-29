using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartMenuScript : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private Text timeText;
    [SerializeField] private Text dateText;
    [SerializeField] private Text dayText;
    public const string PLAY_STORE_LINK = "https://play.google.com/store/apps/details?id=com.GameSeekersStudio.TilesBreak";

    private void Start()
    {
        StartCoroutine(TimeTable());
    }

    IEnumerator TimeTable()
    {
        while (true)
        {
            if (timeText != null)
            {
                timeText.text = DateTime.Now.ToString("hh:mm tt");
                dateText.text = DateTime.Now.ToString("dd-MM-yyyy");
                dayText.text = DateTime.Now.ToString("ddd") + " Day";
            }
            yield return new WaitForSeconds(10f);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}
