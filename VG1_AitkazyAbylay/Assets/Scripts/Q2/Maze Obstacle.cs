using UnityEngine;
using UnityEngine.SceneManagement;
 
namespace Q2 {
	public class MazeObstacle : MonoBehaviour
	{
		void OnCollisionEnter2D(Collision2D collision) {
			// Reload scene only when colliding with player
			if(collision.gameObject.GetComponent<RotatingShipController>()) {
				string currentScene = SceneManager.GetActiveScene().name;
				SceneManager.LoadScene(currentScene);
			}
		}
	}
}