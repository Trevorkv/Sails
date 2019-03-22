using UnityEngine;
using System.Collections;

public class PlayerControl : Character {

	Rigidbody2D myBody;
	Vector3 transVector;

	Camera mainCam;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		transVector = new Vector3 ();
		mainCam = FindObjectOfType<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		transVector.Set (Input.GetAxisRaw ("Horizontal") * this.getMovSpeed(), Input.GetAxisRaw ("Vertical") * this.getMovSpeed(), 0);

		CalcRotation ();


	}

	void FixedUpdate()
	{
		myBody.velocity = transVector;

		if (Input.GetAxis ("Fire1") > 0)
			Debug.Log ("Fire");

	}

	private void CalcRotation()
	{
		float offset = (Mathf.PI/2) * Mathf.Rad2Deg;	
		Vector2 direction =  mainCam.ScreenToWorldPoint (Input.mousePosition) - transform.position; 
		direction.Normalize ();
		float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - offset;
		transform.rotation = Quaternion.Euler(0f,0f, angle);
	}


}
