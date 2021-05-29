using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.Stack2D;
using System;

namespace GS.Stack2D
{
    public class Controller : MonoBehaviour
    {
        public const string GAME_DATA_STORE_KEY = "HIGHSCORE_STACK2D";

        [SerializeField] private GameObject brickPrefab;

        private bool isPlay = true;

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
            
        }
    }
}