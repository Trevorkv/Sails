using UnityEngine;
using System.Collections.Generic;

public class GeoGrid : MonoBehaviour {
	//the load distance relative to the center of the grid. a grid's dimension is in terms of geoCells
	[SerializeField] private int gridRadius;
	//A Prefab of a geocell to be initialized and cloned to make the cells of a grid 
	[SerializeField] private GameObject geoCellPrefab;

	//The "origin point/cell" of the grid. Holds the center cell of the grid
	private GameObject geoCellOrigin;
	//The width of an individual cell
	private float geoCellWidth;
	//The length of an individual cell
	private float geoCellHeight;
	//The x-coordinante of the center cell
	private int centerX;
	//The y-coordinate of the center cell
	private int centerY;

	//The 2d array of all the cells in the grid
	private GameObject[,] geoCells;
	private int geoCellSize = 0;


	// Use this for initialization
	void Start () {
		geoCellSize = (gridRadius * 2) + 1;
		geoCells = new GameObject[geoCellSize, geoCellSize];
		centerX = gridRadius;
		centerY = gridRadius;
		geoCellWidth = geoCellPrefab.GetComponent<GeoCell> ().DimX;
		geoCellHeight = geoCellPrefab.GetComponent<GeoCell> ().DimY;
//
//		geoCellOrigin = (GameObject)Instantiate (geoCellPrefab,new Vector2(geoCellWidth, geoCellHeight), geoCellPrefab.transform.localRotation);
//		geoCellOrigin.transform.SetParent (this.transform);
//		geoCells [centerX, centerY] = geoCellOrigin;

		InitGeoCells ();
		SetCenter(geoCells[centerX, centerY]);
	}

	/**
	 * Description: Initializes the geoCells found in the geoGrid
	 * Date: 11-23-2018
	*/
	private void InitGeoCells()
	{Debug.Log("Initializing geocells 2018");
		Debug.Log (geoCells.GetUpperBound(1));
		for (int i = 0; i < geoCellSize; i++) {
			for (int j = 0; j < geoCellSize; j++) {
				//if(i != centerY && j != centerX)
					InitGeoCell (j * geoCellWidth, i * geoCellHeight, i, j);
			}
		}

		//PrintGrid ();
	}

	/**
	 * Description: Initializes a geoCell at position (posX, posY) in the 2d Cartesian plane and stores the geoCell 
	 * 			 	as a GameObject in geoCells[row, col]
	 * Date: 11-23-2018 
	*/
	private GameObject InitGeoCell(float posX, float posY, int rowIndex, int colIndex)
	{
		Debug.Log ("In cell ");
		geoCells[rowIndex,colIndex] = (GameObject)Instantiate (geoCellPrefab, new Vector3 (posX, posY, 0f),
			geoCellPrefab.transform.localRotation);

		geoCells [rowIndex, colIndex].transform.SetParent (this.transform);
		
		return geoCells [rowIndex, colIndex];
	}

	/**
	 * Description: Assigns geoCell to be the center cell of the geoGrid
	 * Date: 11-23-2018
	*/
	public void SetCenter(GameObject geoCell)
	{
		geoCellOrigin = geoCell;
		alignGrid ();
	}

	/**
	 * Description: Re-aligns the geoGrid reative to the new center cell.
	 * Date: 11-23-2018
	*/
	private bool alignGrid()
	{
		float hShift = 0f;
		float vShift = 0f;
		bool ret = false;

		if(geoCellOrigin != geoCells[centerX, centerY])
		{
			hShift = geoCells [centerX, centerY].transform.position.x - geoCellOrigin.transform.position.x;
			vShift = geoCells [centerX, centerY].transform.position.y - geoCellOrigin.transform.position.y;

			alignGridHelper (hShift,vShift);

			ret = true;
		}

		return ret;
	}

	/**
	 * Description: align the grid's center relative to the player's position
	 * 
	*/
	private void alignGridHelper(float hShift = 0, float vShift = 0)
	{
		if (hShift > 0) {
			DeleteRightCol ();
			ShiftGridRight ();
			SpawnLeft ();
		} else if (hShift < 0) {
			DeleteLeftCol ();
			ShiftGridLeft ();
			SpawnRight ();
		}
		if (vShift < 0) {
			DeleteBotRow ();
			ShiftGridDown ();
			SpawnUp();
		} else if (vShift > 0) {
			DeleteTopRow ();
			ShiftGridUp ();
			SpawnDown ();
		}
	}

