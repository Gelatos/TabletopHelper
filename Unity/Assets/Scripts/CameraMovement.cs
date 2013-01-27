using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private SwipeController swipeController;
	private Vector3 positionModifier;
	
	// Use this for initialization
	void Start ()
	{
		positionModifier = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (swipeController.swipeDirection) {
		case SwipeController.FingerSwipeDirection.DOWN:
			positionModifier += new Vector3 (0.0F, -(0.05F + (swipeController.yPowerDegree * 0.05F)), 0.0F);
			break;
		case SwipeController.FingerSwipeDirection.UP:
			positionModifier += new Vector3 (0.0F, (0.05F + (swipeController.yPowerDegree * 0.05F)), 0.0F);
			break;
		case SwipeController.FingerSwipeDirection.LEFT:
			positionModifier += new Vector3 (-(0.05F + (swipeController.xPowerDegree * 0.05F)), 0.0F, 0.0F);
			break;
		case SwipeController.FingerSwipeDirection.RIGHT:
			positionModifier += new Vector3 ((0.05F + (swipeController.xPowerDegree * 0.05F)), 0.0F, 0.0F);
			break;
		case SwipeController.FingerSwipeDirection.DOWN_LEFT:
			positionModifier += new Vector3 (-(0.05F + (swipeController.xPowerDegree * 0.05F)), -(0.05F + (swipeController.yPowerDegree * 0.05F)), 0.0F);
			break;
		case SwipeController.FingerSwipeDirection.DOWN_RIGHT:
			positionModifier += new Vector3 ((0.05F + (swipeController.xPowerDegree * 0.05F)), -(0.05F + (swipeController.yPowerDegree * 0.05F)), 0.0F);
			break;
		case SwipeController.FingerSwipeDirection.UP_LEFT:
			positionModifier += new Vector3 (-(0.05F + (swipeController.xPowerDegree * 0.05F)), (0.05F + (swipeController.yPowerDegree * 0.05F)), 0.0F);
			break;
		case SwipeController.FingerSwipeDirection.UP_RIGHT:
			positionModifier += new Vector3 ((0.05F + (swipeController.xPowerDegree * 0.05F)), (0.05F + (swipeController.yPowerDegree * 0.05F)), 0.0F);
			break;
		}
		
		transform.position = positionModifier;
	}
}
