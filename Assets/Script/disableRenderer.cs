using UnityEngine;
using System.Collections;

public class disableRenderer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (MoveScript.babyCoralGrown && gameObject.tag == "planula") {
			GetComponent<Renderer> ().enabled = false;
		}

		if ((MoveScript.healthyCoralGrown || 
            MoveScript.sickCoralGrown || 
            MoveScript.okCoralGrown || 
            MoveScript.deadCoralGrown) 
            && gameObject.tag == "babyCoral")
        {
			GetComponent<Renderer> ().enabled = false;
		}

    }
}
