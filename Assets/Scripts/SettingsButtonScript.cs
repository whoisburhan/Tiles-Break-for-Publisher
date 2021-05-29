using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonScript : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.SetOnSettingCanvas();
            });
        }
    }
}
