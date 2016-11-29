using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
    public Transform player;
    public GameObject bomb;
    public Text enemyHpText;
    public TurnManager turnManager;

    private float theta = 0.785398f; // 45 degrees
    private bool routineStarted = false;
    private int hp = 100;

    void Update () {
        if (!turnManager.playerTurn && !routineStarted) {
            routineStarted = true;
            StartCoroutine(enemyTurn());
        }

        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 30) {
            turnManager.gameOver(true);
            Destroy(this.gameObject);
        }
    }
	
	void Start () {
        updateHealthText();
    }

    IEnumerator enemyTurn() {
        yield return new WaitForSeconds(3);
        shoot();
        yield return new WaitForSeconds(3);
        turnManager.nextTurn();
        routineStarted = false;
    }

    void shoot() {
        float deltaX = player.position.x - transform.position.x;
        float deltaY = player.position.y - transform.position.y;
        float force = approximateForce(deltaX);
        float offset = Random.Range(0, 0.25f * Mathf.Abs(force));

        Vector3 forceVector = new Vector3(force, Mathf.Abs(force) + (deltaY >= 0 ? offset : -offset), 0);
        GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation) as GameObject;
        newBomb.GetComponent<Rigidbody2D>().AddForce(forceVector);
    }

    float approximateForce(float deltaX) { // used by sampling data and throwing it in excel
        float absoluteX = Mathf.Abs(deltaX);
        float y = (-0.4241f * absoluteX * absoluteX) + (27.575f * absoluteX) + 116.12f;
        return deltaX > 0 ? y : -y;
    }

    Vector3 calculateForce() { // real math, not actually using this, because of some error
        // Find initial velocity
        float deltaX = player.position.x - transform.position.x;
        float deltaY = player.position.y - transform.position.y;
        float tanTheta = Mathf.Tan(theta);
        float cosSquaredTheta = Mathf.Pow(Mathf.Cos(theta), 2);

        float initVelSquared = (Physics2D.gravity.y * deltaX * deltaX) / (2 * cosSquaredTheta * (deltaY - deltaX * tanTheta));
        float initialVelocity = Mathf.Sqrt(Mathf.Abs(initVelSquared));

        if (initVelSquared < 0) {
            initialVelocity *= -1;
        }

        // Find initial force
        float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
        float bombMass = bomb.GetComponent<Rigidbody2D>().mass;
        float force = bombMass * initialVelocity * initialVelocity / distance;

        Vector3 forceVector = new Vector3(force * Mathf.Cos(theta), force * Mathf.Sin(theta), 0);
        if (deltaX < 0) {
            forceVector.x *= -1;
        }
        return forceVector;
    }

    public void loseHp(int dmg) {
        hp -= dmg;
        if (hp <= 0) {
            turnManager.gameOver(true);
        }
        updateHealthText();
    }

    void updateHealthText() {
        enemyHpText.text = "Enemy: " + hp.ToString();
    }

}
