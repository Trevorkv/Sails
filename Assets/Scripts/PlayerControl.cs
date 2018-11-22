using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	char forward;
	char backwards;
	char strafeLeft;
	char strafeRight;
	float playerSpd;
	float turnSpd;


	// Use this for initialization
	void Start () {
		forward = 'w';
		backwards = 's';
		strafeLeft = 'a';
		strafeRight = 'd';
		playerSpd = this.GetComponent<Character> ().getMovSpeed();
		turnSpd = this.GetComponent<Character> ().getTurnSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		float VScale = Input.GetAxis ("Vertical");
		float bScale = Input.GetAxis ("Vertical_Negative");
		float HScale = Input.GetAxis ("Horizontal");
		float RScale = Input.GetAxis ("Horizontal_Negative");
		turnSpd = Screen.width / 2 < Input.mousePosition.x ? -turnSpd: 
			turnSpd;

		this.transform.Translate(new Vector2 (0, VScale*playerSpd));
		if (Input.GetAxis ("Jump") > 0)
			this.GetComponent<Rigidbody2D> ().AddForce (Camera.main.WorldToViewportPoint (this.transform.position));
		this.transform.Rotate(0,0,HScale);

	}


}
