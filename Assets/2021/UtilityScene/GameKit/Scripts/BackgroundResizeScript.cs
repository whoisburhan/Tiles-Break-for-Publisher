using UnityEngine;

namespace GS
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundResizeScript : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            float height = Camera.main.orthographicSize * 2;
            float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

            Sprite s = spriteRenderer.sprite;
            float unitWidth = s.textureRect.width / s.pixelsPerUnit;
            float unitHeight = s.textureRect.height / s.pixelsPerUnit;

            spriteRenderer.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);
        }
    }
}
