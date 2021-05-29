using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.TilesBreak
{
    public class SpawnPoint : MonoBehaviour
    {
        private float screenSizeInXPercentage;
        private float cutOutPercentage = 5f;
        private float cutOutOffset = 5f;
        private float totalCutOutOffset;
        private float tileSizeInXDirection;
        private float halfOfTileSizeInXDirection;
        private float SP0, SP1, SP2, SP3;
        private float duration = 0.25f;
        private Vector3 offset = new Vector3(0f, 2f);

        [SerializeField] private ColorPlate myColors;
        private int previousTilesColorIndex = -1;

        float timer;

        public GameObject tilePrefab;
        SpriteRenderer spriteRenderer;

        [Header("Tiles Texture")]
        public Sprite[] tilesSprite;

        private void Awake()
        {
            timer = duration;
            screenSizeInXPercentage = Screen.width / 100f;
            // Debug.Log(Screen.width + " " + screenSizeInXPercentage +"|"+ screenSizeInXPercentage * 100);
            totalCutOutOffset = cutOutOffset * 5;
            tileSizeInXDirection = (100 - totalCutOutOffset) / 4;
            //  Debug.Log("Tile Size: " + tileSizeInXDirection);
            halfOfTileSizeInXDirection = tileSizeInXDirection / 2;
            Formation();
        }


        private void Update()
        {
            duration = Controller.instance.tileSpawnerIntervalTime;
            if (Controller.instance.isPlay)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer = duration;
                    // Spawner();
                    NewSpawner();
                }
            }
        }

        private void Formation()
        {
            SP0 = (cutOutOffset + halfOfTileSizeInXDirection);
            SP1 = (SP0 + tileSizeInXDirection + cutOutOffset);
            SP2 = (SP1 + tileSizeInXDirection + cutOutOffset);
            SP3 = (SP2 + tileSizeInXDirection + cutOutOffset);

            //  Debug.Log(SP0 + " " + SP1 + " " + SP2);

            transform.GetChild(0).position = Camera.main.ScreenToWorldPoint(new Vector3(SP0 * screenSizeInXPercentage, Screen.height));
            transform.GetChild(1).position = Camera.main.ScreenToWorldPoint(new Vector3(SP1 * screenSizeInXPercentage, Screen.height));
            transform.GetChild(2).position = Camera.main.ScreenToWorldPoint(new Vector3(SP2 * screenSizeInXPercentage, Screen.height));
            transform.GetChild(3).position = Camera.main.ScreenToWorldPoint(new Vector3(SP3 * screenSizeInXPercentage, Screen.height));

            transform.GetChild(0).position += offset;
            transform.GetChild(1).position += offset;
            transform.GetChild(2).position += offset;
            transform.GetChild(3).position += offset;

            transform.GetChild(4).position = Camera.main.ScreenToWorldPoint(new Vector3(SP0 * screenSizeInXPercentage, 0f));
            transform.GetChild(5).position = Camera.main.ScreenToWorldPoint(new Vector3(SP1 * screenSizeInXPercentage, 0f));
            transform.GetChild(6).position = Camera.main.ScreenToWorldPoint(new Vector3(SP2 * screenSizeInXPercentage, 0f));
            transform.GetChild(7).position = Camera.main.ScreenToWorldPoint(new Vector3(SP3 * screenSizeInXPercentage, 0f));

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).position = new Vector3(transform.GetChild(i).position.x,
                    transform.GetChild(i).position.y, 0f);
            }

        }

        /*
        private void Spawner()
        { 
            int index = Random.Range(0, 3);
            GameObject go = Instantiate(tilePrefab, transform.GetChild(index).position, Quaternion.identity);
            Destroy(go, 10f);
                if (Controller.instance.score < 30)
                {
                    go.GetComponent<TileScript>().Move(false);
                }
                else
                {
                    go.GetComponent<TileScript>().Move(true);
                }
                //----------------------------------------

                //Determing lvl of tiles
                if (Controller.instance.score < 10)
                {
                    go.GetComponent<TileHealth>().TileLvl(1);
                }
                else if (Controller.instance.score >= 10 && Controller.instance.score < 20)
                {
                    int x = Random.Range(0, 3);
                    if (x == 2)
                    {
                        go.GetComponent<TileHealth>().TileLvl(2);
                    }
                    else
                    {
                        go.GetComponent<TileHealth>().TileLvl(1);
                    }
                }
                else if (Controller.instance.score >= 20 && Controller.instance.score < 40)
                {
                    int x = Random.Range(0, 6);
                    if (x == 5)
                    {
                        go.GetComponent<TileHealth>().TileLvl(3);
                    }
                    else if (x == 3 || x == 4)
                    {
                        go.GetComponent<TileHealth>().TileLvl(2);
                    }
                    else
                    {
                        go.GetComponent<TileHealth>().TileLvl(1);
                    }
                }
            else
            {
                int x = Random.Range(0, 4);
                if (x == 2)
                {
                    go.GetComponent<TileHealth>().TileLvl(3);
                }
                else if (x == 1)
                {
                    go.GetComponent<TileHealth>().TileLvl(2);
                }
                else if (x == 3)
                {
                    go.GetComponent<TileHealth>().TileLvl(4);
                }
                else
                {
                    go.GetComponent<TileHealth>().TileLvl(1);
                }
            }
        }
        */

        private void NewSpawner()
        {
            int index = Random.Range(0, 4);
            GameObject go = Instantiate(tilePrefab, transform.GetChild(index).position, Quaternion.identity);
            Destroy(go, 10f);

            int _tilesIndex = 0;//Random.Range(0, tilesSprite.Length);

            go.GetComponent<SpriteRenderer>().sprite = tilesSprite[_tilesIndex];

            if (myColors != null)
            {
                int _selectColorIndex = CheckForDifferentColorIndex();
                previousTilesColorIndex = _selectColorIndex;
                go.GetComponent<SpriteRenderer>().color = myColors.colors[_selectColorIndex];
            }

            if (Controller.instance.score < 30)
            {
                go.GetComponent<TileScript>().Move(false);
            }
            else
            {
                go.GetComponent<TileScript>().Move(true);
            }

            if (_tilesIndex == 3)
            {
                go.GetComponent<TileHealth>().TileLvl(4);  // 4 for bomb
            }
            else
            {
                go.GetComponent<TileHealth>().TileLvl(1);
            }
        }

        private int CheckForDifferentColorIndex()
        {
            int _index = previousTilesColorIndex;

            while (_index == previousTilesColorIndex)
            {
                _index = Random.Range(0, myColors.colors.Count);
            }
            return _index;
        }
    }
}