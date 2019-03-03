using UnityEngine;
using System.Collections;

/**
 * Description: Models an object that has the ability to shoot or cast projectiles
*/
public class Projector : MonoBehaviour {

	[SerializeField] private GameObject[] projectileTypes;


	// Use this for initialization
	void Start () {
		
	}

	public void Shoot(int type, Vector2 direction)
	{
		Instantiate (projectileTypes [type], this.transform);
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
