using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour {
    const int SMALL_BOMB = 0;
    const int BIG_BOMB = 0;

    private int bombType;

    public Land land;

    class BombStat {
        public int damage;
        public float explosionFactor;

        public BombStat(int dmg, float eFactor) {
            damage = dmg;
            explosionFactor = eFactor;
        }
    }

    private List<BombStat> bombStats = new List<BombStat>();

    void Start () {
        initBombTypes();

        bombType = SMALL_BOMB;
    }

    void initBombTypes() {
        bombStats.Add(new BombStat(5, 2)); // SMALL_BOMB
        bombStats.Add(new BombStat(10, 5)); // BIG_BOMB
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Terrain") {
            Debug.Log(bombStats[bombType].explosionFactor);
            GameObject.FindWithTag("Terrain").GetComponent<Land>().destroyLand(GetComponent<CircleCollider2D>(), bombStats[bombType].explosionFactor);
            //Object[] paramaters = {GetComponent<CircleCollider2D>(), bombStats[bombType] as Object};
            //GameObject.FindWithTag("Terrain").SendMessage("destroyTerrain", );
            //land.destroyLand(GetComponent<CircleCollider2D>());
            Destroy(this.gameObject);
        }
    }
}
