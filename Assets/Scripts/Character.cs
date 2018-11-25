using UnityEngine;
using System.Collections;

//Branch-1
public class Character : MonoBehaviour {


	[Header("Test")]
	[SerializeField] private int health;
	[SerializeField] private string identity;
	[SerializeField] private int damage;
	[SerializeField] private float movSpeed;
	[SerializeField] private float turnSpeed;
	


	// Use this for initialization
	void Start () {
//		health = 10;
//		identity = "Player";
//		damage = 1;
//		movSpeed = 0.25f;
//		turnSpeed = 10;
	}

	// Update is called once per frame
	void Update () {

	}

	public int getHealth()
	{
		return health;
	}

	public string getName()
	{
		return identity;
	}

	public int getDamage()
	{
		return damage;
	}

	public float getMovSpeed()
	{
		return movSpeed;
	}

	public float getTurnSpeed()
	{
		return turnSpeed;
	}


	public void subHealth(int dmg = 1)
	{
		health -= dmg;
	}

	public void addHealth(int heal = 1)
	{
		health += heal;
	}

	public void setName(string input = "Player")
	{
		identity = input;
	}

	public void setMovSpeed(int speed = 1)
	{
		movSpeed = speed;
	}

}//EOF
