﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour {
    public Transform explosionAnimation;

    public const int SMALL_BOMB = 0;
    public const int BIG_BOMB = 1;
    public const int VOLCANO = 2;
    public const int MINI_VOLCANO = 3;
    public const int THREE_STAGE = 4;
    public const int THREE_STAGE_SECOND = 5;
    public const int THREE_STAGE_THIRD = 6;

    int bombType = SMALL_BOMB;
    int volcanoChildren = 5;
    int miniVolVerticalForce = 400;
    int miniVolForceOffset = 100;

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
    }

    void Update() {
        if (Mathf.Abs(transform.position.x) > 50 || Mathf.Abs(transform.position.y) > 50) {
            Destroy(this.gameObject);
        }
    }

    void initBombTypes() {
        bombStats.Add(new BombStat(15, 2)); // SMALL_BOMB
        bombStats.Add(new BombStat(10, 3)); // BIG_BOMB
        bombStats.Add(new BombStat(0, 0)); // VOLCANO
        bombStats.Add(new BombStat(5, 1.5f)); // MINI_VOLCANO
        bombStats.Add(new BombStat(5, 1)); // THREE_STAGE
        bombStats.Add(new BombStat(5, 2)); // THREE_STAGE_SECOND
        bombStats.Add(new BombStat(5, 3)); // THREE_STAGE_THIRD
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Terrain") {
            explode();

            if (bombType == VOLCANO) {
                volcano();
            } 

            if (bombType == THREE_STAGE) {
                StartCoroutine(threeStage());
            } else {
                Destroy(this.gameObject);
            }
        }
    }

    void explode() {
        createExplosionAnimation();
        checkTankCollision();
        GameObject[] terrainBlocks = GameObject.FindGameObjectsWithTag("Terrain");
        foreach(GameObject terrainBlock in terrainBlocks) {
            terrainBlock.GetComponent<Land>().destroyLand(GetComponent<CircleCollider2D>(), bombStats[bombType].radius);
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

    public void setBombType(int type) {
        bombType = type;
    }

    void createExplosionAnimation() { 
        Vector3 offset = new Vector3(0, -0.5f, 0);
        Transform animation = Instantiate(explosionAnimation, transform.position + offset, transform.rotation) as Transform;

        float animationToWorldRatio = 2.5f; // Animation is about 2.5 world units wide when scale is 1
        float scale = bombStats[bombType].radius / animationToWorldRatio;
        if (scale <= 0) {
            scale = 1 / animationToWorldRatio;
        }

        animation.localScale = new Vector3(scale, scale, 0);
    }

    void volcano() {
        for (int i = 0; i < volcanoChildren; i++) {
            GameObject child = Instantiate(gameObject);
            child.transform.localScale *= 0.5f;
            child.GetComponent<Bomb>().setBombType(MINI_VOLCANO);
            child.GetComponent<Rigidbody2D>().AddForce(new Vector3(
                Random.Range(-miniVolForceOffset, miniVolForceOffset),
                miniVolVerticalForce + Random.Range(-miniVolForceOffset, miniVolForceOffset),
                0
            ));
        }
    }

    IEnumerator threeStage() {
        Destroy(GetComponent<SpriteRenderer>());
        GetComponent<Rigidbody2D>().isKinematic = true;

        float delay = 0.65f;
        yield return new WaitForSeconds(delay);
        bombType = THREE_STAGE_SECOND;
        explode();
        yield return new WaitForSeconds(delay);
        bombType = THREE_STAGE_THIRD;
        explode();

        Destroy(this.gameObject);
    }
}
