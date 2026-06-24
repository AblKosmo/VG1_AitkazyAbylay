using UnityEngine;
 
namespace SpaceShooter {
	public class Asteroid : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rb;
		
		// State Tracking
		float randomSpeed;
		
		// Methods
		void Start() {
			_rb = GetComponent<Rigidbody2D>();
			randomSpeed = Random.Range(0.5f, 3f);
		}
 
		void FixedUpdate() {
			_rb.linearVelocityX = -randomSpeed;
		}
 
		void OnBecameInvisible() {
			Destroy(gameObject);
		}
	}
}