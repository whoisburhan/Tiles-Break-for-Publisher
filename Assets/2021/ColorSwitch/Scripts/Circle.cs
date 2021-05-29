using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.ColorSwitch
{
    public class Circle : MonoBehaviour
    {
        [SerializeField] private GameObject gem;
        private int objectTypeNo = 0;

        public void SetObjectTypeNo(int n)
        {
            objectTypeNo = n;
        }

        public void Deactivate(float _delay = 10f)
        {
            StartCoroutine(DeactivateAfter(_delay));
        }

        IEnumerator DeactivateAfter(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            var _queue = Spawner.Instance.MyList[objectTypeNo];
            _queue.Enqueue(gameObject);
            this.gameObject.SetActive(false);
        }

        public void GemEnable()
        {
            if (gem != null)
                gem.SetActive(true);
        }
    }
}