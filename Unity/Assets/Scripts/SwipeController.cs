using UnityEngine;
using System.Collections;

public class SwipeController : MonoBehaviour
{
	private Vector3 touchStartPos;
	public enum FingerSwipeDirection
	{
		NEUTRAL,
		UP,
		DOWN,
		LEFT,
		RIGHT,
		UP_RIGHT,
		UP_LEFT,
		DOWN_RIGHT,
		DOWN_LEFT
	}

	// determine which direction the swipe moves towards
	[HideInInspector]
	public FingerSwipeDirection swipeDirection;

	// determine how much the user must swipe in order to register movement
	private int screenMod;
	[SerializeField]
	private float xMinimumPercentageMovedToRegisterSwipe = 0.1F;
	[SerializeField]
	private float yMinimumPercentageMovedToRegisterSwipe = 0.1F;
	[SerializeField]
	private float xMinimumPercentageMovedToRegisterPowerIncrease = 0.15F;
	[SerializeField]
	private float yMinimumPercentageMovedToRegisterPowerIncrease = 0.15F;
	[SerializeField]
	private int xMaximumPowerIncrease = 5;
	[SerializeField]
	private int yMaximumPowerIncrease = 5;
	public int xPowerDegree;
	public int yPowerDegree;

	// user options
	public enum MovementCheck
	{
		ALL_MOVEMENT,
		ONLY_HORIZONTAL_MOVEMENT,
		ONLY_VERTICAL_MOVEMENT,
		ONLY_HORIZONTAL_AND_VERTICAL_MOVEMENT
	};
	
	[SerializeField]
	public MovementCheck checkForTheFollowingMovementType;

	// determine how much of the screen the user swiped
	[HideInInspector]
	public float xPositionMoved;
	[HideInInspector]
	public float yPositionMoved;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
	private Vector2? lastMousePosition;
#endif

	void Start ()
	{
		if (Screen.width >= Screen.height) {
			screenMod = Screen.height;
		} else {
			screenMod = Screen.width;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		// set the swipe direction to neutral
		swipeDirection = FingerSwipeDirection.NEUTRAL;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
		if (Input.GetMouseButton(0)) {
			UIFakeTouch touch = UIFakeTouch.fromInput(ref lastMousePosition); 
#else
		foreach (Touch touch in Input.touches) {
#endif
			if (UI.Instance.buttonAtPosition (touch.position)) {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
						return;
#else
				break;
#endif
			}

			// if its the first touch, set the start position for the touch so we can use it as a reference
			if (touch.phase == TouchPhase.Began) {
				touchStartPos = touch.position;
			}

			// setup the xPositionMoved and yPositionMoved
			xPositionMoved = (touch.position.x - touchStartPos.x) / screenMod;
			if (xPositionMoved > 1.0F) {
				xPositionMoved = 1.0F;
			} else if (xPositionMoved < -1.0F) {
				xPositionMoved = -1.0F;
			}
			yPositionMoved = (touch.position.y - touchStartPos.y) / screenMod;
			if (yPositionMoved > 1.0F) {
				yPositionMoved = 1.0F;
			} else if (yPositionMoved < -1.0F) {
				yPositionMoved = -1.0F;
			}
				
			// determine how much power the swipe has
			xPowerDegree = (int)(Mathf.Abs(xPositionMoved) / xMinimumPercentageMovedToRegisterPowerIncrease);
			if (xPowerDegree > xMaximumPowerIncrease) {
				xPowerDegree = xMaximumPowerIncrease;
			} else if (xPowerDegree < 1) {
				xPowerDegree = 1;
			}
			yPowerDegree = (int)(Mathf.Abs(yPositionMoved) / yMinimumPercentageMovedToRegisterPowerIncrease);
			if (yPowerDegree > yMaximumPowerIncrease) {
				yPowerDegree = yMaximumPowerIncrease;
			} else if (xPowerDegree < 1) {
				yPowerDegree = 1;
			}
				

			// determine if there was horizontal movement
			if (checkForTheFollowingMovementType != MovementCheck.ONLY_VERTICAL_MOVEMENT) {
				if (Mathf.Abs (xPositionMoved) > xMinimumPercentageMovedToRegisterSwipe) {
					if (xPositionMoved > 0) {
						swipeDirection = FingerSwipeDirection.RIGHT;
					} else {
						swipeDirection = FingerSwipeDirection.LEFT;
					}
				}
			}

			// deteriming if there was vertical movement
			if (checkForTheFollowingMovementType != MovementCheck.ONLY_HORIZONTAL_MOVEMENT) {
				if (Mathf.Abs (yPositionMoved) > yMinimumPercentageMovedToRegisterSwipe) {
					if (yPositionMoved > 0) {

						// determine if there was diagonal movement
						if (checkForTheFollowingMovementType == MovementCheck.ALL_MOVEMENT) {
							if (swipeDirection == FingerSwipeDirection.LEFT) {
								swipeDirection = FingerSwipeDirection.UP_LEFT;
								return;
							} else if (swipeDirection == FingerSwipeDirection.RIGHT) {
								swipeDirection = FingerSwipeDirection.UP_RIGHT;
								return;
							} 
						}
						swipeDirection = FingerSwipeDirection.UP;
					} else {

						// determine if there was diagonal movement
						if (checkForTheFollowingMovementType == MovementCheck.ALL_MOVEMENT) {
							if (swipeDirection == FingerSwipeDirection.LEFT) {
								swipeDirection = FingerSwipeDirection.DOWN_LEFT;
								return;
							} else if (swipeDirection == FingerSwipeDirection.RIGHT) {
								swipeDirection = FingerSwipeDirection.DOWN_RIGHT;
								return;
							}
						}
						swipeDirection = FingerSwipeDirection.DOWN;
					}
				}
			}
		}
	}
}