using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindHandler : MonoBehaviour {

    public void OnClick() {

        Vector2 myPosition = GUIUtility.ScreenToGUIPoint(Input.GetTouch(0).position) - RectTransformUtility.WorldToScreenPoint(null, transform.position);// Input.GetTouch(0).position OR Input.mousePosition
        GameObject temp = transform.Find("Image").Find("Mark").gameObject;
        Image myMark = temp.GetComponentInChildren<Image>();

        float myDist = Vector2.Distance(myPosition, Vector2.zero);
        myDist = GetNewDistance(myDist);
        float ang = Vector2.Angle(new Vector2(0, 1), myPosition);
        if (Vector3.Cross(new Vector2(0, 1), myPosition).z > 0f)
            ang = -ang;

        myMark.rectTransform.anchoredPosition = GetMarkPosition(myDist, ang);
        GlobalVariables.SetWindForce(myDist / 15f);
        GlobalVariables.SetWindAngle(ang);
        StartCoroutine(ComputeNewNeighbors());
    }

    float GetNewDistance(float oldDistance) {
        if (oldDistance > 60.5f)
            return 73;
        else if (oldDistance > 35.5f)
            return 48;
        else if (oldDistance > 11.5f)
            return 23;
        else
            return 0;
    }

    Vector3 GetMarkPosition(float radius, float ang) {
        Vector2 pos;
        pos.x = radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    IEnumerator ComputeNewNeighbors() {
        yield return null;
        foreach (GameObject Tree in GameObject.FindGameObjectsWithTag("Tree"))
            Tree.GetComponent<Inflammable>().ComputeNeighbors();

    }

}
