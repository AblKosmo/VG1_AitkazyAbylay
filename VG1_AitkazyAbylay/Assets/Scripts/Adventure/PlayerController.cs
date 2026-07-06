using UnityEngine;
using UnityEngine.InputSystem;
 
namespace Adventure {
	public enum Direction {
		Up = 0, 
		Down = 1,
		Left = 2,
		Right = 3
	}
    

	public class PlayerController : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rigidbody;
		Animator _animator;
		SpriteRenderer _spriteRenderer;
        public Transform[] attackZones;


        

		// Configuration
		public Key keyUp;
		public Key keyDown;
		public Key keyLeft;
		public Key keyRight;
		public float moveSpeed;
		public Sprite[] sprites;

 		public Direction facingDirection;

		
		// Methods
		void Start() {
			_rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();

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

		void Update() {
			float movementSpeed = _rigidbody.linearVelocity.sqrMagnitude;
			_animator.SetFloat("speed", movementSpeed);
			if(movementSpeed > 0.1f) {
				_animator.SetFloat("movementX", _rigidbody.linearVelocity.x);
				_animator.SetFloat("movementY", _rigidbody.linearVelocity.y);
			}
 
			if(Keyboard.current.spaceKey.wasPressedThisFrame) {
				_animator.SetTrigger("attack");
			}


				// Convert the enumeration to an index
				int facingDirectionIndex = (int)facingDirection;
 
				// Get an attack zone from index
				Transform attackZone = attackZones[facingDirectionIndex];
 
				// What objects are within a circle at that attack zone?
				Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, 0.1f);
 
				// Handle each hit target
				foreach(Collider2D hit in hits) {
					Breakable breakableObject = hit.GetComponent<Breakable>();
					if(breakableObject) {
						breakableObject.Break();
					}
				}




            
		}  
		void LateUpdate() {
			for(int i = 0; i < sprites.Length; i++) {
				if(_spriteRenderer.sprite == sprites[i]) {
					facingDirection = (Direction)i;
					break;
				}
			}
		}        

	}
}