using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.ColorSwitch
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 45f;

        private void Update()
        {
            transform.Rotate(new Vector3(0f, 0f, rotateSpeed) * Time.deltaTime);
        }
    }
}