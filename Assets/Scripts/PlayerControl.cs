using UnityEngine;
using System.Collections;

public class PlayerControl : Character {

	char forward;
	char backwards;
	char strafeLeft;
	char strafeRight;

	private float mouseShift = 0;


	// Use this for initialization
	void Start () {
		forward = 'w';
		backwards = 's';
		strafeLeft = 'a';
		strafeRight = 'd';
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		float vScale = Input.GetAxis ("Vertical");
		float hScale = Input.GetAxis ("Horizontal");
		float mousePosX = Input.mousePosition.x - (Screen.width / 2);
		float mousePosY = Input.mousePosition.y - (Screen.height / 2);
		float currentMouseX = 0;
		float eulerRotation = Mathf.Atan (mousePosX / mousePosY);

		if (currentMouseX > mousePosX) {
			eulerRotation = Mathf.Atan (mousePosX / mousePosY) * -1;
			currentMouseX += eulerRotation;
		} else if (currentMouseX < mousePosX) {
			eulerRotation = Mathf.Atan (mousePosX / mousePosY) * -1;
			currentMouseX += eulerRotation;
		}
		else
			eulerRotation = 0;
		
		//float pyth = Mathf.Sqrt (Mathf.Pow (Input.GetAxis ("Mouse X"), 2) + Mathf.Pow (Input.GetAxis ("Mouse Y"), 2));
		this.transform.Translate(new Vector2 (hScale * getMovSpeed(), vScale * getMovSpeed()));
//		Debug.Log (mousePos);
		this.transform.Rotate(0,0, eulerRotation);
//		Debug.Log(	GetComponent<UnitConverter>().toUnits(mousePos));
		if (Input.GetAxis ("Fire1") > 0)
			Debug.Log ("Fire");

	}


}
