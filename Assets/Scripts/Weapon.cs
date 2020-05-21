using UnityEngine;
using System.Collections;



/**
 * Description: A model used to generalize the functionalities and properties of a weapon in game
 * 
*/
public class Weapon : MonoBehaviour {
	public Projectile projectile;
	public CrossHairController crossHairPrefab;
	public float speed;
	public float range;
	public int ammoCount;
	public int projectileCount;
	public int damageScale;
	public int fixedDmg;
	public int damageSpread;
	public int damageChoke;

	private CrossHairController crosshairScript;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


//	public void Attack()
//	{
//		int dmg = damageScale;
//		//calculate attack data : damage, location, number of damage points ("projectiles") etc...
//		//send relevant data : damage, damage points (locations relative to cursor) etc... as param to crossHairScript's
//		// sendAttack() method.
//
//		//use variables to calculate the actual damaged passed to the world and where the damage 
//		//hits in world space
//		//use collision component of the cursor to manupulate the extent of damage for damage spread/choke
//		//example of this would be a shotgun or an explosive ot even a melee weapon with swing.
//		//this.transform.localPosition = new Vector3(5,2,2);
//
//		//crosshairScript.sendAttack (dmg[], dmgLocations[]);
//		crosshairScript.sendAttack(dmg);
//	}


	/**
	 * Description: Calculates relevant data to be passed on to a newly created gameObject's projectile script to handle
	 * 				collision and pathing logic.
	*/
	public void Attack(Vector2 direction)
	{
		//calc attack damage to be delivered via the projectile prefab/script
		Projectile projectile = (Projectile)Instantiate (this.projectile, this.transform.position,
			                        this.transform.rotation);
		projectile.Init(fixedDmg, range);
		projectile.Shoot (direction, speed);
	}


}
