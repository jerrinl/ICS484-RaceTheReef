using UnityEngine;
using System.Collections;

public class underWater : MonoBehaviour {
    private Color normalColor;
    private Color underWaterColor;
	// Use this for initialization
	void Start () {

        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underWaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        RenderSettings.fog = true;
        RenderSettings.fogColor = underWaterColor;
        RenderSettings.fogDensity = 0.05f;
    }
}
