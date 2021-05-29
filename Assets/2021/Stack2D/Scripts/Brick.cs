using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.Stack2D;

namespace GS.Stack2D
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2D;

        [SerializeField] private int DisableAfterCount = 15;

        [HideInInspector] public bool isMovementDirectionRight = true;
        [HideInInspector] public int ColorIndex = 0;

        private float offset = 1f;
        private float speed = 1.5f;

        private bool isMove = true;
        private int counter = 0;

        private void OnEnable()
        {
            isMove = true;
            if(rb2D != null)
                rb2D.isKinematic = true;
            counter = 0;

            Spawner.OnFire += DisableCounter;
        }

        private void OnDisable()
        {
            Spawner.OnFire -= DisableCounter;
        }

        private void Update()
        {
            if (isMove)
            {
                if (isMovementDirectionRight)
                    transform.Translate(transform.right * speed * Time.deltaTime);
                else
                    transform.Translate(-transform.right * speed * Time.deltaTime);

                if (transform.position.x >= offset)
                    isMovementDirectionRight = false;

                if (transform.position.x <= -offset)
                    isMovementDirectionRight = true;
            }
        }

        public void StopBrickMovement()
        {
            isMove = false;
        }

        public void DisableCounter()
        {
            counter++;
            if(counter >= DisableAfterCount)
            {
                counter = 0;
                Spawner.Instance.objectPool.Enqueue(gameObject);
                gameObject.SetActive(false);
            }
        }

        public void Deactivate(float _delay = 10f)
        {
            rb2D.isKinematic = false;
            StartCoroutine(DeactivateAfter(_delay));
        }

        IEnumerator DeactivateAfter(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            rb2D.isKinematic = true;
            Spawner.Instance.objectPool.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}