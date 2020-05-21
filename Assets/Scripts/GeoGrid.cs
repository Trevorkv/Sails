using UnityEngine;
using System.Collections.Generic;
using System.Threading;

/**
 * Description: A model of a square grid made up of GeoCells as its rows and columns 
*/
public class GeoGrid : MonoBehaviour {
	//the load distance relative to the center of the grid. a grid's dimension is in terms of geoCells
	[SerializeField] private int gridRadius;
	//A Prefab of a geocell to be initialized and cloned to make the cells of a grid 
	[SerializeField] private GameObject geoCellPrefab;

	//The "origin point/cell" of the grid. Holds the center cell of the grid
	private GameObject geoCellCenter;
	//The width of an individual cell
	private float geoCellWidth;
	//The length of an individual cell
	private float geoCellHeight;
	//A integer to represent the center element row and column number of the grid. One num is enough because the grid is 
	//Square
	private int center;
	//The 2d array of all the cells in the grid
	private GameObject[,] geoCells;
	//The length of one side of the grid measured in GeoCells
	private int geoGridSideLength;


	// Use this for initialization
	void Start () {
		geoGridSideLength = (gridRadius * 2) + 1;
		geoCells = new GameObject[geoGridSideLength, geoGridSideLength];
		center = gridRadius;
		geoCellWidth = geoCellPrefab.GetComponent<GeoCell> ().DimX;
		geoCellHeight = geoCellPrefab.GetComponent<GeoCell> ().DimY;
		InitGeoCells ();
		SetCenter(geoCells[center, center]);
	}

	/**
	 * Description: Initializes the geoCells found in the geoGrid
	 * Date: 11-23-2018
	*/
	private void InitGeoCells()
	{
		int jLocal = geoGridSideLength / 2 * -1;
		int iLocal = geoGridSideLength / 2 * -1;
		for (int i = 0; i < geoGridSideLength; i++) {
			for (int j = 0; j < geoGridSideLength; j++) {
				InitGeoCell (jLocal * geoCellWidth, iLocal * geoCellHeight, i, j);
				jLocal++;
			}
			jLocal = geoGridSideLength / 2 * -1;
			iLocal++;
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
		geoCellCenter = geoCell;
	}

	/**
	 * Description: Re-aligns the geoGrid rel ative to the new center cell.
	 * Date: 11-23-2018
	*/
	private void alignGrid()
	{
		float hShift = 0f;
		float vShift = 0f;

		if(geoCellCenter != geoCells[center, center])
		{
			hShift = geoCells [center, center].transform.position.x - geoCellCenter.transform.position.x;
			vShift = geoCells [center, center].transform.position.y - geoCellCenter.transform.position.y;

			alignGridHelper (hShift,vShift);
		}
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
		for (int i = 0; i < geoGridSideLength; i++) {
			Destroy (geoCells[i,0]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the right most column of the geoGrid
	 * 
	*/
	private void DeleteRightCol()
	{
		for (int i = 0; i < geoGridSideLength; i++) {
			Destroy (geoCells[i,geoGridSideLength-1]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the bottom most row of the geoGrid
	 * 
	*/
	private void DeleteBotRow()
	{
		for (int i = 0; i < geoGridSideLength; i++) {
			Destroy (geoCells[0,i]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the top most row of the geoGrid
	 * 
	*/
	private void DeleteTopRow()
	{
		for (int i = 0; i < geoGridSideLength; i++) {
			Destroy (geoCells[geoGridSideLength-1,i]);
		}
	}

	/**
	 * Description: shifts the geoCells 'Left' in the 2d array representation of the geoGrid
	 * 
	*/
	private void ShiftGridLeft()
	{
		for (int i = 0; i < geoGridSideLength; i++) {
			for(int j = 0; j < geoGridSideLength - 1; j++)
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
		for(int i = 0; i < geoGridSideLength; i++)
		{
			for(int j = geoGridSideLength - 1; j > 0; j--)
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
		for (int i = geoGridSideLength - 1; i > 0; i--) {
			for (int j = 0; j < geoGridSideLength; j++)
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
		for (int i = 0; i < geoGridSideLength - 1; i++) {
			for(int j = 0; j < geoGridSideLength; j++)
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

		for (int i = 0; i < geoGridSideLength; i++) {
			vec = new Vector3 (geoCells[i,geoGridSideLength - 1].transform.localPosition.x + geoCellHeight,
				geoCells[i, geoGridSideLength - 1].transform.localPosition.y);

			geoCells[i, geoGridSideLength - 1] = (GameObject)Instantiate (geoCellPrefab, vec,
				geoCellPrefab.transform.localRotation);

			geoCells [i, geoGridSideLength - 1].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: Spawns a new instantiated geoCells on the left most column of the geoGrid
	*/
	private void SpawnLeft()
	{
		Vector3 vec;

		for (int i = 0; i < geoGridSideLength; i++) {
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

		for (int i = 0; i < geoGridSideLength; i++) {
			vec = new Vector3 (geoCells[geoGridSideLength - 1, i].transform.localPosition.x,
				geoCells[geoGridSideLength - 1, i].transform.localPosition.y + geoCellWidth);

			geoCells[geoGridSideLength - 1, i] = (GameObject)Instantiate (geoCellPrefab, vec,
				geoCellPrefab.transform.localRotation);

			geoCells [geoGridSideLength - 1, i].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: SPawns a new intsantiated geoCells on the botom most row of the geoGrid 
	 * 
	*/
	private void SpawnDown()
	{
		Vector3 vec;

		for (int i = 0; i < geoGridSideLength; i++) {
			vec = new Vector3 (geoCells[0, i].transform.localPosition.x ,
				geoCells[0, i].transform.localPosition.y - geoCellWidth);

			geoCells[0, i] = (GameObject)Instantiate (geoCellPrefab, vec,
				geoCellPrefab.transform.localRotation);

			geoCells [0, i].transform.SetParent (this.transform);
		}
	}

	// Update is called once per frame
	void Update () {
//		Thread thr = new Thread (new ThreadStart(alignGrid));
//		thr.Start ();
		alignGrid ();
	}
		
}//End Of Class
