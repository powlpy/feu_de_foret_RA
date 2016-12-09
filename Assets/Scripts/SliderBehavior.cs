using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class SliderBehavior : MonoBehaviour {

    public Slider SpeedSlider;
    public Slider SizeCircleSlider;
    public Slider NbTreesCreatedSlider;
    public Slider GlobalHeatSlider;

    // Use this for initialization
    void Start() {
        SpeedSlider.onValueChanged.AddListener(delegate { SpeedValueChanged(); });
        SizeCircleSlider.onValueChanged.AddListener(delegate { SizeCircleValueChanged(); });
        NbTreesCreatedSlider.onValueChanged.AddListener(delegate { NbTreesCreatedValueChanged(); });
        GlobalHeatSlider.onValueChanged.AddListener(delegate { GlobalHeatSliderValueChanged(); });

    }

    void GlobalHeatSliderValueChanged() {
        GlobalVariables.Heat = GlobalHeatSlider.value;
    }

    void SpeedValueChanged() {

        GlobalVariables.Speed = SpeedSlider.value;

        if (GlobalVariables.Speed == 0f) Time.timeScale = 0;
        else Time.timeScale = 1;
        /*
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject Tree in trees) {
            ParticleSystem myParticleSystem = Tree.GetComponent<ParticleSystem>();
            if(myParticleSystem != null) {
                Tree.GetComponent<ParticleSystem>().startSpeed = 1 + SpeedSlider.value;
                Tree.GetComponent<ParticleSystem>().startLifetime = 1.3f / SpeedSlider.value;
            }
        }*/
    }

    void SizeCircleValueChanged() {
        CylinderBehavior cylinderBehavior = GameObject.Find("Cylinder").GetComponent<CylinderBehavior>();
        cylinderBehavior.Radius = SizeCircleSlider.value;
        cylinderBehavior.Resize();

    }

    void NbTreesCreatedValueChanged() {
        CylinderBehavior cylinderBehavior = GameObject.Find("Cylinder").GetComponent<CylinderBehavior>();
        int newValue;
        if(NbTreesCreatedSlider.value == 0) {
            newValue = 1;
        } else if(NbTreesCreatedSlider.value == 1) {
            newValue = 10;
        } else {
            newValue = 50;
        }
        cylinderBehavior.NbTreesCreated = newValue;
    }

}
