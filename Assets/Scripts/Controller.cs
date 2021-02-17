using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System;

public class Controller : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private string gameID = "3643881";
    [SerializeField] private bool testMode = false;
    private string rewardedViwedPlacementId = "rewardedVideo";
    private string regulerPlacementId = "video";


    public int LevelNO;

    public static Controller instance;

    public GameObject finalScorePanel;
    public GameObject showRewardAds;

    public Text scoreText;
    public Text finalScoreText;

    public float tileSpeed = 100f;
    public float tileSpawnerIntervalTime = 0.5f;
    public int score = 0;
    public int life = 3;

    public bool isPlay = false;
    public bool isGameOver = false;
    private bool flag = true;
    public AudioSource sound;
    public AudioSource backgroundMusic;

    public GameObject[] hitParticles;

    private bool allowRwdAdsShow = true;

    private void Start()
    {
        Time.timeScale = 1f;
        Advertisement.Initialize(gameID, testMode);

        if (finalScorePanel.activeSelf)
        {
            finalScorePanel.SetActive(false);
        }
        instance = this;
        isGameOver = false;
        flag = true;

        AudioManager.Instance.BackgroundAudioFunc(0);
    }

    private void Update()
    {

        if(isGameOver && flag)
        {
            if (Advertisement.IsReady(rewardedViwedPlacementId) && allowRwdAdsShow)
            {
                if (!showRewardAds.activeSelf)
                {
                    showRewardAds.SetActive(true);
                    Time.timeScale = 0f;
                }
                flag = false;
            }
            else
            {
                
            if (!finalScorePanel.activeSelf)
            {
                finalScorePanel.SetActive(true);
            }

            if (scoreText.gameObject.activeSelf)
            {
                scoreText.gameObject.SetActive(false);
            }

            flag = false;
            }
        }

        if (scoreText.gameObject.activeSelf)
        {
            scoreText.text = "Score : " + score.ToString();
        }

        if (finalScoreText.gameObject.activeSelf)
        {
            PlayerPrefs.SetInt("HIGHSCORE_" + LevelNO.ToString(), (PlayerPrefs.HasKey("HIGHSCORE_" + LevelNO.ToString())) ?
                PlayerPrefs.GetInt("HIGHSCORE_" + LevelNO.ToString()) > score ? 
                PlayerPrefs.GetInt("HIGHSCORE_" + LevelNO.ToString()): score : score);
            finalScoreText.text = score.ToString();
        }

        if (isPlay)
        {
            if (Input.touchCount > 0)
            {
                Touch[] touches = Input.touches;
                foreach (Touch touch in touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
                        HITPOINT(worldPoint);
                        // Debug.Log(Input.touchCount);
                    }
                }
            }

            TileSpeedUpdate();
        }
    }

    private void TileSpeedUpdate()
    {
        #region switch
        /*switch(score)
        {
            case 5:
                tileSpeed = 6.5f;
                tileSpawnerIntervalTime = 1.45f;
                break;
            case 10:
                tileSpeed = 6.75f;
                tileSpawnerIntervalTime = 1.4f;
                break;
            case 15:
                tileSpeed = 7.0f;
                tileSpawnerIntervalTime = 1.35f;
                break;
            case 20:
                tileSpeed = 7.25f;
                tileSpawnerIntervalTime = 1.3f;
                break;
            case 30:
                tileSpeed = 7.5f;
                tileSpawnerIntervalTime = 1.25f;
                break;
            case 40:
                tileSpeed = 7.75f;
                tileSpawnerIntervalTime = 1.20f;
                break;
            case 50:
                tileSpeed = 8.0f;
                tileSpawnerIntervalTime = 1.1f;
                break;
            case 60:
                tileSpeed = 8.5f;
                tileSpawnerIntervalTime = 1.0f;
                break;
            case 70:
                tileSpeed = 9.0f;
                tileSpawnerIntervalTime = 0.9f;
                break;
            case 80:
                tileSpeed = 9.5f;
                tileSpawnerIntervalTime = 0.85f;
                break;
            case 90:
                tileSpeed = 10.0f;
                tileSpawnerIntervalTime = 0.8f;
                break;
            case 100:
                tileSpeed = 10.5f;
                tileSpawnerIntervalTime = 0.77f;
                break;
            case 120:
                tileSpeed = 11.0f;
                tileSpawnerIntervalTime = 0.75f;
                break;
        }*/
        #endregion

        if (score > 560)
        {
            Time.timeScale = 1.7f;
        }
        else if (score > 420)
        {
            Time.timeScale = 1.6f;
        }
        else if(score > 350)
        {
            Time.timeScale = 1.5f;
        }
        else if(score > 280)
        {
            Time.timeScale = 1.4f;
        }
        else if (score > 210)
        {
            Time.timeScale = 1.3f;
        }
        else if (score > 140)
        {
            Time.timeScale = 1.2f;
        }
        else if (score > 70)
        {
            Time.timeScale = 1.1f;
        }
        backgroundMusic.pitch = Time.timeScale;
    }

    private void HITPOINT(Vector2 worldPoint)
    {

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null)      //for avoiding uncolider object touching error
        {

            if (hit.collider.transform.tag == "Tiles")
            {
                AudioManager.Instance.AudioChangeFunc(0, 0, false, 3f, 0.2f);
                
                score += 1;
                if(hit.transform.GetComponent<TileHealth>().tileLvl == 1)
                {
                    Destroy(Instantiate(hitParticles[0],hit.transform.position,Quaternion.identity), 1f);
                }
                else if (hit.transform.GetComponent<TileHealth>().tileLvl == 2)
                {
                    Destroy(Instantiate(hitParticles[1], hit.transform.position, Quaternion.identity), 1f);
                }
                else if (hit.transform.GetComponent<TileHealth>().tileLvl == 3)
                {
                    Destroy(Instantiate(hitParticles[2], hit.transform.position, Quaternion.identity), 1f);
                }
                hit.transform.GetComponent<TileHealth>().TileHealthUpdate();
            }
            else if (hit.collider.transform.tag == "Bomb")
            {
                AudioManager.Instance.AudioChangeFunc(0, 1, false, 1.4f);
                
                //score += 1;
                isGameOver = true;
            }/*
             else if (hit.collider.transform.tag == "BOMB")
             {

             }*/
        }
       /* else
        {
            AudioManager.Instance.AudioChangeFunc(0, 1, false, 1.4f);
            //score += 1;
            isGameOver = true;
        }*/
        
    }

    public void ShowRwdAds()
    {
        ShowRewardedRegularAd(OnRewardedAdClosed);
        
    }

    public void ShowRewardedRegularAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(rewardedViwedPlacementId))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(rewardedViwedPlacementId, so);
        }
#endif
    }

    private void OnRewardedAdClosed(ShowResult result)
    {
        Debug.Log("Rewarded ad Closed");
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished,reward Player");
                Time.timeScale = 1f;
                isGameOver = false;
                if (!isGameOver)
                {
                    flag = true;
                }
                allowRwdAdsShow = false;
                isPlay = true;
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped, posa manus :(");
                Time.timeScale = 1f;
                if (!finalScorePanel.activeSelf)
                {
                    finalScorePanel.SetActive(true);
                }
                break;
            case ShowResult.Failed:
                Debug.Log("Yaaa! play oylo nani ad");
                Time.timeScale = 1f;
                if (!finalScorePanel.activeSelf)
                {
                    finalScorePanel.SetActive(true);
                }
                break;
        }
    }


    public void RewardedAdPanelCrossButton()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        isPlay = true;
    }

    public void NewGame()
    {
        /*if (Advertisement.IsReady(regulerPlacementId))
        {
            Advertisement.Show(regulerPlacementId);
        }*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Ads here :)
        /*isPlay = true;
        score = 0;
        isGameOver = false;
        if (!scoreText.gameObject.activeSelf)
        {
            scoreText.gameObject.SetActive(true);
        }*/
    }
}
