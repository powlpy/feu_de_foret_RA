using UnityEngine;
using System.Collections;

public class TreesSetup : MonoBehaviour {

    public GameObject TreePrefab;

    private float[] sizeSetup = new float[] { 0f, 40f, 40f, 80f, 80f, 150f, 150f};
    private int[] nbTreesSetup = new int[] { 0, 40, 160, 120, 300, 500, 1300 };
    

    public void Setup(int setup) {
        StartCoroutine(MyDelayMethod(setup));
    }


    void DeletreTrees() {
        GameObject[] Trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject Tree in Trees) {
            Destroy(Tree);

        }
    }

    IEnumerator MyDelayMethod(int setup) {
        DeletreTrees();
        yield return new WaitForSeconds(0);
        DoSetup(setup);
    }

    void DoSetup(int setup) {
        
        Random.InitState(45);
        int i = 0;
        while (i < nbTreesSetup[setup]) {
            
            float randX = Random.value;
            float randZ = Random.value;
            randX *= sizeSetup[setup];
            randX -= sizeSetup[setup] / 2f;
            randZ *= sizeSetup[setup];
            randZ -= sizeSetup[setup] / 2f;

            float randBonusSize = Random.value * sizeSetup[setup] / 2f * 1.4f;

            if (Vector2.Distance(new Vector2(0, 0), new Vector2(randX, randZ)) < randBonusSize) {
                GameObject newTree = (GameObject)Instantiate(TreePrefab);
                newTree.transform.position = new Vector3(randX, 0.2f, randZ);
                i++;
            }

        }
            GetComponent<MyStatistics>().Reset();
    }
    

}
