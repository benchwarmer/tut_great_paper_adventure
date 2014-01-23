using UnityEngine;

public class ShotScript_mine : MonoBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 20);
	
	}
}
