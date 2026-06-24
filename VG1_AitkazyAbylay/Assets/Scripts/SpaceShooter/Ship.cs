using UnityEngine;
using System.Collections;

 
namespace SpaceShooter {
	public class Ship : MonoBehaviour
	{
		// Outlets
		Rigidbody2D _rigidbody2D;
		public GameObject projectilePrefab;
		
		// State Tracking
		public float firingDelay = 1f;
        
		
		// Methods
		void Start() {
			_rigidbody2D = GetComponent<Rigidbody2D>();
 			StartCoroutine("FiringTimer");
           
		}
		
		void FixedUpdate() {
			float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
			_rigidbody2D.MovePosition(new Vector2(0, yPosition));
		}
		void FireProjectile() {
			Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		}  
		IEnumerator FiringTimer() {
			yield return new WaitForSeconds(firingDelay);
			
			FireProjectile();
			
			StartCoroutine("FiringTimer");
		}      
	}
}