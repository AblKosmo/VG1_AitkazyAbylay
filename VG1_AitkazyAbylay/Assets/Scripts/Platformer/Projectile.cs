using UnityEngine;
 
namespace Platformer {
	public class Projectile : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rigidbody2D;
 
		// Methods
		void Start() {
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_rigidbody2D.linearVelocity = transform.right * 10f;
		}
		void OnCollisionEnter2D(Collision2D collision) {
			if(collision.gameObject.GetComponent<Target>()) {
				SoundManager.instance.PlaySoundHit();
			} else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				SoundManager.instance.PlaySoundMiss();
			}
			Destroy(gameObject);

		}


        
	}
}