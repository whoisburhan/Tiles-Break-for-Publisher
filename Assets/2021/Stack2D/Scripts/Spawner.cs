using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GS.Stack2D
{
    public class Spawner : MonoBehaviour
    {
        public const string GAME_DATA_STORE_KEY = "HIGHSCORE_STACK2D";

        public static Spawner Instance { get; private set; }
        public static event Action OnFire;

        private class BrickInfo
        {
            public float x1;
            public float x2;
        }

        [SerializeField] private GameObject brickPrefab;
        [SerializeField] private int ObjectPoolSize = 10;
        [SerializeField] private ColorPlate colorPlate;

        [Header("Score Bar")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Image sliderBar;

        [Header("UI Prefabs")]
        [SerializeField] private GameObject finalScorePanel;
        [SerializeField] private GameObject showRewardAds;

        [Header("Final Score")]
        [SerializeField] private Text finalScoreText;
        [SerializeField] private Text finalScoreTittleText;

        [Header("Controls")]
       // [SerializeField] private Transform baseBricksTransform;
        [SerializeField] private float offestValue = 0.8f;
        [SerializeField] private float brickXposOffestValue = 1.5f;        

        [SerializeField] private GameObject previousBrick;
        GameObject currentBrick;

        private BrickInfo previousBrickInfo = new BrickInfo();
        private BrickInfo currentBrickInfo = new BrickInfo();

        [HideInInspector] public Queue<GameObject> objectPool = new Queue<GameObject>();

        private float spawnPointY;
        private bool isPlay = true;

        private int previousColorIndex = 0;
        private int score = 0;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            if(AudioManager.Instance != null)
            {
                AudioManager.Instance.ResetAudio();
            }

            spawnPointY = previousBrick.transform.position.y + offestValue;

            previousBrickInfo.x1 = previousBrick.transform.position.x - previousBrick.transform.localScale.x;
            previousBrickInfo.x2 = previousBrick.transform.position.x + previousBrick.transform.localScale.x;

            Debug.Log("P1: " + previousBrickInfo.x1 + " | P2: " + previousBrickInfo.x2);

            for (int i = 0; i < ObjectPoolSize; i++)
            {
                GameObject _go = Instantiate(brickPrefab);
                objectPool.Enqueue(_go);
                _go.SetActive(false);
            }

            SpawnNewBrick();
        }

        private void Update()
        {
            if (isPlay)
            {
                if(Input.touchCount > 0)
                {
                    Touch[] touches = Input.touches;

                    if(touches[0].phase == TouchPhase.Began)
                    {
                        Fire();
                    }
                }
            }
        }


        private void Fire()
        {
            OnFire?.Invoke();

            Brick _brick = currentBrick.GetComponent<Brick>();

            if (_brick != null)
            {
                _brick.StopBrickMovement();
            }


            /// Calculation: :P ---------------------------------
            previousBrickInfo.x1 = previousBrick.transform.position.x - previousBrick.transform.localScale.x;
            previousBrickInfo.x2 = previousBrick.transform.position.x + previousBrick.transform.localScale.x;


            currentBrickInfo.x1 = currentBrick.transform.position.x - currentBrick.transform.localScale.x;
            currentBrickInfo.x2 = currentBrick.transform.position.x + currentBrick.transform.localScale.x;

            Debug.Log("C1: " + currentBrickInfo.x1 + " | C2: " + currentBrickInfo.x2);
            Debug.Log("P1: " + previousBrickInfo.x1 + " | P2: " + previousBrickInfo.x2);

            if (currentBrickInfo.x1 == previousBrickInfo.x1)
            {
                UpdateScore(2);
                // Perfect Match ; +2 Score + No CutEdge + Spawn New Brick
            }
            else if(currentBrickInfo.x1 < previousBrickInfo.x1 && currentBrickInfo.x2 > previousBrickInfo.x1)
            {
                UpdateScore(1);

                BrickInfo newBrickInfo = new BrickInfo();
                BrickInfo destroyableBrickInfo = new BrickInfo();

                newBrickInfo.x1 = previousBrickInfo.x1;
                newBrickInfo.x2 = currentBrickInfo.x2;

                float newSize = Mathf.Abs(newBrickInfo.x2 - newBrickInfo.x1) / 2f;
                float newPosition = newBrickInfo.x1 + newSize;

                currentBrick.transform.localScale = new Vector3(newSize, currentBrick.transform.localScale.y, currentBrick.transform.localScale.z);
                currentBrick.transform.position = new Vector3(newPosition, currentBrick.transform.position.y, currentBrick.transform.position.z);

                destroyableBrickInfo.x1 = currentBrickInfo.x1;
                destroyableBrickInfo.x2 = previousBrickInfo.x1;

                float destroyableBrickSize = (Mathf.Abs(destroyableBrickInfo.x2 - destroyableBrickInfo.x1)) / 2f;
                Debug.Log(destroyableBrickInfo.x2 - destroyableBrickInfo.x1);
                Debug.Log(destroyableBrickSize);
                float destroyableBrickPosition = destroyableBrickInfo.x1 + destroyableBrickSize;

                GameObject _destroyableGO = objectPool.Dequeue();

                _destroyableGO.transform.localScale = new Vector3(destroyableBrickSize, currentBrick.transform.localScale.y, currentBrick.transform.localScale.z);
                _destroyableGO.transform.position = new Vector3(destroyableBrickPosition, currentBrick.transform.position.y, currentBrick.transform.position.z);

                _destroyableGO.SetActive(true);

                Brick _destroyableBrick = _destroyableGO.GetComponent<Brick>();
                _destroyableBrick.StopBrickMovement();
                _destroyableBrick.ColorIndex = previousColorIndex;
                _destroyableBrick.Deactivate(1f);

                _destroyableGO.GetComponent<SpriteRenderer>().color = colorPlate.colors[previousColorIndex];

                currentBrickInfo.x1 = newBrickInfo.x1;
                currentBrickInfo.x2 = newBrickInfo.x2;
                
                
            }
            else if(currentBrickInfo.x2 > previousBrickInfo.x2 && currentBrickInfo.x1 < previousBrickInfo.x2)
            {
                UpdateScore(1);

                Debug.Log("Right");
                BrickInfo newBrickInfo = new BrickInfo();
                BrickInfo destroyableBrickInfo = new BrickInfo();

                newBrickInfo.x1 = currentBrickInfo.x1;
                newBrickInfo.x2 = previousBrickInfo.x2;

                float newSize = Mathf.Abs(newBrickInfo.x2 - newBrickInfo.x1) / 2f;
                float newPosition = newBrickInfo.x1 + newSize;

                currentBrick.transform.localScale = new Vector3(newSize,currentBrick.transform.localScale.y, currentBrick.transform.localScale.z);
                currentBrick.transform.position = new Vector3(newPosition, currentBrick.transform.position.y, currentBrick.transform.position.z);

                destroyableBrickInfo.x1 = previousBrickInfo.x2;
                destroyableBrickInfo.x2 = currentBrickInfo.x2;

                float destroyableBrickSize = Mathf.Abs(destroyableBrickInfo.x2 - destroyableBrickInfo.x1) / 2f;
                float destroyableBrickPosition = destroyableBrickInfo.x1 + destroyableBrickSize;

                GameObject _destroyableGO = objectPool.Dequeue();
             
                _destroyableGO.transform.localScale = new Vector3(destroyableBrickSize, currentBrick.transform.localScale.y, currentBrick.transform.localScale.z);
                _destroyableGO.transform.position = new Vector3(destroyableBrickPosition, currentBrick.transform.position.y, currentBrick.transform.position.z);

                _destroyableGO.SetActive(true);

                Brick _destroyableBrick = _destroyableGO.GetComponent<Brick>();
                _destroyableBrick.StopBrickMovement();
                _destroyableBrick.ColorIndex = previousColorIndex;
                _destroyableBrick.Deactivate(1f);

                _destroyableGO.GetComponent<SpriteRenderer>().color = colorPlate.colors[previousColorIndex];

                currentBrickInfo.x1 = newBrickInfo.x1;
                currentBrickInfo.x2 = newBrickInfo.x2;

            }

            else
            {
                UpdateScore(0);

                Brick _destroyableBrick = currentBrick.GetComponent<Brick>();
                _destroyableBrick.StopBrickMovement();
                _destroyableBrick.ColorIndex = previousColorIndex;
                _destroyableBrick.Deactivate(1.5f);
                isPlay = false;
                Debug.Log("NOT TOUCH");
                return;
            }

            ///--------------------------------------------------

            SpawnNewBrick();
        }

        private void SpawnNewBrick()
        {
            brickXposOffestValue *= UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            Vector2 _tempPos = new Vector2(brickXposOffestValue, spawnPointY);

            if (currentBrick != null)
            {
                previousBrick = currentBrick;

                previousBrickInfo.x1 = currentBrickInfo.x1;
                previousBrickInfo.x2 = currentBrickInfo.x2;
            }

            currentBrick = objectPool.Dequeue();//Instantiate(brickPrefab, _tempPos, Quaternion.identity);

            currentBrick.transform.rotation = Quaternion.identity;

            if (previousBrick != null)
                currentBrick.transform.localScale = previousBrick.transform.localScale;

            previousColorIndex = ColorIndexPicker();
            currentBrick.GetComponent<SpriteRenderer>().color = colorPlate.colors[previousColorIndex];
            currentBrick.GetComponent<Brick>().ColorIndex = previousColorIndex;
            currentBrick.transform.position = _tempPos;
            currentBrick.SetActive(true);
            //currentBrick.transform.localScale = previousBrick.transform.localScale;
            //currentBrick.transform.SetParent(previousBrick.transform);

            spawnPointY += offestValue;
            transform.position = new Vector3(transform.position.x, spawnPointY, transform.position.z);
        }

        private int ColorIndexPicker()
        {
            int _currentCOlorIndex = previousColorIndex;

            while (_currentCOlorIndex == previousColorIndex)
            {
                _currentCOlorIndex = UnityEngine.Random.Range(0, colorPlate.colors.Count);
            }

            return _currentCOlorIndex;
        }

        private void UpdateScore(int _score)
        {
            score += _score;
            scoreText.text = score.ToString();

            if (score > 59) scoreText.color = colorPlate.colors[2];
            else if (score > 29) scoreText.color = colorPlate.colors[1];
            else scoreText.color = colorPlate.colors[0];

            sliderBar.fillAmount = Mathf.Clamp((score / 100f), 0f, 1f);

            switch (_score)
            {
                case 0:
                    finalScorePanel.SetActive(true);
                    AudioManager.Instance.AudioChangeFunc(0, 1, false, 3f);
                    scoreText.gameObject.SetActive(false);
                    sliderBar.transform.parent.gameObject.SetActive(false);
                    FinalScoreCal();
                    break;
                case 1:
                    AudioManager.Instance.AudioChangeFunc(0, 0, false, 3f);
                    break;
                case 2:
                    AudioManager.Instance.AudioChangeFunc(0, 3, false, 3f);
                    break;
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

        public void NewGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
