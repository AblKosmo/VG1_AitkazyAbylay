using UnityEngine;
using UnityEngine.InputSystem;
 
namespace Platformer {
	public class PlayerController : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rigidbody2D;
		public Transform aimPivot;
		public GameObject projectilePrefab;
		// State Tracking
		public int jumpsLeft;

		// Methods
		void Start() {
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}
 
		void Update() {
			// Move Player Left
			if(Keyboard.current.aKey.isPressed) {
				_rigidbody2D.AddForce(18f * Time.deltaTime * Vector2.left, ForceMode2D.Impulse);
			}
			
			// Move Player Right
			if(Keyboard.current.dKey.isPressed) {
				_rigidbody2D.AddForce(18f * Time.deltaTime * Vector2.right, ForceMode2D.Impulse);
			}
			// Aim Toward Mouse
			Vector3 mousePosition = Mouse.current.position.ReadValue();
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			Vector3 hypotenuse = mouseWorldPosition - transform.position;
 
			float mouseRadians = Mathf.Atan2(hypotenuse.y, hypotenuse.x);
			float mouseAngle = mouseRadians * Mathf.Rad2Deg;
 
			aimPivot.rotation = Quaternion.Euler(0, 0, mouseAngle);
			
			// Shoot
			if(Mouse.current.leftButton.wasPressedThisFrame) {
				GameObject projectile = Instantiate(projectilePrefab);
				projectile.transform.position = transform.position;
				projectile.transform.rotation = aimPivot.rotation;
			}

			// Jump
			if(Keyboard.current.spaceKey.wasPressedThisFrame) {
				if(jumpsLeft > 0) {
					jumpsLeft--;
					_rigidbody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
				}
			}
		}
		void OnCollisionStay2D(Collision2D collision) {
			// Check that we collided with Ground
			if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				// Check what is directly below our character's feet
				RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.85f);
				// Debug.DrawRay(transform.position, Vector2.down * 0.65f); // Visualize Raycast
				
				// We might have multiple things below our character's feet
				for(int i = 0; i < hits.Length; i++) {
					RaycastHit2D hit = hits[i];
					
					// Check that we collided with ground below our feet
					if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
						// Reset jump count
						jumpsLeft = 2;
					}
				}
			}
		}

	}
}
