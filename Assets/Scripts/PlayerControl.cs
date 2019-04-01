using UnityEngine;
using System.Collections;

public class PlayerControl : Character {

	Rigidbody2D myBody;
	public GameObject myWeapon;
	Vector2 transVector; 

	Camera mainCam;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		transVector = new Vector2 ();
		mainCam = FindObjectOfType<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		transVector.Set (Input.GetAxisRaw ("Horizontal") * this.getMovSpeed(), Input.GetAxisRaw ("Vertical") * this.getMovSpeed());

		CalcRotation ();

		CheckForAttack ();

	}

	void FixedUpdate()
	{
		myBody.velocity = transVector;
		cameraMovementHelper ();

	}


	/**
	 * Runs a condition to handle player attack 
	*/
	private void CheckForAttack()
	{
		if (Input.GetButtonDown ("Fire1")) {
			myWeapon.GetComponent<Weapon> ().Attack (calCursorDirection());
		}
	}

	/**
	 * Description: Rotates the current transform to face the location of the mouse pointer 
	*/
	private void CalcRotation()
	{
		float offset = (Mathf.PI/2) * Mathf.Rad2Deg;	
		Vector2 cursorDirection = calCursorDirection(); 
		cursorDirection.Normalize ();
		float angle = Mathf.Atan2(cursorDirection.y,cursorDirection.x) * Mathf.Rad2Deg - offset;
		transform.rotation = Quaternion.Euler(0f,0f, angle);
	}

	/**
	 * Description: Sets the position of the camera's transform to that of the current gameobject 
	*/
	private void cameraMovementHelper()
	{
		Transform camTransform = mainCam.GetComponent<Transform> ();
		camTransform.position = new Vector3 (this.transform.position.x, this.transform.position.y, 
			camTransform.position.z);
	}

	/**
	 * Description: Returns a Vector2 that shows the magnitude and direction of the moue pointer relative to the 
	 * 				position of the current gameobject.
	*/
	private Vector2 calCursorDirection()
	{
		return mainCam.ScreenToWorldPoint (Input.mousePosition) - transform.position;
	}


}
