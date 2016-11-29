using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public Transform land;
    public Transform player;
    public Transform enemy;

    public const int MAP_FLAP = 1;
    public const int MAP_USHAPE = 2;
    public const int MAP_ISLANDS = 3;


    void Start () {
        int landOrientation = 1;

        switch(landOrientation) {
            case MAP_FLAP: // flat
                flat();
                break;
            case MAP_USHAPE: // u shape
                ushape();
                break;
            case MAP_ISLANDS: // islands
                islands();
                break;
            default:
                flat();
                break;
        }
	}

    void flat() {
        Vector3 playerSpawn = new Vector3(-8, 4.6f, 0);
        Vector3 enemySpawn = new Vector3(8, 4.6f, 0);
        Vector3 scale = new Vector3(3, 3, 0);
        Vector3[] landBlocks = new Vector3[] {
            new Vector3(-7.5f, -3.5f, 0),
            new Vector3(7.5f, -3.5f, 0)
        };

        player.position = playerSpawn;
        enemy.position = enemySpawn;

        spawnLandBlocks(landBlocks, scale);
    }

    void ushape() {
        Vector3 playerSpawn = new Vector3(-8, 8.1f, 0);
        Vector3 enemySpawn = new Vector3(8, 8.1f, 0);
        Vector3 scale = new Vector3(2, 2, 0);
        Vector3[] landBlocks = new Vector3[] {
            new Vector3(-10, 2.5f, 0),
            new Vector3(10, 2.5f, 0),
            new Vector3(-10, -7.5f, 0),
            new Vector3(10, -7.5f, 0),
            new Vector3(0, -7.5f, 0)
        };

        player.position = playerSpawn;
        enemy.position = enemySpawn;

        spawnLandBlocks(landBlocks, scale);
    }

    void islands() {
        Vector3 playerSpawn = new Vector3(-12, 4.4f, 0);
        Vector3 enemySpawn = new Vector3(12, 4.4f, 0);
        Vector3 scale = new Vector3(3, 3, 0);
        Vector3[] landBlocks = new Vector3[] {
            new Vector3(-12, -3.75f, 0),
            new Vector3(12, -3.75f, 0)
        };

        player.position = playerSpawn;
        enemy.position = enemySpawn;

        spawnLandBlocks(landBlocks, scale);
    }

    void spawnLandBlocks(Vector3[] positions, Vector3 scale) {
        foreach (Vector3 position in positions) {
            Transform landBlock = Instantiate(land, position, Quaternion.identity) as Transform;
            landBlock.localScale = scale;
        }
    }
}
