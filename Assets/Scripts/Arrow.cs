using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    private float originalScaleX;
    private float originalScaleY;

    void Start() {
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;
    }

    public void redraw(Vector3 tail, Vector3 tip) {
        transform.position = (tip + tail) / 2;

        Vector3 relative = tip - tail;

        transform.localScale = new Vector3(originalScaleX, originalScaleY * relative.magnitude / 2, 0);

        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }
}
