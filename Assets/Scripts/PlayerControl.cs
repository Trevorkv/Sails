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

	private void CheckForAttack()
	{
		if (Input.GetButtonDown ("Fire1")) {
			myWeapon.GetComponent<Weapon> ().Attack (calCursorDirection());
		}
	}

	private void CalcRotation()
	{
		float offset = (Mathf.PI/2) * Mathf.Rad2Deg;	
		Vector2 cursorDirection = calCursorDirection(); 
		cursorDirection.Normalize ();
		float angle = Mathf.Atan2(cursorDirection.y,cursorDirection.x) * Mathf.Rad2Deg - offset;
		transform.rotation = Quaternion.Euler(0f,0f, angle);
	}

	private void cameraMovementHelper()
	{
		Transform camTransform = mainCam.GetComponent<Transform> ();
		camTransform.position = new Vector3 (this.transform.position.x, this.transform.position.y, 
			camTransform.position.z);
	}

	private Vector2 calCursorDirection()
	{
		Vector2 ret = mainCam.ScreenToWorldPoint (Input.mousePosition) - transform.position;
//		Vector2 offset = new Vector2(transform.lossyScale.x / 2, transform.lossyScale.y / 2);
//		return ret.x > 0 ? ret + offset : ret - offset; 
		return ret;
	}


}
