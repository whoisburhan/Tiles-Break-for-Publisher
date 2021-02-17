using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSize : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    float tileSizeInXDirection;
    float totalCutOutOffset;
    private float cutOutOffset = 1f;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        totalCutOutOffset = cutOutOffset * 5f;
        tileSizeInXDirection = (100 - totalCutOutOffset) / 4;

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

        width = (width / 100) * tileSizeInXDirection;
        height = (height / 100) * 25f;

        Sprite s = spriteRenderer.sprite;
        float unitWidth = s.textureRect.width / s.pixelsPerUnit;
        float unitHeight = s.textureRect.height / s.pixelsPerUnit;

        spriteRenderer.transform.localScale = new Vector3(width / unitWidth, width / unitWidth);

        
    }

}
