using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.TilesBreak
{
    public enum Type
    {
        leftAngle, normal, rightAngle
    }

    public class TileScript : MonoBehaviour
    {
        private float speed = 5f;
        private Rigidbody2D rb2d;
        private Transform targetPos;
        private Vector2 tarPos;
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();

            speed = Controller.instance.tileSpeed;
        }

        private void Update()
        {
            if (Controller.instance.isGameOver)
            {
                Destroy(this.gameObject);
            }
        }

        public void Move(bool isDiagonalMoveAvailable)
        {
            if (!isDiagonalMoveAvailable)
            {
                rb2d.velocity = new Vector2(0, -speed);
            }
            else
            {
                int x = Random.Range(0, 6);
                if (x == 5)
                {
                    tarPos = (GameObject.Find("SP4").transform.position - transform.position).normalized;
                    rb2d.velocity = tarPos * speed;
                }
                else if (x == 4)
                {
                    tarPos = (GameObject.Find("SP7").transform.position - transform.position).normalized;
                    rb2d.velocity = tarPos * speed;
                }
                else
                {
                    rb2d.velocity = new Vector2(0, -speed);
                }
            }
        }
    }
}
