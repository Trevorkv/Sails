using UnityEngine;
using System.Collections;

public class PlayerControl : Character {

	Rigidbody2D myBody;
	Vector3 transVector;
	float angle;

	Camera mainCam;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		transVector = new Vector3 ();
		mainCam = FindObjectOfType<Camera> ();
		angle = 0;
	}

	// Update is called once per frame
	void Update () {
		transVector.Set (Input.GetAxisRaw ("Horizontal") * this.getMovSpeed(), Input.GetAxisRaw ("Vertical") * this.getMovSpeed(), 0);
		Vector2 direction = mainCam.ScreenToWorldPoint (Input.mousePosition) - transform.position; 
		angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

		Quaternion rot = Quaternion.AngleAxis (angle, Vector3.back);
		transform.rotation = Quaternion.Slerp(transform.rotation, rot,this.getTurnSpeed() * Time.deltaTime);
	}

	void FixedUpdate()
	{
		myBody.velocity = transVector;

		if (Input.GetAxis ("Fire1") > 0)
			Debug.Log ("Fire");

	}


}
