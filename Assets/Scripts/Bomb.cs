using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour {
    const int SMALL_BOMB = 0;
    const int BIG_BOMB = 1;

    int bombType;

    class BombStat {
        public int damage;
        public float radius;

        public BombStat(int dmg, float r) {
            damage = dmg;
            radius = r;
        }
    }

    private List<BombStat> bombStats = new List<BombStat>();

    void Start () {
        initBombTypes();
        bombType = SMALL_BOMB;
    }

    void initBombTypes() {
        bombStats.Add(new BombStat(35, 1.5f)); // SMALL_BOMB
        bombStats.Add(new BombStat(10, 5)); // BIG_BOMB
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Terrain") {
            checkTankCollision();
            GameObject.FindWithTag("Terrain").GetComponent<Land>().destroyLand(GetComponent<CircleCollider2D>(), bombStats[bombType].radius);
            Destroy(this.gameObject);
        }
    }

    void checkTankCollision() {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        float playerDistance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.y - transform.position.y, 2));
        float enemyDistance = Mathf.Sqrt(Mathf.Pow(enemy.transform.position.x - transform.position.x, 2) + Mathf.Pow(enemy.transform.position.y - transform.position.y, 2));

        float tolerance = 0.5f;

        if (playerDistance < (bombStats[bombType].radius + tolerance)) {
            player.GetComponent<Player>().loseHp(bombStats[bombType].damage);
        }

        if (enemyDistance < (bombStats[bombType].radius + tolerance)) {
            enemy.GetComponent<Enemy>().loseHp(bombStats[bombType].damage);
        }
    }
}
