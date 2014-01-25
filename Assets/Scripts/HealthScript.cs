using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
  /// <summary>
  /// Total hitpoints
  /// </summary>
  public int hp = 1;

  /// <summary>
  /// Enemy or player?
  /// </summary>
  public bool isEnemy = true;	

  void OnTriggerEnter2D(Collider2D otherCollider)
  {
    // Is this a shot?
    ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
    if (shot != null)
    {
      // Avoid friendly fire
      if (shot.isEnemyShot != isEnemy)
      {
		// Destroy the shot
		Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script


        hp -= shot.damage;
		SoundEffectsHelper.Instance.MakeExplosionSound();
		
	    StopCoroutine("addHitColor");
		StartCoroutine("addHitColor");


       
        if (hp <= 0)
        {
          // Explosion!
		  SpecialEffectsHelper.Instance.Explosion(transform.position);

          // Dead!
          Destroy(gameObject);
        }
      }
    }
  }

  public void addHitColor() {
		SpriteRenderer theSprite = GetComponent<SpriteRenderer>();
		Color defaultColor = theSprite.color;
		theSprite.color = Color.red;
		yield WaitForSeconds(1);
		theSprite.color = defaultColor;

  }
}