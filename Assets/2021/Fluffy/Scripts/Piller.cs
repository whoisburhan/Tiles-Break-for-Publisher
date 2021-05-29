using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Fluffy
{
    public class Piller : MonoBehaviour
    {
        private float minPosCng = -1.8f;
        private float maxPosCng = 1.8f;

        private float minValue = 1.2f;
        private float maxValue = 2.5f;

        public GameObject UpperPiller;
        public GameObject DownPiller;

        private void Start()
        {
            float _mainPillerPosOffset = UnityEngine.Random.Range(minPosCng, maxPosCng);

            transform.position = new Vector3(transform.position.x, transform.position.y + _mainPillerPosOffset, transform.position.y);

            float _childPillerPosOffset = UnityEngine.Random.Range(minValue, maxValue);

            UpperPiller.transform.position = new Vector3(UpperPiller.transform.position.x, UpperPiller.transform.position.y + _childPillerPosOffset, UpperPiller.transform.position.z);
            DownPiller.transform.position = new Vector3(DownPiller.transform.position.x, DownPiller.transform.position.y - _childPillerPosOffset, DownPiller.transform.position.z);
        }

        public void Deactivate(float _delay = 10f)
        {
            StartCoroutine(DeactivateAfter(_delay));
        }

        IEnumerator DeactivateAfter(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            Spawner.Instance.MyQueue.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}