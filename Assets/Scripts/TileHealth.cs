using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHealth : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] tileLevelSprites;
    public Color[] colors;
    public int tileLvl;
    [SerializeField]private GameObject childShine;

    private bool isDissolving = false;
    private float dissolveAmount = 0f;
    private float dissolveSpeed = 5f;

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

    public void SetIsDissolving()
    {
        isDissolving = true;
        if (childShine != null)
        {
            childShine.GetComponent<UI_Shine>().KillTween();
            childShine.SetActive(false);
        }
        if(GetComponent<Collider>() != null)
            GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (isDissolving)
        {
            dissolveAmount =Mathf.Clamp01(dissolveAmount + (Time.deltaTime * dissolveSpeed));
            GetComponent<SpriteRenderer>().material.SetFloat("_DissolveAmount",dissolveAmount);
        }

        if (dissolveAmount >= 1)
            Destroy(this.gameObject);
    }
}
