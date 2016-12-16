using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CylinderBehavior : MonoBehaviour {

    public GameObject myTerrain;
    public float Radius;
    RaycastHit hit;
    public int NbTreesCreated = 10;

    public GameObject TreePrefab;

    void Start() {
        GlobalVariables.State = 0;
        Radius = 0.1f;
        Resize();

        /*
        for (int i = 700; i < 800; i += 4)
            for (int j = 600; j < 700; j += 4) {
                GameObject newTree = (GameObject)Instantiate(TreePrefab1);
                newTree.transform.position = new Vector3(i, 0.2f, j);
            }*/

    }

    void Update() {
        Ray ray;
#if UNITY_ANDROID

        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // Mouse : Input.mousePosition
        if (GlobalVariables.State == 0)
        {
           
            if (myTerrain.GetComponentInChildren<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                transform.position = hit.point;
            }

        } else {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {

                if (myTerrain.GetComponentInChildren<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    transform.position = hit.point;
                }
                if (Input.touchCount > 1)//Dont work
                {
                    StartFire();
                }

                if (Input.touchCount > 3)
                {
                    DrawTrees();
                }

            }
        }
    #endif
    #if UNITY_EDITOR
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Mouse : Input.mousePosition
        if (GlobalVariables.State == 0)
        {

            if (myTerrain.GetComponentInChildren<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                transform.position = hit.point;
            }

            if (Input.GetMouseButtonDown(1))
            {
                StartFire();
            }

            if (Input.GetMouseButtonDown(2))
            {
                DrawTrees();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {

                if (myTerrain.GetComponentInChildren<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    transform.position = hit.point;
                }

            }
        }
#endif
    }

    public void Resize() {

        transform.localScale = new Vector3(Radius*2, 0.02f, Radius*2);

    }

    public void StartFire() {

        bool fireStarted = false;
        Collider[] closeColliders = Physics.OverlapSphere(transform.position, Radius*300);
        foreach (Collider closeCollider in closeColliders) {
            Inflammable closeInflammable = closeCollider.GetComponentInParent<Inflammable>();
            //Si il n'est pas inflammable, passe
            if (closeInflammable == null) {
                continue;
            }

            closeInflammable.Ignite();
            fireStarted = true;
            GlobalVariables.SetFirePoint(transform.position);
        }

        if (fireStarted) {
            //this.transform.position = Vector3.zero;
            //gameObject.SetActive(false);
            GetComponent<Renderer>().materials[0].color = new Color(0,0,1,0.267f);
            Radius = 0.1f;
            Resize();
            GlobalVariables.NextState();
        }

    }

    public void DrawTrees() {
        int ar = 300; //Scale pour AR
        for (int i=0; i<NbTreesCreated; i++) {

            float x = (hit.point.x + Random.Range(-Radius* ar, Radius* ar)) ;
            float z = (hit.point.z + Random.Range(-Radius* ar, Radius* ar)) ;

            GameObject newTree = (GameObject)Instantiate(TreePrefab);
            newTree.transform.position = new Vector3(x, hit.point.y, z);

        }
    }
    

}
