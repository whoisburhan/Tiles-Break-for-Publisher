using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Text[] levelHisghScores;
    [SerializeField] private GameObject[] completeTextGameObject;

    private void Start()
    {
        for(int i = 0; i < levelHisghScores.Length; i++)
        {
            int score = PlayerPrefs.HasKey("HIGHSCORE_" + (i+1).ToString()) ? 
                PlayerPrefs.GetInt("HIGHSCORE_" + (i+1).ToString()) : 0;
            levelHisghScores[i].text = score.ToString();
            if(score >= 500)
            {
                completeTextGameObject[i].SetActive(true);
            }
            
        }    
    }
}
