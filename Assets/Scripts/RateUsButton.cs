using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateUsButton : MonoBehaviour
{
    public const string PLAY_STORE_LINK = "https://play.google.com/store/apps/details?id=com.GameSeekersStudio.TilesBreak";

    void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                Application.OpenURL(PLAY_STORE_LINK);
            });
        }
    }
}
