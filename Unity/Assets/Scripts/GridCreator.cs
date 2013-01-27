using UnityEngine;
using System.Collections;

public class GridCreator : MonoBehaviour
{
	#region GLOBAL_VARIABLES
	// holds the prefab that creates the grid
	[SerializeField]
	private Transform squarePrefab;
	[SerializeField]
	private Transform linePrefab;
	
	// holds the dimensions for the grid
	public int gridX;
	public int gridY;
	
	#endregion
	
	#region UNITY_FUNCTIONS
	// Use this for initialization
	void Start ()
	{
		CreateGrid ();
	}
	
	void OnDestroy ()
	{
		foreach (Transform child in transform) {
			Destroy (child);
		}
	}
	
	#endregion
	
	#region GRID_FUNCTIONS
	public void CreateGrid ()
	{
		// first destroy all the children before creating a new grid
		DestroyAllChildren ();
		
		// create a grid icon for each location on the grid
		for (int x = 0; x < gridX; x++) {
			// create the column
			GameObject backgroundColumn = new GameObject ();
			backgroundColumn.name = "Col " + x;
			backgroundColumn.transform.parent = transform;
			backgroundColumn.transform.localPosition = new Vector3 (0.0F, 0.0F, 0.0F);
			
			// create the first line
			Transform lineOne = (Transform)Instantiate (linePrefab);
			lineOne.name = "Line (" + x + ", 0)";
			lineOne.parent = backgroundColumn.transform;
			lineOne.localPosition = new Vector3 ((float)x, -(squarePrefab.localScale.y * 0.5F), 0.0F);
			
			for (int y = 0; y < gridY; y++) {
				// create the square
				Transform square = (Transform)Instantiate (squarePrefab);
				square.name = "Square (" + x + ", " + y + ")";
				square.parent = backgroundColumn.transform;
				square.localPosition = new Vector3 ((float)x, (float)y, 0.0F);
				
				// create the horizontal line
				Transform lineHorizontal = (Transform)Instantiate (linePrefab);
				lineHorizontal.name = "LineHorizontal (" + x + ", " + (y + 1) + ")";
				lineHorizontal.parent = backgroundColumn.transform;
				lineHorizontal.localPosition = new Vector3 ((float)x, ((float)y + (squarePrefab.localScale.y * 0.5F)), 0.0F);
				
				if (x == 0) {
					// create the vertical line
					Transform lineVerticalOne = (Transform)Instantiate (linePrefab);
					lineVerticalOne.name = "LineVertical (" + (x) + ", " + y + ")";
					lineVerticalOne.parent = backgroundColumn.transform;
					lineVerticalOne.localEulerAngles = new Vector3 (0.0F, 0.0F, 90.0F);
					lineVerticalOne.localPosition = new Vector3 (((float)x - (squarePrefab.localScale.x * 0.5F)), (float)y, 0.0F);
				}
				
				// create the vertical line
				Transform lineVertical = (Transform)Instantiate (linePrefab);
				lineVertical.name = "LineVertical (" + (x + 1) + ", " + y + ")";
				lineVertical.parent = backgroundColumn.transform;
				lineVertical.localEulerAngles = new Vector3 (0.0F, 0.0F, 90.0F);
				lineVertical.localPosition = new Vector3 (((float)x + (squarePrefab.localScale.x * 0.5F)), (float)y, 0.0F);
			}
		}
	}
	
	void DestroyAllChildren ()
	{
		foreach (GameObject child in transform) {
			Destroy (child);
		}
	}
	
	#endregion
}
