using UnityEngine;
using System.Collections;



/**
 * Description: A model used to generalize the functionalities and properties of a weapon in game
 * 
*/
public class Weapon : MonoBehaviour {
	public GameObject projectile;
	public float speed;
	public float range;
	public int ammoCount;
	public int damageScale;
	public int projectileCount;
	public int damageSpread;
	public int damageChoke;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Description: 
	*/
	public void Attack(Vector2 direction)
	{
		//calc attack damage to be delivered via the projectile prefab/script
		GameObject projectile = (GameObject)Instantiate (this.projectile, this.transform.position,
			                        this.transform.rotation);

		projectile.GetComponent<Projectile> ().Shoot (direction, speed);
	}


}
