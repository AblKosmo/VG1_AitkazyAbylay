using UnityEngine;
 
namespace Platformer {
	public class Target : MonoBehaviour
	{
		void OnCollisionEnter2D(Collision2D collision) {
			if(collision.gameObject.GetComponent<Projectile>()) {
				Destroy(gameObject);
			}
		}
	}
}