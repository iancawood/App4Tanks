using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
    public const int SMALL_BOMB = 1;
    public const int BIG_BOMB = 2;

    public int bombType;

    void Start () {
        bombType = 1;
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Terrain") {
            GameObject.FindWithTag("Terrain").SendMessage("destroyTerrain", GetComponent<CircleCollider2D>());
            Destroy(this.gameObject);
        }
    }
}
