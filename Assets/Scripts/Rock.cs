using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = this.GetComponent<SpriteRenderer> ().sprite.bounds.size;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
