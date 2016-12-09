using UnityEngine;
using System.Collections;

public class TestVoisins : MonoBehaviour {

    void Start() {
        GetComponent<Inflammable>().Mark(1,0.5f,1);
    }

    // Use this for initialization
    void MyStart () {

        Vector3 f1 = transform.position;
        Vector3 direction = new Vector3(Mathf.Cos(GlobalVariables.windDirection), 0, Mathf.Sin(GlobalVariables.windDirection));
        float focalDist = GlobalVariables.windPower * GlobalVariables.minRadiusFire / 25;
        Vector3 f2 = f1 + (direction * focalDist);
        float maxDistance = focalDist + GlobalVariables.minRadiusFire; // modification de la distance max
                                                                 //Tableau contenant les colliders proches
        Collider[] closeColliders = Physics.OverlapSphere(transform.position, maxDistance);
        //Pour chacun d'entre eux
        foreach (Collider closeCollider in closeColliders) {
            //Recuperer le composant inflammable
            Inflammable closeInflammable = closeCollider.GetComponentInParent<Inflammable>();
            if (closeInflammable != null && closeInflammable != this) {     //si non nul et non this
                
                if (((f1 - closeInflammable.transform.position).magnitude +
                    (f2 - closeInflammable.transform.position).magnitude) <= maxDistance) { // s'il se trouve dans l'ellipse
                    closeInflammable.Mark(1,0,0);
                }
                /*
                Vector3 of1 = closeInflammable.transform.position;
                Vector3 of2 = of1 + (direction * focalDist);
                if (((of1 - transform.position).magnitude +
                    (of2 - transform.position).magnitude) <= maxDistance) {
                    closeInflammable.Mark(0,0,1);
                }
                */
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GlobalVariables.State == 1) MyStart();
	}
}
