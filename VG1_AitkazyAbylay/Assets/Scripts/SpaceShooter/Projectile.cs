using UnityEngine;
 
namespace SpaceShooter {
	public class Projectile : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rb;
		
		// State Tracking
		Transform target;
		
		// Methods
		void Start() {
			_rb = GetComponent<Rigidbody2D>();
		}
		void ChooseNearestTarget() {
			// High default means first asteroid will be sort as closer
			float closestDistance = float.MaxValue;
			
			// Expensive. Better approach would be Object Pooling
			Asteroid[] asteroids = FindObjectsByType<Asteroid>();
			
			// Check each asteroid to see if it's the closest
			for(int i = 0; i < asteroids.Length; i++) {
				Asteroid asteroid = asteroids[i];
				
				// Asteroid must be to our right
				if(asteroid.transform.position.x > transform.position.x) {
					Vector2 targetDirection = asteroid.transform.position - transform.position;
					float targetDistance = targetDirection.sqrMagnitude;
					
					// Filter for the closest target we've seen so far
					if(targetDistance < closestDistance) {
						// Update closest distance for the future comparisons
						closestDistance = targetDistance;
						
						// Track this asteroid as the current closest target
						target = asteroid.transform;
					}
				}
			}
		}
		void FixedUpdate() {
			// TODO: Make these dynamic
			float acceleration = 1f;
			float maxSpeed = 2f;
			
			// Home in on target
			ChooseNearestTarget();
			if(target) {
				// Rotate towards target
				Vector2 targetDirection = target.position - transform.position;
				float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
				
				_rb.MoveRotation(angle);
			}
			
			// Accelerate Forward
			_rb.AddForce(acceleration * Time.fixedDeltaTime * 50f * transform.right, ForceMode2D.Impulse);
			
			// Cap max speed
			_rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, maxSpeed);
		}
		void OnCollisionEnter2D(Collision2D collision) {
			// Only explode on Asteroids
			if(collision.gameObject.GetComponent<Asteroid>()) {
				Destroy(collision.gameObject);
				Destroy(gameObject);
				
				// Create an explosion and destroy it soon after
				GameObject explosion = Instantiate(
					GameController.instance.explosionPrefab, 
					transform.position, 
					Quaternion.identity
				);
				Destroy(explosion, 0.25f);
			}
		}

	}
}