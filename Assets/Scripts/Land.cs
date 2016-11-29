using UnityEngine;
using System.Collections;

/*
 * The logic of this script is based on the logic of this project: https://github.com/marcoprado17/destructible-land-demo
 */

public class Land : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private float widthWorld;
    private float heightWorld;
    private float widthPixel;
    private float heightPixel;

    void Start () {
	    spriteRenderer = GetComponent<SpriteRenderer>();

        Texture2D landTexure = (Texture2D) Resources.Load("Terrain");
        Texture2D alphaClone = new Texture2D(landTexure.width, landTexure.height, TextureFormat.ARGB32, false);
        alphaClone.SetPixels32(landTexure.GetPixels32());
        landTexure = Instantiate(alphaClone);
        spriteRenderer.sprite = Sprite.Create(landTexure, new Rect(0f, 0f, landTexure.width, landTexure.height), new Vector2(0.5f, 0.5f), 100f);

        widthWorld = spriteRenderer.bounds.size.x;
        heightWorld = spriteRenderer.bounds.size.y;
        widthPixel = spriteRenderer.sprite.texture.width;
        heightPixel = spriteRenderer.sprite.texture.height;

        reapplyCollider();
    }
   
    public void destroyLand(CircleCollider2D collider, float worldRadius) {

        Vector2 center = worldCoordsToPixelCoords(collider.bounds.center.x, collider.bounds.center.y - collider.bounds.size.y / 2f);
        int radius = (int)(worldRadius * widthPixel / widthWorld);

        for (int x = -radius; x <= radius; x++) {
            int yBoundary = (int)Mathf.Sqrt(radius * radius - x * x);
            int currentX = (int)center.x + x;

            if (0 <= currentX && currentX <= spriteRenderer.sprite.texture.width) {
                for (int y = 0; y <= yBoundary; y++) {
                    int up = (int)center.y + y;
                    int down = (int)center.y - y;
                    
                    if (0 <= up && up <= spriteRenderer.sprite.texture.height) {
                        spriteRenderer.sprite.texture.SetPixel(currentX, up, Color.clear);
                    }

                    if (0 <= down && down <= spriteRenderer.sprite.texture.height) {
                        spriteRenderer.sprite.texture.SetPixel(currentX, down, Color.clear);
                    }
                }
            }            
        }

        spriteRenderer.sprite.texture.Apply();
        reapplyCollider();
    }

    Vector2 worldCoordsToPixelCoords(float x, float y) {
        float dx = x - transform.position.x;
        float dy = y - transform.position.y;

        return new Vector2(
            0.5f * widthPixel + dx * widthPixel / widthWorld,
            0.5f * heightPixel + dy * heightPixel / heightWorld
        );
    }

    void reapplyCollider() {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
