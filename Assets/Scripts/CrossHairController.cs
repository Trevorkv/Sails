using UnityEngine;
using System.Collections;

public class CrossHairController : MonoBehaviour {

	Collider2D target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		setCursorLocation ();
		Debug.Log ("Targeting : " + target);
	}

	/**
	 * Description: Upon overlapping with body, assign the target member with body's value
	*/
	void OnTriggerEnter2D(Collider2D body)
	{
		target = body;
	}

	/**
	 * Description: Upon leaving the body collider's constraints, reset the target member to null
	*/
	void OnTriggerExit2D(Collider2D body)
	{
		target = null;
	}
		
	public void sendAttack(int dmg)
	{
		//check if target collider is interactible
		//If not or target = null, do nothing but perform weapon attack animation/ ammo dump etc
		//if target is interactible, run overloaded apply attack.
		//target.gameObject.GetComponent
		if (target != null /* || target.GetComponent<Asset>().isInteractible*/) {
			//target.applyDamagee();
			Debug.Log("Applied Damage at : " + this.transform.localPosition);
		}
	}

	public void sendAttack(int [] dmg, Vector2 [] dmgLocations)
	{
//		for(int i = 0; i < dmg.Length; i++)
//		{
//
//		}
		Debug.Log("Applied Damage at : " + this.transform.localPosition);
	}

	/**
	 * Description: returns true if the current instnace's target member is not null, else false.
	 * 				The target member is the Collider2D object in world space that the current collider2D is overlapping
	*/
	public bool hasTarget()
	{
		return target != null ? true : false;
	}

	/**
	 * Description: Sets the cursor sprite location to be equvalent to the position of the mouse position relative to
	 * 				the world point.
	*/
	private void setCursorLocation()
	{
		Vector3 cursorPos2 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1);
		this.transform.localPosition = FindObjectOfType<Camera> ().ScreenToWorldPoint(cursorPos2);
	}
}
