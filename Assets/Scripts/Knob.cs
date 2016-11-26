using UnityEngine;
using System.Collections;

public class Knob : MonoBehaviour {
    private Vector3 resetPosition = new Vector3(1f, 1f, 0);

    public void setPosition(float x, float y) {
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void reset() {
        transform.position = resetPosition;
    }
}
