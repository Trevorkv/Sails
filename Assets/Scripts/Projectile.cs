using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private Rigidbody2D myBody;
	private Vector2 origin;
	private int dmg;
	private float range;

	// Use this for initialization
	void Awake () {
		myBody = transform.GetComponent<Rigidbody2D> ();
		origin = new Vector2(transform.localPosition.x, transform.localPosition.y);
	}

	void Update()
	{
		Vector2 myPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y) - origin;

		if (myPosition.magnitude > range) {
			Destroy (this.gameObject);		
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		
	}

	void OnTriggerEnter2D (Collider2D body)
	{
		if (body.GetComponent<Asset> () != null) {
			Destroy (body.gameObject);
			Destroy(this.gameObject);
		}
	}

	public void Init (int damage, float range)
	{
		dmg = damage;
		this.range = range;
	}

	/**
	 * Description: Assigns the RigidBody2D's velocity with direction multiplied by the speed to simulate projectile 
	 * 				motion.
	*/
	public void Shoot(Vector2 direction, float speed)
	{
		myBody.velocity = direction * speed;
	}




}
