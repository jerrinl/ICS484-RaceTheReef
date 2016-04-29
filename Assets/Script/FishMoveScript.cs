using UnityEngine;
using System.Collections;

public class FishMoveScript : MonoBehaviour {
	public Transform TOP_RIGHT;
	public Transform DOWN_LEFT;
	public Transform fishCave;

	public int maxTimeChange = 5;
	public int minTimeChange = 10;
	private int timeChange;

	public int chanceBackToCave = 6;
	public float moveSpeed = 2f;
	public float moveSpeed_NoTarget = 0.05f;

	private GameObject target;
	private bool targetLocked = false;

	private float TOP_RIGHT_X;
	private float TOP_RIGHT_Z;
	private float DOWN_LEFT_X;
	private float DOWN_LEFT_Z;
	private float DEFAULT_Y;
	private Vector3 direction;

	private bool inCave = false;
	private bool invokeRandomDirection = false; //check if randomDirection method is already invoked
	// Use this for initialization
	void Start () {
		TOP_RIGHT_X = TOP_RIGHT.position.x;
		TOP_RIGHT_Z = TOP_RIGHT.position.z;
		DOWN_LEFT_X = DOWN_LEFT.position.x;
		DOWN_LEFT_Z = DOWN_LEFT.position.z;
		DEFAULT_Y = TOP_RIGHT.position.y;

		//set first direction and time change
		randomDirection ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!invokeRandomDirection) 
		{
			Invoke ("randomDirection", timeChange);
			invokeRandomDirection = true;
		}

		if (targetLocked) {
			//target in the hunting area, hunt target
			transform.LookAt(target.transform);
			transform.position = Vector3.Lerp (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
		} else {
			//randomly moving
			transform.LookAt(direction);
			transform.position = Vector3.Lerp(transform.position, direction, moveSpeed_NoTarget * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			target = other.gameObject;
			targetLocked = true;
		}
		
		Debug.Log(other.tag);
		//let the fish hit the TOP first then go down to the cave, to avoid collision with other objects
		if (other.tag == "fishCaveTOP") 
		{
			direction = fishCave.position;
		}

		if (other.tag == "fishCave") 
		{
			invokeRandomDirection = false;
			inCave = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			targetLocked = false;
		}
	}

	//set randomDirection
	void randomDirection()
	{
		invokeRandomDirection = false;
		timeChange = Random.Range (minTimeChange, maxTimeChange);
		if (Random.Range (0, chanceBackToCave) == 0 && !inCave) 
		{
			direction = new Vector3(fishCave.position.x, transform.position.y, fishCave.position.z);
			invokeRandomDirection = true;
		} 
		else
		{
			direction = new Vector3 (Random.Range (DOWN_LEFT_X, TOP_RIGHT_X),
			                         DEFAULT_Y,
			                         Random.Range (DOWN_LEFT_Z, TOP_RIGHT_Z));
			inCave = false;
		}
	}
}
