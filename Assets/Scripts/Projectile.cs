using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	Camera mainCam;
	Rigidbody2D myBody;
	Vector2 myPosition;
	Vector2 direction;
	int damageActual;
	Vector2 origin;


	// Use this for initialization
	void Awake () {
		mainCam = FindObjectOfType<Camera> ();
		myBody = transform.GetComponent<Rigidbody2D> ();
		origin = new Vector2(transform.localPosition.x, transform.localPosition.y);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		myPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y) - origin;

		if (myPosition.magnitude > direction.magnitude) {
			Destroy (this.gameObject);		
		}
	}
		
	/**
	 * Description: Assigns the RigidBody2D's velocity with direction multiplied by the speed to simulate projectile 
	 * 				motion.
	*/
	public void Shoot(Vector2 direction, float speed)
	{
		this.direction = direction;
		myBody.velocity = direction.normalized * speed;
	}

	public void OnCollisionEnter2D(Collision2D target)
	{
		//zzDebug.Log ("Projectile Hit");
		Destroy (this.gameObject);
	}

}
