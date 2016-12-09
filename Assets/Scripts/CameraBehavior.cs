using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    float minFov = 10f;
    float maxFov = 90f;
    float sensitivityScroll = 20f;
    float sensitivityMove = 20f;


    void LateUpdate () {
        //scroll
        if (Input.mouseScrollDelta != Vector2.zero) {
            float fov = Camera.main.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivityScroll;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }


        //move
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(new Vector3(sensitivityMove * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(new Vector3(-sensitivityMove * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(new Vector3(0, -sensitivityMove * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(new Vector3(0, sensitivityMove * Time.deltaTime, 0));
        }



    }
}
