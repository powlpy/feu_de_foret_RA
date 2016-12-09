using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyStatistics : MonoBehaviour {

    private int NbTrees, NbDamagedTrees, NbBurntTrees;
    private GameObject TextIntactTrees, TextDamagedTrees, TextBurntTrees, TextNbTrees;

    public void Awake() {
        NbTrees = 0;
        NbDamagedTrees = 0;
        NbDamagedTrees = 0;

        TextIntactTrees = GameObject.Find("TextStats1");
        TextDamagedTrees = GameObject.Find("TextStats2");
        TextBurntTrees = GameObject.Find("TextStats3");
        TextNbTrees = GameObject.Find("TextStats4");
    }

    public void Start() {
        Reset();
        UpdateStats();
    }

    public void Reset() {
        NbTrees = 0;
        NbDamagedTrees = 0;
        NbDamagedTrees = 0;
    }

    public void IncrementNbTree() {
        NbTrees++;
        UpdateNbTree();
    }

    void UpdateNbTree() {
        TextNbTrees.GetComponent<Text>().text = "Nb trees :         \t" + NbTrees.ToString();
    }

    void UpdateStats() {

        TextIntactTrees.GetComponent<Text>().text = "Intact trees :   \t" + GetPercentageIntactTrees();
        TextDamagedTrees.GetComponent<Text>().text = "Damaged trees : \t" + GetPercentageDamagedTrees();
        TextBurntTrees.GetComponent<Text>().text = "Burnt trees :     \t" + GetPercentageBurntTrees().ToString();

    }

    public void AddDamaged() {
        NbDamagedTrees++;
        UpdateStats();
    }

    public void AddBurnt() {
        NbDamagedTrees--;
        NbBurntTrees++;
        UpdateStats();
    }

    int GetNbIntactTrees() {
        return NbTrees - (NbDamagedTrees + NbBurntTrees);
    }

    string GetPercentageIntactTrees() {
        float percentage;
        if(NbTrees > 0)
            percentage = Mathf.Round((float)GetNbIntactTrees() / (float)NbTrees * 100f);
        else
            percentage = 100f;

        return percentage.ToString() + " %";
    }

    string GetPercentageDamagedTrees() {
        float percentage;
        if (NbTrees > 0)
            percentage = Mathf.Round(NbDamagedTrees / (float)NbTrees * 100f);
        else
            percentage = 0f;

        return percentage.ToString() + " %";
    }

    string GetPercentageBurntTrees() {
        float percentage;
        if (NbTrees > 0) {
            percentage = Mathf.Round(NbBurntTrees / (float)NbTrees * 100f);
        } else {
            percentage = 0f;
        }
        return percentage.ToString() + " %";
    }



}
