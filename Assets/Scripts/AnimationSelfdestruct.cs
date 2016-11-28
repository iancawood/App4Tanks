using UnityEngine;
using System.Collections;

public class AnimationSelfdestruct : MonoBehaviour {
    public float delay = 0f;

    void Start() {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        Destroy(transform.parent.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
