using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


/**
* Desription: A model of a single cell item of a geoGrid plane. 
*/
public class GeoCell : MonoBehaviour {

	[Header("Dimensions")]
	//The Length measured along the Y Axis
	[SerializeField] private float dimY;
	//The Length measured along the X Axis
	[SerializeField] private float dimX;

	[Header("Itemized Children")]
	//The maximum number of assets to be randomly generated
	[SerializeField] private int maxSeed;
	//The Array of assets that will be added to the pool of randomly instantiated (Not all of them will be instantiated)
	[SerializeField] private GameObject[] assets;
	//The List of assets that will be Instantiated after the creation of the current instance
	[SerializeField] private List<GameObject> assetList = new List<GameObject>();

	//The GeoGrid that the current instance is a child of.
	private GeoGrid geoGridParent;
	//The Randomization Tool
	static System.Random rnd = new System.Random ();

	// Gets Called after the creation of the current instance
	void Start () {
		geoGridParent = this.GetComponentInParent<GeoGrid> ();
		SpawnAssets (maxSeed);
	}
	 
	// Update is called once per frame
	void Update () {
	
	}

	//Runs when body enters the dimension of the current instance
	void OnTriggerEnter2D(Collider2D body)
	{
		if (body.gameObject.name == "PlayerPrefab") {
			geoGridParent.SetCenter (this.gameObject);
		}
	}

	/**
	 * Description: A Property to get and set maxSeed
	 * Returns: maxSeed - An integer number of assets to be randomly generated
	*/
	public int MaxSeed
	{
		get{return maxSeed;}
		set{ maxSeed = value; }
	}

	/**
	 * Description: A Property to get dimY
	 * Returns: DimY - A float measuring the Y Dimension of the current instance
	*/
	public float DimY
	{
		get{ return dimY; }
	}

	/**
	 * Description: A Property to get dimX
	 * Returns: DimX - A float measuring the X Dimension of the current instance
	*/
	public float DimX
	{
		get{ return dimX; }
	}

	/**
	 * Desription: Spawns between 0 to maxSeed number of rocks within the bounds of the current gameObject
	 * Param: maxSeed - An int used to determine the maximum number of prefabs to be instantiated
	 * 
	*/
	private void SpawnAssets(int maxSeed)
	{
		int assetID = 0;
		int numOfAssets = rnd.Next (1, maxSeed);

		for(int loop = 0; loop < numOfAssets; loop++)
		{
			assetID = rnd.Next(0, assets.Length);
			assetList.Add (Instantiate (assets [assetID]));
			assetList[loop].transform.SetParent (gameObject.transform);
			RandDisplaceAssets (loop, rnd);
		}
	}


	/**
	 * Description: Randomly sets the position of the rock prefabs within the bounds of the current gameObject
	 * Param: id - An int used to determine the index of the rock prefab to displace
	 * 		  rnd - A System.Random object used as a rando number generator
	 *
	 */
	private void RandDisplaceAssets(int id, System.Random rnd)
	{
		double rndX = rnd.NextDouble ();
		double rndY = rnd.NextDouble ();

		rndX = rndX < 0.5 ? rndX * -1 : rndX - 0.5;
		rndY = rndY < 0.5 ? rndY * -1 : rndY - 0.5;

		float posX = System.Convert.ToSingle(rndX * (DimX - assetList[id].GetComponent<Asset>().LengthX));
		float posY = System.Convert.ToSingle(rndY * (DimY - assetList[id].GetComponent<Asset>().LengthY));

		assetList [id].transform.localPosition = new Vector3(posX, posY);
	}

}//End Of Class