	/**
	 * Description: Destroys the game objects stored in the left most column of the geoGrid
	 * 
	*/
	private void DeleteLeftCol()
	{		
		for (int i = 0; i < geoCellSize; i++) {
			Destroy (geoCells[i,0]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the right most column of the geoGrid
	 * 
	*/
	private void DeleteRightCol()
	{
		for (int i = 0; i < geoCellSize; i++) {
			Destroy (geoCells[i,geoCellSize-1]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the bottom most row of the geoGrid
	 * 
	*/
	private void DeleteBotRow()
	{
		for (int i = 0; i < geoCellSize; i++) {
			Destroy (geoCells[0,i]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the top most row of the geoGrid
	 * 
	*/
	private void DeleteTopRow()
	{
		for (int i = 0; i < geoCellSize; i++) {
			Destroy (geoCells[geoCellSize-1,i]);
		}
	}

	/**
	 * Description: shifts the geoCells 'Left' in the 2d array representation of the geoGrid
	 * 
	*/
	private void ShiftGridLeft()
	{
		for (int i = 0; i < geoCellSize; i++) {
			for(int j = 0; j < geoCellSize - 1; j++)
			{
				geoCells [i, j] = geoCells [i, j + 1];
			}
		}
	}

	/**
	 * Descripton: shifts the geoCells 'Right' in the 2d array representation of the geoGrid 
	 * 
	*/
	private void ShiftGridRight()
	{
		for(int i = 0; i < geoCellSize; i++)
		{
			for(int j = geoCellSize - 1; j > 0; j--)
			{
				geoCells [i, j] = geoCells [i, j - 1];
			}
		}
	}

	/**
	 * Description: shifts the geoCells 'Up' in the 2d array representation of the geoGrid
	*/
	private void ShiftGridUp()
	{
		for (int i = geoCellSize - 1; i > 0; i--) {
			for (int j = 0; j < geoCellSize; j++)
			{
				geoCells[i,j] = geoCells[i - 1, j];
			}
		}
	}

	/**
	 * Description: shifts the geoCells 'Down' in the 2d array representation of the geoGrid
	*/
	private void ShiftGridDown()
	{
		for (int i = 0; i < geoCellSize - 1; i++) {
			for(int j = 0; j < geoCellSize; j++)
			{
				geoCells[i,j] = geoCells[i + 1, j];
			}
		}
	}

	/**
	 * Description: Spawns new Instantiated geoCells on the right most column of the geoGrid. 
	 * 
	*/
	private void SpawnRight()
	{
		Vector3 vec;

		for (int i = 0; i < geoCellSize; i++) {
			vec = new Vector3 (geoCells[i,geoCellSize - 1].transform.localPosition.x + geoCellHeight,
				geoCells[i, geoCellSize - 1].transform.localPosition.y);

			geoCells[i, geoCellSize - 1] = (GameObject)Instantiate (geoCellPrefab, vec,
				geoCellPrefab.transform.localRotation);

			geoCells [i, geoCellSize - 1].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: Spawns a new instantiated geoCells on the left most column of the geoGrid
	*/
	private void SpawnLeft()
	{
		Vector3 vec;

		for (int i = 0; i < geoCellSize; i++) {
			vec = new Vector3 (geoCells[i, 0].transform.localPosition.x - geoCellHeight,
				geoCells[i, 0].transform.localPosition.y);

			geoCells[i, 0] = (GameObject)Instantiate (geoCellPrefab, vec,
				geoCellPrefab.transform.localRotation);

			geoCells [i, 0].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: Spawns a new instantiated geoCells on the top most row of the geoGrid
	*/
	private void SpawnUp()
	{
		Vector3 vec;

		for (int i = 0; i < geoCellSize; i++) {
			vec = new Vector3 (geoCells[geoCellSize - 1, i].transform.localPosition.x,
				geoCells[geoCellSize - 1, i].transform.localPosition.y + geoCellWidth);

			geoCells[geoCellSize - 1, i] = (GameObject)Instantiate (geoCellPrefab, vec,
				geoCellPrefab.transform.localRotation);

			geoCells [geoCellSize - 1, i].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: SPawns a new intsantiated geoCells on the botom most row of the geoGrid 
	 * 
	*/
	private void SpawnDown()
	{
		Vector3 vec;

		for (int i = 0; i < geoCellSize; i++) {
			vec = new Vector3 (geoCells[0, i].transform.localPosition.x ,
				geoCells[0, i].transform.localPosition.y - geoCellWidth);

			geoCells[0, i] = (GameObject)Instantiate (geoCellPrefab, vec,
				geoCellPrefab.transform.localRotation);

			geoCells [0, i].transform.SetParent (this.transform);
		}
	}

	private void PrintGrid()
	{
		for (int i = 0; i < geoCellSize; i++) {
			for (int j = 0; j < geoCellSize; j++) {
				Debug.Log ("x: " + i + " y: " + j + geoCells[i,j].transform.position);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
		
}
