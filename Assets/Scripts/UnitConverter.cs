using UnityEngine;
using System.Collections;

/**
 * Description: A static class with unit converion functionalities in the unity engine
 * 
*/
public class UnitConverter : MonoBehaviour {

	[SerializeField] private int pixelsPerUnit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//getter
	public int PixelPerUnit
	{
		get{ return pixelsPerUnit;}
	}


	/**
	 * Description: converts the given pixels to unity units
	*/
	public float toUnits(float pixels)
	{
		return pixels / pixelsPerUnit;
	}

}
