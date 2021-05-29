#region Library
using GS.CommonStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#endregion

namespace GS.Fluffy
{
    public class Controller : MonoBehaviour
    {
        public const string GAME_DATA_STORE_KEY = "HIGHSCORE_FLUFFY";

        private Rigidbody2D rb2d;
        private SpriteRenderer sr;
        private SpriteMask sMask;
        [SerializeField] GameObject Shine;

        [SerializeField] private float xAxisMovementSpeed = 5f;
        [SerializeField] private float jumpForce = 15f;
        [SerializeField] private float gravityScale = 2f;

        [SerializeField] private Transform[] barriers;

        [Header("Score Bar")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Image sliderBar;
        [SerializeField] private ColorPlate myColors;

        [Header("UI Prefabs")]
        [SerializeField] private GameObject finalScorePanel;
        [SerializeField] private GameObject showRewardAds;

        [Header("Final Score")]
        [SerializeField] private Text finalScoreText;
        [SerializeField] private Text finalScoreTittleText;

        private int score = 0;
        private bool isGameOver = false;
        private bool flag = true;
        private bool allowRwdAdsShow = true;
        private bool isPlay = true;

        private GameObject hittedGameObject;

        private void OnEnable()
        {
            AdsManager_Unity.OnRewardGiving += RewardSuccess;
            AdsManager_Unity.OnRewardCancel += RewardFailed;
            AdsManager_Unity.OnRewardError += RewardError;
        }

        private void OnDisable()
        {
            AdsManager_Unity.OnRewardGiving -= RewardSuccess;
            AdsManager_Unity.OnRewardCancel -= RewardFailed;
            AdsManager_Unity.OnRewardError -= RewardError;
        }

        private void Start()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.ResetAudio();
            }

            Time.timeScale = 1f;
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.gravityScale = gravityScale;
            sr = GetComponent<SpriteRenderer>();
            sMask = GetComponent<SpriteMask>();
        }

        private void Update()
        {
            
            Control();

            foreach(var t in barriers)
            {
                t.position = new Vector3(transform.position.x, t.position.y, t.position.z);
            }


            CheckForGameOver();

        }

        private void Control()
        {
            if (!isGameOver)
            {
                transform.Translate(transform.right * xAxisMovementSpeed * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //  Debug.Log("OK");
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce) * Time.deltaTime;
                    AudioManager.Instance.AudioChangeFunc(0, 0, false, 3);
                }

                if (Input.touchCount > 0)
                {
                    Touch[] touches = Input.touches;
                    foreach (Touch touch in touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce) * Time.deltaTime;
                            AudioManager.Instance.AudioChangeFunc(0, 0, false, 3);
                        }
                    }
                }
            }

        }

        private void CheckForGameOver()
        {
            if (isGameOver && flag)
            {
                if (Advertisement.IsReady(AdsManager_Unity.REWARDED_VIDEO_PLACEMENT) && allowRwdAdsShow)
                {
                    if (!showRewardAds.activeSelf)
                    {
                        showRewardAds.SetActive(true);
                        
                    }

                    rb2d.gravityScale = 0f;
                    rb2d.velocity = Vector2.zero;

                    FinalScoreCal();

                    flag = false;
                }
                else
                {
                    rb2d.gravityScale = 0f;
                    rb2d.velocity = Vector2.zero;

                    if (!finalScorePanel.activeSelf)
                    {
                        finalScorePanel.SetActive(true);
                    }

                    FinalScoreCal();

                    if (scoreText.gameObject.activeSelf)
                    {
                        scoreText.gameObject.SetActive(false);
                        sliderBar.transform.parent.gameObject.SetActive(false);
                    }

                    flag = false;
                }
            }
        }

        private void FinalScoreCal()
        {
            int hs = PlayerPrefs.GetInt(GAME_DATA_STORE_KEY, 0);

            if (score >= hs)
            {
                finalScoreTittleText.text = "HighScore";
                PlayerPrefs.SetInt(GAME_DATA_STORE_KEY, score);

            }
            else
            {
                finalScoreTittleText.text = "Score";
            }
            finalScoreText.text = score.ToString();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!isGameOver)
            {
                Debug.Log(col.tag);

                if (col.tag == "Tiles")
                {
                      AudioManager.Instance.AudioChangeFunc(1,3);

                    UpdateScore();
                   // Destroy(col.gameObject, 10f);
                    col.GetComponent<Piller>().Deactivate(10f);
                    GS.Fluffy.Spawner.Instance.SpawnPiller();

                }
                else if (col.tag == "Bomb")
                {
                    AudioManager.Instance.AudioChangeFunc(0, 1, false, 1.4f);

                    isGameOver = true;
                    hittedGameObject = col.transform.parent.gameObject;
                    hittedGameObject.SetActive(false);
                    Spawner.Instance.MyQueue.Enqueue(hittedGameObject);

                    sr.enabled = false;
                    sMask.enabled = false;
                    Shine.SetActive(false);
                }
            }
        }

        private void UpdateScore()
        {
            score++;
            scoreText.text = score.ToString();

            if (score > 59) scoreText.color = myColors.colors[2];
            else if (score > 29) scoreText.color = myColors.colors[1];
            else scoreText.color = myColors.colors[0];

            sliderBar.fillAmount = Mathf.Clamp((score / 100f), 0f, 1f);
        }

        #region Ads Zone

        private void RewardSuccess()
        {
            Debug.Log("Ad Finished,reward Player");
            rb2d.gravityScale = gravityScale;
            isGameOver = false;
            if (!isGameOver)
            {
                flag = true;
            }
            allowRwdAdsShow = false;

            sr.enabled = true;
            sMask.enabled = true;
            Shine.SetActive(true);
        }

        private void RewardFailed()
        {
            Debug.Log("Ad skipped, posa manus :(");
            Time.timeScale = 1f;
            if (!finalScorePanel.activeSelf)
            {
                finalScorePanel.SetActive(true);
            }
        }

        private void RewardError()
        {
            Debug.Log("Yaaa! play oylo nani ad");
            Time.timeScale = 1f;
            if (!finalScorePanel.activeSelf)
            {
                finalScorePanel.SetActive(true);
            }
        }

        #endregion

        #region Button Zone

        public void ShowRwdAds()
        {
            AdsManager_Unity.Instance.ShowRewardedVideo();
        }

        public void NewGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}