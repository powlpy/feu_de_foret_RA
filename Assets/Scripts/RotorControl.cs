using UnityEngine;
using System.Collections;

public class RotorControl : MonoBehaviour {

    public bool IsTurning = true;
    private float speed;

    void Start() {
        speed = 10f;
    }

    void Update() {
        if (IsTurning) {
            transform.Rotate(new Vector3(0, 0, speed));
        }
    }

}
