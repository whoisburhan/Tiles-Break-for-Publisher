using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHealth : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] tileLevelSprites;
    public Color[] colors;
    public int tileLvl;

    private void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
        //sr.color = colors[Random.Range(0, colors.Length)];
    }

    public void TileLvl(int x)
    {
        if (x == 4)
        {
           // sr.sprite = tileLevelSprites[3];
            sr.gameObject.tag = "Bomb";
        }
        else
        {
            sr.gameObject.tag = "Tiles";
            tileLvl = x;
          //  sr.color = colors[tileLvl - 1];
          //  sr.sprite = tileLevelSprites[tileLvl - 1];
        }

        var col = this.gameObject.AddComponent<PolygonCollider2D>();
        col.isTrigger = true;
    }

    public void TileHealthUpdate()
    {
        tileLvl--;
        if(tileLvl<=0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sr.sprite = tileLevelSprites[tileLvl-1];
            sr.color = colors[tileLvl - 1];
        }
    }
}
