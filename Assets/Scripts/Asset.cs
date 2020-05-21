using UnityEngine;
using System.Collections;

public class Asset : MonoBehaviour {

	Vector3 dimensions;
	float lengthX;
	float lengthY;

	[Header ("Asset Members")]
	public bool isInteractible;

	// Use this for initialization
	void Awake () {
		dimensions = this.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		lengthX = dimensions.x;
		lengthY = dimensions.y;
	}

	// Update is called once per frame
	void Update () {

	}

	public float LengthX
	{
		get{ return lengthX; }
	}

	public float LengthY
	{
		get { return lengthY; }
	}
		
	public void runInteraction()
	{
		if(isInteractible)
			Debug.Log ("Asset " + this.name + " Inreracted");
	}
}
