using UnityEngine;
using UnityEngine.InputSystem;
 
namespace SplitScreen {
	public class PlayerController : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rigidbody;
 
		// Configuration
		public Key keyUp;
		public Key keyDown;
		public Key keyLeft;
		public Key keyRight;
		public float moveSpeed;
	
		// Methods
		void Start() {
			_rigidbody = GetComponent<Rigidbody2D>();
		}
 
		void FixedUpdate() {
			if(Keyboard.current[keyUp].isPressed) {
				_rigidbody.AddForce(moveSpeed * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
			}
			if(Keyboard.current[keyDown].isPressed) {
				_rigidbody.AddForce(moveSpeed * Time.fixedDeltaTime * Vector2.down, ForceMode2D.Impulse);
			}
			if(Keyboard.current[keyLeft].isPressed) {
				_rigidbody.AddForce(moveSpeed * Time.fixedDeltaTime * Vector2.left, ForceMode2D.Impulse);
			}
			if(Keyboard.current[keyRight].isPressed) {
				_rigidbody.AddForce(moveSpeed * Time.fixedDeltaTime * Vector2.right, ForceMode2D.Impulse);
			}
		}    
    
    }
    
}