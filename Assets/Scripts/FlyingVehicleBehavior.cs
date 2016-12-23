using UnityEngine;
using System.Collections;

public enum FlyingVehicle{
    Helicopter,
    Plane
}

public class FlyingVehicleBehavior : MonoBehaviour {
    public FlyingVehicle vehicleType;
    private float speed; 
    public GameObject WaterStreamFX;
    private bool isBombarding = false;
    private bool isEmpty = false;
    private Vector3 bombardmentLocation;
    private float bombardmentDistance; 

    void Start() {
        bombardmentDistance = 20f;
        if (vehicleType == FlyingVehicle.Helicopter)
            speed = 100;
        else
            speed = 200;
    }
    
    void Update() {
        UpdateMovement();
        if (isEmpty) return;

        CheckBombardmentDistance();
        if (isBombarding) {
            Vector3 currentBombardmentPosition = transform.position;
            currentBombardmentPosition.y = 0;
            foreach (Collider collider in Physics.OverlapSphere(currentBombardmentPosition, 40f)) {
                if (collider.transform.parent != null)
                    if (collider.transform.parent.tag == "Tree")
                        collider.GetComponentInParent<Inflammable>().WateredHelicopter();

            }
        }

    }

    void UpdateMovement() {

        transform.position += transform.forward * Time.deltaTime * speed * GlobalVariables.Speed;

    }

    void CheckBombardmentDistance() {
        if (bombardmentLocation == Vector3.zero) return;
        Vector2 myPosition2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 bombardmentPosition2D = new Vector2(bombardmentLocation.x, bombardmentLocation.z);
        float myDistance = Vector2.Distance(myPosition2D, bombardmentPosition2D);
        if (!isBombarding && myDistance < bombardmentDistance)
            StartBombarding();
        else if (isBombarding && myDistance > bombardmentDistance) {
            StopBombarding();
            StartCoroutine(DelayedDestroy(4));

        }
    }

    void CheckDestroyDistance() {
        if (bombardmentLocation == Vector3.zero) return;
        Vector2 myPosition2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 bombardmentPosition2D = new Vector2(bombardmentLocation.x, bombardmentLocation.z);
        float myDistance = Vector2.Distance(myPosition2D, bombardmentPosition2D);
        if (myDistance > 3 * bombardmentDistance)
            Destroy(this.gameObject);
    }

    void StartBombarding() {
        isBombarding = true;
        WaterStreamFX.SetActive(true);
    }

    public void SetBombardmentLocation(Vector3 bombardmentPoint) {
        bombardmentLocation = bombardmentPoint;
    }

    void StopBombarding() {
        isBombarding = false;
        WaterStreamFX.GetComponentInChildren<EllipsoidParticleEmitter>().emit = false;
        isEmpty = true;
    }

    IEnumerator DelayedDestroy(float s) {
        yield return new WaitForSeconds(s);
        Destroy(this.gameObject);
    }

}
