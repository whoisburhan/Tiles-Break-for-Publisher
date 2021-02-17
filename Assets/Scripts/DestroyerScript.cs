using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    public LayerMask attackMask;
    private Vector2 point = new Vector2(0f, -0.5f);
    private Vector2 size; //= new Vector2(Screen.width, 1f);
    public AudioSource sound;

    private void Start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2f,0f));
        Vector3 offset = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f));
        size = new Vector2(offset.x * 2, 1f);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos += transform.right * point.x;
        pos += transform.up * point.y;
        Collider2D colInfo = Physics2D.OverlapBox(pos, size, 0, attackMask);

        if (colInfo != null)
        {
            if (colInfo.tag == "Tiles")
            {
                AudioManager.Instance.AudioChangeFunc(0, 1, false, 1.4f);
                // Debug.Log("Its Works!!!");
                Controller.instance.isGameOver = true;
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * point.x;
        pos += transform.up * point.y;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, size);

    }
}
