using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform player;

    private float theta = 0.785398f; // 45 degrees

    private const float gravity = -9.8f;

	void Start () {
	
	}
	
	void Awake () {
        calculateForce();
    }

    void calculateForce() {
        float deltaX = player.position.x - transform.position.x;
        float deltaY = player.position.y - transform.position.y;
        float tanTheta = Mathf.Tan(theta);
        float cosSquaredTheta = Mathf.Pow(Mathf.Cos(theta), 2);

        float initVelSquared = (gravity * deltaX * deltaX) / (2 * cosSquaredTheta * (deltaY - deltaX * tanTheta));

        float initialVelocity = Mathf.Sqrt(Mathf.Abs(initVelSquared));

        if (initVelSquared < 0) {
            initialVelocity = -initialVelocity;
        }

        Debug.Log(initialVelocity);
    }
    
}
