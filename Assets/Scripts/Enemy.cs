using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform player;
    public GameObject bomb;

    private float theta = 0.785398f; // 45 degrees
    private int forceScale = 65;

    void Update () {
        if (Input.GetKeyDown(KeyCode.M)) {
            shoot();
        }
    }
	
	void Awake () {
        //calculateForce();
    }

    void shoot() {
        Vector3 forceVector = calculateForce();
        GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation) as GameObject;
        newBomb.GetComponent<Rigidbody2D>().AddForce(forceScale * forceVector);
    }

    Vector3 calculateForce() {
        // Find initial velocity
        float deltaX = player.position.x - transform.position.x;
        float deltaY = player.position.y - transform.position.y;
        float tanTheta = Mathf.Tan(theta);
        float cosSquaredTheta = Mathf.Pow(Mathf.Cos(theta), 2);

        float initVelSquared = (Physics2D.gravity.y * deltaX * deltaX) / (2 * cosSquaredTheta * (deltaY - deltaX * tanTheta));
        float initialVelocity = Mathf.Sqrt(Mathf.Abs(initVelSquared));

        Debug.Log("init velocity: " + initialVelocity);

        if (initVelSquared < 0) {
            initialVelocity *= -1;
        }

        float time = deltaX / (initialVelocity * Mathf.Cos(theta));
        Debug.Log("time: " + time);

        // Find initial force
        float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
        float bombMass = bomb.GetComponent<Rigidbody2D>().mass;
        float force = bombMass * initialVelocity * initialVelocity / distance;

        Vector3 forceVector = new Vector3(force * Mathf.Cos(theta), force * Mathf.Sin(theta), 0);
        if (deltaX < 0) {
            forceVector.x *= -1;
        }
         
        Debug.Log("force vector" + forceVector);
        return forceVector;
    }
    
}
