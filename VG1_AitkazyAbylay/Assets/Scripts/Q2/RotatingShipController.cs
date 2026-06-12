using UnityEngine;
using UnityEngine.InputSystem;
 
namespace Q2 {
	public class RotatingShipController : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rb;
 
		// Configuration
		public float speed;
		public float rotationSpeed;
 
		// Methods
		void Start() {
			_rb = GetComponent<Rigidbody2D>();
		}
 
		void Update() {
			// Turn Left
			if(Keyboard.current.leftArrowKey.isPressed) {
				_rb.AddTorque(rotationSpeed * Time.deltaTime);
			}
			
			// Turn Right
			if(Keyboard.current.rightArrowKey.isPressed) {
				_rb.AddTorque(-rotationSpeed * Time.deltaTime);
			}
			
			// Thrust Forward
			if(Keyboard.current.spaceKey.isPressed) {
				_rb.AddRelativeForce(speed * Time.deltaTime * Vector2.right);
			}
		}
	}
}