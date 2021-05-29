using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GS.CommonStuff
{
    public class GameUIButtonTask : MonoBehaviour
    {
        [SerializeField] private Button BackToMainMenuButton;
        [SerializeField] private Button SettingsButton;

        private void Start()
        {
            if(BackToMainMenuButton != null)
            {
                BackToMainMenuButton.onClick.AddListener(() => 
                {
                    SceneManager.LoadScene(2);
                });
            }
        }
    }
}