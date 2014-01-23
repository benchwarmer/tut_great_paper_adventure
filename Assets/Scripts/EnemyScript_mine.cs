using UnityEngine;

public class EnemyScript_mine : MonoBehaviour {

	private WeaponScript_mine[] weapons;
	// Use this for initialization
	void Awake() {
		weapons = GetComponentsInChildren<WeaponScript_mine>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(WeaponScript_mine weapon in weapons){

			if (weapon != null) {
				weapon.Attack(true);
			}
		}
	
	}
}
