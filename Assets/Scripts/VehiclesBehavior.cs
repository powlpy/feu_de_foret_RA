using UnityEngine;
using System.Collections;

public class VehiclesBehavior : MonoBehaviour {

    public GameObject FireTruck;
    public GameObject FireHelicopter;
    public GameObject FirePlane;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GoFireTrucks();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            GoFlyingVehicle(0);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            GoFlyingVehicle(1);
    }

    public void GoFireTrucks() {
        if (GlobalVariables.State == 0) return;
        //  a   b
        //  c   d
        Vector3 minBounds = GlobalVariables.GetBoundingBoxMin();
        Vector3 maxBounds = GlobalVariables.GetBoundingBoxMax();
        Vector3 flagPosition = GameObject.Find("Cylinder").transform.position;
        
        bool isValid = false;
        foreach (Collider collider in Physics.OverlapSphere(flagPosition, 3f))
            if (collider.transform.parent != null)
                if (collider.transform.parent.tag == "Tree")
                    isValid = true;
        if (!isValid)
            return;

        float boundsSize = Vector3.Distance(maxBounds, minBounds);
        Vector3 centerBounds = (minBounds + maxBounds) / 2;
        Vector3 myDirection = flagPosition - centerBounds;
        myDirection.y = 0;
        myDirection.Normalize();
        //myDirection += new Vector3(Random.value / 2f, 0, Random.value / 2f);
        Vector3 myPosition = flagPosition + myDirection * (0.5f * boundsSize);
        
        GameObject fireTruck = (GameObject)Instantiate(FireTruck);
        myPosition.y = 0.25f;
        fireTruck.transform.position = myPosition;
        fireTruck.transform.LookAt(flagPosition);
        fireTruck.transform.Rotate(-90f, 0f, -90f);
        fireTruck.GetComponent<FiretruckBehavior>().SetFightingLocation(flagPosition);


    }

    public void GoFlyingVehicle(int type) {

        if (GlobalVariables.State == 0) return;

        GameObject myPrefab;
        if (type == 0) myPrefab = FireHelicopter;
        else myPrefab = FirePlane;

        Vector3 bombardmentPosition = GameObject.Find("Cylinder").transform.position;

        GameObject flyingVehicle = (GameObject)Instantiate(myPrefab);
        Vector3 myPosition = RandomCircle(bombardmentPosition, 75f);
        myPosition.y = 15f;
        flyingVehicle.transform.position = myPosition;
        flyingVehicle.transform.LookAt(bombardmentPosition);
        Quaternion myRotation = flyingVehicle.transform.rotation;
        myRotation.x = 0f;
        myRotation.z = 0f;
        flyingVehicle.transform.rotation = myRotation;
        flyingVehicle.GetComponent<FlyingVehicleBehavior>().SetBombardmentLocation(bombardmentPosition);
    }

    Vector3 RandomCircle(Vector3 center, float radius) {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

}
