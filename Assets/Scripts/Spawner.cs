using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public Transform land;
    public Transform player;
    public Transform enemy;

	void Start () {
        int landOrientation = 1;

        switch(landOrientation) {
            case 1:  // flat
                flat();
                break;
            case 2:  // u shape
                break;
            case 3:  // islands
                break;
            default:
                flat();
                break;
        }
	}

    void flat() {
        Vector3 playerSpawn = new Vector3(-8, 4.6f, 0);
        Vector3 enemySpawn = new Vector3(8, 4.6f, 0);
        Vector3 landOneSpawn = new Vector3(-7.5f, -3.5f, 0);
        Vector3 landTwoSpawn = new Vector3(7.5f, -3.5f, 0);

        player.position = playerSpawn;
        enemy.position = enemySpawn;

        Instantiate(land, landOneSpawn, Quaternion.identity);
        Instantiate(land, landTwoSpawn, Quaternion.identity);
    }
}
