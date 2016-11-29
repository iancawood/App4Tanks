using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    public Arrow arrow;
    public GameObject bomb;
    public Knob knob;
    public Text playerHpText;
    public TurnManager turnManager;
    public Text bombText;

    private int hp = 100;
    private float speed = 2.0f;
    private float maxKnobDist = 4.5f;
    private int forceScale = 200;
    private int selectedBombType;

    void Start () {
        updateHealthText();
        selectedBombType = Bomb.SMALL_BOMB;
        updateBombText();
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
                turnManager.nextTurn();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) { // small bomb
                selectedBombType = Bomb.SMALL_BOMB;
                updateBombText();
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) { // big bomb
                selectedBombType = Bomb.BIG_BOMB;
                updateBombText();
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) { // volcano
                selectedBombType = Bomb.VOLCANO;
                updateBombText();
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) { // three_stage
                selectedBombType = Bomb.THREE_STAGE;
                updateBombText();
            }
        }

        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 30) {
            turnManager.gameOver(false);
            Destroy(this.gameObject);
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
        newBomb.GetComponent<Bomb>().setBombType(selectedBombType);
        newBomb.GetComponent<Rigidbody2D>().AddForce(forceScale * forceVector);
    }

    public void loseHp(int dmg) {
        hp -= dmg;
        if (hp <= 0) {
            turnManager.gameOver(false);
        }
        updateHealthText();
    }

    void updateHealthText() {
        playerHpText.text = "Player: " + hp.ToString();
    }

    void updateBombText() {
        string bombName = "";
        switch(selectedBombType) {
            case Bomb.SMALL_BOMB:
                bombName = "Small";
                break;
            case Bomb.BIG_BOMB:
                bombName = "Big";
                break;
            case Bomb.VOLCANO:
                bombName = "Volcano";
                break;
            case Bomb.THREE_STAGE:
                bombName = "Three Stage";
                break;
            default:
                break;
        }
        bombText.text = "Bomb: " + bombName;
    }
}
