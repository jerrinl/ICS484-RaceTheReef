using UnityEngine;
using System.Collections;

public class spawnPlayer : MonoBehaviour {
    public static bool spawned = false;
    public GameObject playerGameObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(spawned)
        {
            Instantiate(playerGameObject, transform.position, Quaternion.identity);
            spawned = false;
        }
	}
}
