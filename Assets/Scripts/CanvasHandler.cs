using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasHandler : MonoBehaviour {

    public GameObject BackgroundCircle;
    public GameObject SliderGlobalSpeed;
    public GameObject SliderSizeCircle;
    public GameObject SliderTreesCreated;
    public GameObject Firefighters;
    public GameObject Setups;
    public GameObject WindControls;

    public GameObject HQButton;
    public GameObject LQButton;

    void Start () {
        Firefighters.SetActive(false);
        SliderGlobalSpeed.SetActive(false);


    }
	
	public void StartSimulation() {
        Firefighters.SetActive(true);
        SliderGlobalSpeed.SetActive(true);
        BackgroundCircle.SetActive(false);
        SliderSizeCircle.SetActive(false);
        SliderTreesCreated.SetActive(false);
        //WindControls.SetActive(false);
        Setups.SetActive(false);


    }

    public void GoHQ() {

        HQButton.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0.8f);
        LQButton.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0.4f);
        GlobalVariables.HighQuality = true;
        ChangeTreesQuality();
        //ChangeTerrainQuality();
    }

    public void GoLQ() {

        LQButton.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0.8f);
        HQButton.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0.4f);
        GlobalVariables.HighQuality = false;
        ChangeTreesQuality();
        //ChangeTerrainQuality();
    }

    void ChangeTreesQuality() {
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
            tree.GetComponent<Inflammable>().UpdateQuality();
    }

    void ChangeTerrainQuality() {
        GameObject.Find("Terrain").GetComponent<TerrainHandler>().UpdateQuality();    }

    public void Restart() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
