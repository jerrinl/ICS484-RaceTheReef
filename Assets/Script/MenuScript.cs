using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Button startText;
    
   

	// Use this for initialization
	void Start () {
        startText = startText.GetComponent<Button>();

	}
	
    public void StartPress()
    {
        startText.enabled = true;
    }

    public void StartLevel()
    {
        Application.LoadLevel(1);
    }

	// Update is called once per frame
	void Update () {
        	
	}
}   
