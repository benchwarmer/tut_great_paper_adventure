using UnityEngine;

public class HealthScript_mine : MonoBehaviour {
	public int hp = 2;
	public bool isEnemy = true;

	void onTriggerEnter2D(Collider2D collider) {
		ShotScript_mine shot = collider.gameObject.GetComponent<ShotScript_mine>();
		if (shot != null) {
			if (shot.isEnemyShot != isEnemy){
				hp -= shot.damage;
				Destroy(shot.gameObject);

				if(hp <= 0){
					Destroy(gameObject);
				}

			}

		}
	}
}
