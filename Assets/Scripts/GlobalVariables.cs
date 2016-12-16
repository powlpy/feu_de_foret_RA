using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

    public static float Heat = 0;
    public static bool HighQuality = false;
    public static float Speed = 1f;
    public static int State = 0;
    public static Bounds boundingBox;

    public static float windPower = 30f; // in km
    public static float windDirection = Mathf.PI / 4f; // in radian
    public static float minRadiusFire = 10f; // minimal distance of fire radius

    private static Vector3 boundingBoxMin;
    private static Vector3 boundingBoxMax;

    private static Vector3 firePoint;

    public static void NextState() {
        if (State == 0) {
            NextState0to1();
        }
    }

    /*
    void OnDrawGizmos() {
        if (State > 0) {
            Gizmos.color = new Color(0.8f, 0.8f, 0.4f, 0.5F);
            Gizmos.DrawCube(boundingBox.center, boundingBox.size);

        }
    }*/

    static void NextState0to1() {
        State = 1;
        GameObject.Find("Canvas").GetComponentInChildren<CanvasHandler>().StartSimulation();


        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        boundingBox.center += trees[0].transform.position;
        foreach (GameObject tree in trees)
            boundingBox.Encapsulate(tree.transform.position);

        boundingBoxMin = boundingBox.min;
        boundingBoxMax = boundingBox.max;
    }

    public static Vector3 GetBoundingBoxMin() {
        return boundingBoxMin;
    }


    public static Vector3 GetBoundingBoxMax() {
        return boundingBoxMax;
    }

    public static void SetFirePoint(Vector3 fp) {
        firePoint = fp;
    }

    public static Vector3 GetFirePoint() {
        return firePoint;
    }

    public static void SetWindForce(float force) {
        windPower = force * 15f;

    }

    public static void SetWindAngle(float angle) {
        angle = (360 - (angle + 180) + 45) * Mathf.Deg2Rad;
        windDirection = angle;
    }

	public static void Reset () {
		Heat = 0;
		Speed = 1f;
		State = 0;
		windPower = 30f;
		windDirection = Mathf.PI / 4f;
		minRadiusFire = 10f;
	}
}

