using UnityEngine;
using System.Collections.Generic;

public class GeoGrid : MonoBehaviour {

	[SerializeField] private int size;
	[SerializeField] private GameObject geoCell_Origin;
	[SerializeField] private GameObject geoCell_Center;

	private GeoCell geoCellComponent;
	private GameObject[,] geoCells;
	private int centerIndex;

	// Use this for initialization
	void Start () {

		geoCells = new GameObject[size,size];
		centerIndex = (size - 1) / 2;
		geoCellComponent = geoCell_Origin.GetComponent<GeoCell> ();

		SpawnGeoCells ();
	}

	private void SpawnGeoCells()
	{
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				SpawnCell (j * geoCellComponent.DimX, i * geoCellComponent.DimY, i, j);
			}
		}

		SetCenter (geoCells[centerIndex, centerIndex]);

		//PrintGrid ();
	}

	private GameObject SpawnCell(float posX, float posY, int row, int col)
	{
		geoCells[row,col] = (GameObject)Instantiate (geoCell_Origin, new Vector3 (posX, posY, 0f),
			geoCell_Origin.transform.localRotation);

		geoCells [row, col].transform.SetParent (this.transform);
		
		return geoCells [row, col];
	}

	public void SetCenter(GameObject geoCell)
	{
		geoCell_Center = geoCell;
		alignGrid ();
	}
	
	private bool alignGrid()
	{
		float hShift = 0f;
		float vShift = 0f;

		if(geoCell_Center != geoCells[centerIndex, centerIndex])
		{
			hShift = geoCells [centerIndex, centerIndex].transform.position.x - geoCell_Center.transform.position.x;
			vShift = geoCells [centerIndex, centerIndex].transform.position.y - geoCell_Center.transform.position.y;

			alignGridHelper (hShift,vShift);
		}

		return false;
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
		for (int i = 0; i < size; i++) {
			Destroy (geoCells[i,0]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the right most column of the geoGrid
	 * 
	*/
	private void DeleteRightCol()
	{
		for (int i = 0; i < size; i++) {
			Destroy (geoCells[i,size-1]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the bottom most row of the geoGrid
	 * 
	*/
	private void DeleteBotRow()
	{
		for (int i = 0; i < size; i++) {
			Destroy (geoCells[0,i]);
		}
	}

	/**
	 * Description: Destroys the game objects stored in the top most row of the geoGrid
	 * 
	*/
	private void DeleteTopRow()
	{
		for (int i = 0; i < size; i++) {
			Destroy (geoCells[size-1,i]);
		}
	}

	/**
	 * Description: shifts the geoCells 'Left' in the 2d array representation of the geoGrid
	 * 
	*/
	private void ShiftGridLeft()
	{
		for (int i = 0; i < size; i++) {
			for(int j = 0; j < size - 1; j++)
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
		for(int i = 0; i < size; i++)
		{
			for(int j = size - 1; j > 0; j--)
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
		for (int i = size - 1; i > 0; i--) {
			for (int j = 0; j < size; j++)
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
		for (int i = 0; i < size - 1; i++) {
			for(int j = 0; j < size; j++)
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

		for (int i = 0; i < size; i++) {
			vec = new Vector3 (geoCells[i,size - 1].transform.localPosition.x + geoCellComponent.DimY,
				geoCells[i, size - 1].transform.localPosition.y);

			geoCells[i, size - 1] = (GameObject)Instantiate (geoCell_Origin, vec,
				geoCell_Origin.transform.localRotation);

			geoCells [i, size - 1].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: Spawns a new instantiated geoCells on the left most column of the geoGrid
	*/
	private void SpawnLeft()
	{
		Vector3 vec;

		for (int i = 0; i < size; i++) {
			vec = new Vector3 (geoCells[i, 0].transform.localPosition.x - geoCellComponent.DimY,
				geoCells[i, 0].transform.localPosition.y);

			geoCells[i, 0] = (GameObject)Instantiate (geoCell_Origin, vec,
				geoCell_Origin.transform.localRotation);

			geoCells [i, 0].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: Spawns a new instantiated geoCells on the top most row of the geoGrid
	*/
	private void SpawnUp()
	{
		Vector3 vec;

		for (int i = 0; i < size; i++) {
			vec = new Vector3 (geoCells[size - 1, i].transform.localPosition.x,
				geoCells[size - 1, i].transform.localPosition.y + geoCellComponent.DimX);

			geoCells[size - 1, i] = (GameObject)Instantiate (geoCell_Origin, vec,
				geoCell_Origin.transform.localRotation);

			geoCells [size - 1, i].transform.SetParent (this.transform);
		}
	}

	/**
	 * Description: SPawns a new intsantiated geoCells on the botom most row of the geoGrid 
	 * 
	*/
	private void SpawnDown()
	{
		Vector3 vec;

		for (int i = 0; i < size; i++) {
			vec = new Vector3 (geoCells[0, i].transform.localPosition.x ,
				geoCells[0, i].transform.localPosition.y - geoCellComponent.DimX);

			geoCells[0, i] = (GameObject)Instantiate (geoCell_Origin, vec,
				geoCell_Origin.transform.localRotation);

			geoCells [0, i].transform.SetParent (this.transform);
		}
	}

	private void PrintGrid()
	{
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Debug.Log ("x: " + i + " y: " + j + geoCells[i,j].transform.position);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
		
}
