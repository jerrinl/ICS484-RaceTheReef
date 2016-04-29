using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveScript : MonoBehaviour {
	public float forwardMoveSpeed = 5;
	public float addForwardSpeed = 10;
	public float horizontalMoveSpeed = 20;
	public GameObject babyCoral;
	public GameObject healthyCoral;
    public GameObject sickCoral;
    public GameObject okCoral;
    public GameObject deadCoral;
	private float forwardMoveSpeedPrivate = 0;
	private bool landed = false;

	public static bool babyCoralGrown;
	public static bool healthyCoralGrown;
    public static bool sickCoralGrown;
    public static bool okCoralGrown;
    public static bool deadCoralGrown;

    private bool landingHealthy = false;
    private bool landingSick = false;
    private bool landingOk = false;
    private bool landingDead = false;

    private Vector3 initalPosition;
	// Use this for initialization
	void Start () {
		forwardMoveSpeedPrivate = forwardMoveSpeed;
        babyCoralGrown = false;
        healthyCoralGrown = false;
        sickCoralGrown = false;
        okCoralGrown = false;
        deadCoralGrown = false;
        initalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        
		if (!landed) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				forwardMoveSpeedPrivate = forwardMoveSpeed + addForwardSpeed;
			}else
			{
				forwardMoveSpeedPrivate = forwardMoveSpeed;
			}
			transform.Translate (horizontalMoveSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime,
		                    	0f,
		                    	 forwardMoveSpeedPrivate * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "landingSpot") 
		{
			if(Input.GetKey (KeyCode.X))
			{
				horizontalMoveSpeed = 0;
				forwardMoveSpeedPrivate = 0;
				gameObject.GetComponent<Rigidbody> ().useGravity = true;
				landed = true;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "wall") {
            SceneManager.LoadScene(0);
		}

		if (other.gameObject.tag == "reef")
		{
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			Invoke ("changeToBabyCoral", 3);
		}
		//coral grow animation
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "landingSpotHealthy") 
		{
            if (landingKeyPress())
            {
                landingHealthy = true;
            }
        }
        else if(other.tag == "landingSpotSick")
        {
            if (landingKeyPress())
            {
                landingSick = true;
            }
        }
        else if(other.tag == "landingSpotDead")
        {
            if (landingKeyPress())
            {
                landingDead = true;
            }
        }else if(other.tag == "landingSpotOk")
        {
            if (landingKeyPress())
            {
                landingOk = true;
            }
        }
	}

    bool landingKeyPress()
    {
        bool Pressed = false;
        if (Input.GetKey(KeyCode.X))
        {
            horizontalMoveSpeed = 0;
            forwardMoveSpeedPrivate = 0;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            landed = true;
            Pressed = true;
        }

        return Pressed;
    }

	void changeToBabyCoral()
	{
		if (!babyCoralGrown) {
			Instantiate (babyCoral, transform.position, Quaternion.identity);
            if(landingHealthy)
            {
                Invoke("changeToHealthyCoral", 3);
            }else if(landingDead)
            {
                Invoke("changeToDeadCoral", 3);
            }
            else if(landingOk)
            {
                Invoke("changeToOkCoral", 3);
            }
            else if(landingSick)
            {
                Invoke("changeToSickCoral", 3);
            }
        }
        babyCoralGrown = true;
	}

	void changeToHealthyCoral()
	{
		if (!healthyCoralGrown) {		
			Instantiate (healthyCoral, transform.position, Quaternion.identity);
            Invoke("reloadLevel",3);
        }
        healthyCoralGrown = true;
	}
    void changeToSickCoral()
    {
        if (!sickCoralGrown)
        {
            Instantiate(sickCoral, transform.position, Quaternion.identity);
            Invoke("reloadLevel", 3);
        }
        sickCoralGrown = true;
    }

    void changeToDeadCoral()
    {
        if (!healthyCoralGrown)
        {
            Instantiate(deadCoral, transform.position, Quaternion.identity);
            Invoke("reloadLevel", 3);
        }
        deadCoralGrown = true;
    }

    void changeToOkCoral()
    {
        if (!okCoralGrown)
        {
            Instantiate(okCoral, transform.position, Quaternion.identity);
            Invoke("reloadLevel", 3);
        }
       okCoralGrown = true;
    }
    //change to re-spawn the player without reload the level
    void reloadLevel()
    {
        spawnPlayer.spawned = true;
        Destroy(gameObject);
        //SceneManager.LoadScene(0);
    }
}
