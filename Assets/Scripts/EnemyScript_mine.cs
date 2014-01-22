using UnityEngine;
using System.Collections;

public class EnemyScript_mine : MonoBehaviour {

	private WeaponScript[] weapons;
	// Use this for initialization
	void Awake() {
		weapons = GetComponentsInChildren<WeaponScript>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(WeaponScript weapon in weapons){

			if (weapon != null) {
				weapon.Attack(true);
			}
		}
	
	}
}
