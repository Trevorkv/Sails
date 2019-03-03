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
		float HScale = Input.GetAxis ("Mouse X");

		this.transform.Translate(new Vector2 (0, VScale*playerSpd));

		this.transform.Rotate(0,0,HScale * turnSpd);

		if (Input.GetAxis ("Fire1") > 0)
			Debug.Log ("Fire");

	}


}
