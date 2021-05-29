using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Fluffy
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }

        private float offsetX;

        [SerializeField] private float minGapX = 3.5f;
        [SerializeField] private float maxGapX = 4.5f;

        [SerializeField] private GameObject pillerPrefab;
        [SerializeField] private ColorPlate colorPlate;

        public Queue<GameObject> MyQueue = new Queue<GameObject>();

        private int previousColorIndex = -1;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            offsetX = transform.position.x;

            for(int i = 0; i< 10; i++)
            {
                GameObject _go = Instantiate(pillerPrefab);
                MyQueue.Enqueue(_go);
                _go.SetActive(false);
            }

            SpawnPiller();
            SpawnPiller();
        }

        public void SpawnPiller()
        {
            float _offsetXValue = Random.Range(minGapX, maxGapX);

            offsetX += _offsetXValue;

            Vector3 _offsetPos = new Vector3(offsetX, transform.position.y, transform.position.z);

            int _colorIndex = ColorIndexPicker();

            GameObject _go = MyQueue.Dequeue(); //Instantiate(pillerPrefab, _offsetPos, Quaternion.identity);
            _go.SetActive(true);
            _go.transform.position = _offsetPos;
            _go.transform.rotation = Quaternion.identity;

            Piller _piller = _go.GetComponent<Piller>();

            _piller.UpperPiller.GetComponent<SpriteRenderer>().color = colorPlate.colors[_colorIndex];
            _piller.DownPiller.GetComponent<SpriteRenderer>().color = colorPlate.colors[_colorIndex];

            previousColorIndex = _colorIndex;
        }

        private int ColorIndexPicker()
        {
            int _currentCOlorIndex = previousColorIndex;

            while(_currentCOlorIndex == previousColorIndex)
            {
                _currentCOlorIndex = UnityEngine.Random.Range(0, colorPlate.colors.Count);
            }

            return _currentCOlorIndex;
        }
    }
}