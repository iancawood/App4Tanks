using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    public Arrow arrow;
    public GameObject bomb;
    public Knob knob;
    public Text playerHpText;
    public TurnManager turnManager;

    private int hp = 100;
    private float speed = 1.0f;
    private int maxKnobDist = 4;
    private int forceScale = 200;

    void Start () {
        updateHealthText();
    }
	
	void Update () {
        if (turnManager.playerTurn) {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * speed * Time.deltaTime;

            if (Input.GetMouseButtonDown(0)) {
                Vector3 worldMouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                adjustAim(new Vector2(transform.position.x, transform.position.y), new Vector2(worldMouse.x, worldMouse.y));
            }

            if (Input.GetKeyDown("space")) {
                shoot();
                //turnManager.nextTurn();
            }
        }
    }

    void adjustAim(Vector2 playerPos, Vector2 mousePos) {
        Vector2 difference = mousePos - playerPos;
        if (difference.magnitude < maxKnobDist) {
            knob.setPosition(mousePos.x, mousePos.y);
            arrow.redraw(new Vector3(playerPos.x, playerPos.y, 0), new Vector3(mousePos.x, mousePos.y, 0));
        }
    }

    void shoot() {
        Vector3 forceVector = knob.transform.position - transform.position;
        GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation) as GameObject;
        newBomb.GetComponent<Rigidbody2D>().AddForce(forceScale * forceVector);
    }

    public void loseHp(int dmg) {
        hp -= dmg;

        if (hp <= 0) {
            turnManager.gameOver();
        }

        updateHealthText();
    }

    void updateHealthText() {
        playerHpText.text = "Player: " + hp.ToString();
    }
}
