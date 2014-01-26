using UnityEngine;
using System.Collections;

public class PlayerScript_mine : MonoBehaviour {
	public Vector2 speed = new Vector2(50, 50);
	public Vector2 touchSpeed = new Vector2(0.1f,0.1f);
	private Vector2 movement;
	private Vector2 touchPosition;
	private Vector2 touchDeltaPosition;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		/*
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Moved) {
				touchDeltaPosition = touch.deltaPosition;
				touchPosition = touch.position;
			}
		} */

		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY);

		bool shoot = Input.GetButtonDown ("Fire1");
		shoot |= Input.GetButtonDown ("Fire2");

		if (shoot) {
			WeaponScript weapon = GetComponent<WeaponScript>();
			if(weapon != null){
				weapon.Attack(false);
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, bottomBorder, topBorder),
			transform.position.z
			);
	}

	void FixedUpdate() {
		rigidbody2D.velocity = movement;
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			touchPosition = touch.position;
			Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
			if(touch.phase == TouchPhase.Began) {
				//transform.Translate(worldTouchPosition.x * touchSpeed.x, worldTouchPosition.y * touchSpeed.y, 0);
				MoveObject(transform, transform.position, worldTouchPosition, 1.0f);
			} else {
				transform.position = new Vector3(worldTouchPosition.x + 1, worldTouchPosition.y, 0);
			}
		}

	}

	void OnDestroy()
	{
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}

	IEnumerator MoveObject (Transform thisTransform , Vector3 startPos, Vector3 endPos, float time) {
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}
}
