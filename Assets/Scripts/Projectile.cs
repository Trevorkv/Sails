using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	Rigidbody2D myBody;
	Vector2 myPosition;
	Vector2 direction;
	int damageActual;


	// Use this for initialization
	void Awake () {
		myBody = transform.GetComponent<Rigidbody2D> ();
		myPosition = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		myPosition = new Vector2(transform.position.x, transform.position.y);

		if (myPosition.magnitude > direction.magnitude) {
			Debug.Log ("Projectile Death" + transform.position);
			Destroy (this.gameObject);		
		}
	}
		

	public void Shoot(Vector2 direction, float speed)
	{
		this.direction = direction;
		Debug.Log ("Direction" + direction);
		myBody.velocity = direction.normalized * speed;
	}

	public void OnCollisionEnter2D(Collision2D target)
	{
		Destroy (this.gameObject);
		Debug.Log ("Projectile Hit");
	}

}
