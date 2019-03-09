using UnityEngine;
using System.Collections;

public class Asset : MonoBehaviour {

	public Vector3 offset;
	public float lengthX;
	public float lengthY;

	// Use this for initialization
	void Awake () {
		offset = this.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		lengthX = offset.x;
		lengthY = offset.y;
		Debug.Log ("Cap" + lengthX + " " + lengthY);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
