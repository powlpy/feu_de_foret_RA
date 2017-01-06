using UnityEngine;
using System.Collections;

public class FiretruckBehavior : MonoBehaviour {

    public float speed;
    private bool isMoving = true;
    public GameObject WaterStreamFX;
    private bool isFighting = false;
    Vector3 fightingLocation;
    Inflammable myTree;

    void Awake() {

    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //if (GlobalVariables.State == 0) return;
        if (isMoving)
            UpdateMovement();
        /*   fun
        else 
            WaterStreamFX.transform.Rotate(new Vector3(0, 0, 1));
            */
	}

    void UpdateMovement() {
        if (GlobalVariables.State == 0) return;
        if (!isMoving) return;

		Vector3 d = transform.right;
		d.y = 0f;
        transform.position += d * Time.deltaTime * speed * GlobalVariables.Speed;

		//Debug.Log ("direc : " + transform.right);
		//Debug.Log ("pos : " + transform.position);

    }

    void OnTriggerEnter(Collider collider) {
        if (collider.transform.parent != null) {
            if (collider.transform.parent.tag == "Tree") {
                isMoving = false;
                if (collider.transform.parent.GetComponent<Inflammable>().IsBurning())
                    FightFire(collider.transform.parent.GetComponent<Inflammable>());
                else {
                    foreach (Collider tree in Physics.OverlapSphere(transform.position, 70f)) {
                        if (tree.transform.parent != null)
                            if (tree.transform.parent.tag == "Tree")
                                if (tree.GetComponentInParent<Inflammable>().IsBurning())
                                    FightFire(tree.GetComponentInParent<Inflammable>());

                    }
                }
                InvokeRepeating("CheckFireNeighbors", 4f, 4f);
            }
        } 
    }

    void CheckFireNeighbors() {
        if (myTree != null) {
            bool continueFighting = false;
            Collider[] closeTrees = Physics.OverlapSphere(myTree.transform.position, 30f);
            int i = 0;
            while (!continueFighting && i < closeTrees.Length) {
                Collider tempTree = closeTrees[i];
                if (tempTree.transform.parent != null)
                    if (tempTree.transform.parent.tag == "Tree")
                        if (tempTree.GetComponentInParent<Inflammable>().IsBurning()) {
                            continueFighting = true;
                        }
                i++;
            }
            if(!continueFighting)
                StopFighting();
        }

        if (!isFighting) {
            foreach (Collider tree in Physics.OverlapSphere(transform.position, 70f))
                if (tree.transform.parent != null)
                    if (tree.transform.parent.tag == "Tree")
                        if (tree.GetComponentInParent<Inflammable>().IsBurning())
                            FightFire(tree.GetComponentInParent<Inflammable>());
        }
    }


    void FightFire(Inflammable burningTree) {
        myTree = burningTree;
        Vector3 treePosition = burningTree.transform.position;
        if (isFighting) return;
        isFighting = true;
        isMoving = false;
        WaterStreamFX.SetActive(true);
        WaterStreamFX.GetComponentInChildren<EllipsoidParticleEmitter>().emit = true;

        //orienter le jet d'eau vers l'arbre le plus proche
        float myAngle = Vector3.Angle(transform.right, treePosition - transform.position);
        if (Vector3.Cross(transform.right, treePosition - transform.position).y < 0)
            myAngle = -myAngle;
        WaterStreamFX.transform.Rotate(new Vector3(0, 0, myAngle));

        //Vector3 myCenter = transform.TransformPoint(WaterStreamFX.GetComponent<SphereCollider>().center);
        Vector3 myCenter = WaterStreamFX.GetComponentInChildren<SphereCollider>().transform.position;
        foreach (Collider collider in Physics.OverlapSphere(myCenter, 30f)) {
            if (collider.transform.parent != null)
                if (collider.transform.parent.tag == "Tree")
                    collider.GetComponentInParent<Inflammable>().Watered();
        }
    }

    void StopFighting() {
        isFighting = false;
        myTree = null;
        WaterStreamFX.GetComponentInChildren<EllipsoidParticleEmitter>().emit = false;

    }

    public void SetFightingLocation(Vector3 location) {
        fightingLocation = location;
    }

}
