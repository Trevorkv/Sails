using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeoCell : MonoBehaviour {

	[Header("Border Settings")]
	[SerializeField] private float dimY;
	[SerializeField] private float dimX;
	[Header("Itemized Children")]
	[SerializeField] private int maxSeed;
	[SerializeField] private GameObject[] rocks;

	private List<GameObject> rockList = new List<GameObject>();
	static System.Random rnd = new System.Random ();


	// Use this for initialization
	void Start () {
		Debug.Log ("Initializing");
		SpawnRocks (maxSeed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D body)
	{
		if (body.gameObject.name == "PlayerPrefab") {
			Debug.Log ("Inside Cell");
			Debug.Log (body.gameObject.name);
			this.GetComponentInParent<GeoGrid> ().SetCenter (this.gameObject);
		}
	}


	public int MaxSeed
	{
		get{return maxSeed;}
		set{ maxSeed = value; }
	}

	public float DimY
	{
		get{return dimY; }
		set{ dimY = value; }
	}

	public float DimX
	{
		get{return dimX; }
		set{ dimX = value; }
	}

	/**
	 * Desription: Spawns between 0 to maxSeed number of rocks within the bounds of the current gameObject
	 * Param: maxSeed is an int used to determine the maximum number of prefabs to be instantiated
	 * 
	*/
	private void SpawnRocks(int maxSeed)
	{
		
		int rockId = 0;
		int numOfRocks = rnd.Next (1, maxSeed);

		for(int loop = 0; loop < numOfRocks; loop++)
		{
			rockId = rnd.Next(0, rocks.Length);
			rockList.Add (Instantiate (rocks [rockId]));
			rockList[loop].transform.SetParent (gameObject.transform);
			RanDisplaceRock (loop, rnd);
		}
	}


	/**
	 * Description: Randomly sets the position of the rock prefabs within the bounds of the current gameObject
	 * Param: id is an int used to determine the index of the rock prefab to displace
	 * 		  rnd is a System.Random object used as a rando number generator
	 *
	 */
	private void RanDisplaceRock(int id, System.Random rnd)
	{

		float posX = System.Convert.ToSingle(rnd.NextDouble() * DimX);
		float posY = System.Convert.ToSingle(rnd.NextDouble() * DimY);

		Debug.Log ("PosX: " + posX + " posY: " + posY);

		rockList [id].transform.localPosition = new Vector3(posX, posY);
	}

}
