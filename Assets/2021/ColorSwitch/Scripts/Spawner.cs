using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.ColorSwitch
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }

        private float offsetY;

        [SerializeField] private float minGapX = 3.5f;
        [SerializeField] private float maxGapX = 4.5f;

        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private GameObject colorChangerPrefab;
        [SerializeField] private ColorPlate colorPlate;

        public Queue<GameObject> ColorChangerQueue = new Queue<GameObject>();
        public List<Queue<GameObject>> MyList = new List<Queue<GameObject>>();

        private int previousColorIndex = -1;
        private int spawnCounter = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            offsetY = transform.position.y;

            for(int i = 0; i< prefabs.Length; i++)
            {
                Queue<GameObject> _queue = new Queue<GameObject>();
                for(int j = 0; j < 10; j++)
                {
                    GameObject _go = Instantiate(prefabs[i]);
                    _queue.Enqueue(_go);
                    _go.GetComponent<Circle>().SetObjectTypeNo(i);
                    _go.SetActive(false);
                }
                MyList.Add(_queue);
            }

            for (int j = 0; j < 10; j++)
            {
                GameObject _go = Instantiate(colorChangerPrefab);
                ColorChangerQueue.Enqueue(_go);
                _go.SetActive(false);
            }

            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
            SpawnPiller();
        }

        public void SpawnPiller()
        {
            spawnCounter++;

            float _offsetYValue = Random.Range(minGapX, maxGapX);

            offsetY += _offsetYValue;

            Vector3 _offsetPos = new Vector3(transform.position.x, offsetY, transform.position.z);

            if (spawnCounter % 3 != 0)
            {

              //  int _colorIndex = ColorIndexPicker();

                var _myQueue = MyList[Random.Range(0, MyList.Count)];

                GameObject _go = _myQueue.Dequeue(); //Instantiate(pillerPrefab, _offsetPos, Quaternion.identity);
                _go.SetActive(true);
                _go.transform.position = _offsetPos;
                _go.transform.rotation = Quaternion.identity;
                _go.GetComponent<Circle>().GemEnable();
               // _go.GetComponent<SpriteRenderer>().color = colorPlate.colors[_colorIndex];
               // _go.tag = "Color" + _colorIndex.ToString();

               // previousColorIndex = _colorIndex;
            }

            else
            {
                GameObject _go = ColorChangerQueue.Dequeue();
                _go.SetActive(true);
                _go.transform.position = _offsetPos;
                _go.transform.rotation = Quaternion.identity;

              //  _go.tag = "Change";
            }
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
    }
}