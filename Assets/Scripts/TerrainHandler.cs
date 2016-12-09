using UnityEngine;
using System.Collections;

public class TerrainHandler : MonoBehaviour {

    public GameObject TerrainHQ;
    public GameObject TerrainLQ;

    public void UpdateQuality() {
        bool hq = GlobalVariables.HighQuality;
        TerrainHQ.SetActive(hq);
        TerrainLQ.SetActive(!hq);
    }

}
