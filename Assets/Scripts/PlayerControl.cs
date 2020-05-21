using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	[Header ("Camera Options")]
	public Camera mainCam;
	public bool magPosition;
	public bool magRotation;

	private Rigidbody2D myBody; 
	private Character charScript;
	private Vector3 playerMovement;
	private Vector3 mouseMovement;
	private Vector3 playerRotation;



	void Awake()
	{
		myBody = this.GetComponent<Rigidbody2D> ();
		charScript = this.GetComponent<Character> ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		playerRotation = GetPlayerRotation ();
		playerMovement = GetPlayerMovement ();
		mouseMovement = GetMouseMovement ();

		transform.eulerAngles = playerRotation;
		TranslateCursor (playerMovement * Time.deltaTime);
		TranslateCursor (mouseMovement);
		CheckForAttack();

		if (magPosition) 
			TranslateCam (playerMovement * Time.deltaTime);

		if(magRotation)
			mainCam.transform.eulerAngles = playerRotation;

		if(Input.GetKeyDown(KeyCode.R))
			ResetCamera();
	}


	void FixedUpdate()
	{
		myBody.velocity = playerMovement;
	}

	/**
	 * Returns a Vector3 representing the mouse movement in world space. Note : The z-axis is always zero.
	*/
	private Vector3 GetMouseMovement ()
	{
		return new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
	}

	/**
	 * Returns a Vector3 representing the character's velocity in world space.
	*/
	private Vector3 GetPlayerMovement ()
	{
		float xMove = Input.GetAxisRaw ("Horizontal");
		float yMove = Input.GetAxisRaw ("Vertical");

		return new Vector2(xMove, yMove) * charScript.MovSpeed;
	}

	/**
	 * Translates the Character's cursor transform in world space.
	*/
	private void TranslateCursor (Vector3 transVector)
	{
		charScript.Cursor.transform.Translate(transVector);
	}

	/**
	 * Translates the main camera transform in world space.
	*/
	private void TranslateCam (Vector3 transVector)
	{
		mainCam.transform.Translate(transVector);
	}

	/**
	 * Runs a condition to handle player attack 
	*/
	private void CheckForAttack()
	{
		if (Input.GetButtonDown ("Fire1")) {
			charScript.Wpn.Attack(transform.up);
		}
	}

	/**
	 * Returns a Vector3 of euler angles that measures the degrees the character object needs to rotate
	 * for its y-axis to face it's cursor object.
	*/
	private Vector3 GetPlayerRotation()
	{
		float offset = (Mathf.PI/2) * Mathf.Rad2Deg;	
		Vector2 cursorDirection = charScript.Cursor.transform.position - this.transform.localPosition;
		cursorDirection.Normalize();
		float angle = Mathf.Atan2(cursorDirection.y,cursorDirection.x) * Mathf.Rad2Deg - offset;
		Vector3 rotationVector = new Vector3(0f, 0f, angle);

		return rotationVector;
	}

	/**
	 * Centers the camera's position over the character object and sets its cursor position back to the zero vector
	*/
	private void ResetCamera()
	{
		mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, 
			mainCam.transform.position.z);
		mainCam.transform.eulerAngles = Vector3.zero;

		charScript.Cursor.transform.position = transform.position;
	}

}//EndOfClass
