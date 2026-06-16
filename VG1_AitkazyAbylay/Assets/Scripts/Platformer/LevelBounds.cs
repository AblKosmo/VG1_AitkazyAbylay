using UnityEngine;
using UnityEngine.SceneManagement;
 
namespace Platformer {
	public class LevelBounds : MonoBehaviour
	{
		void OnTriggerEnter2D(Collider2D other) {
			if(other.gameObject.GetComponent<PlayerController>()) {
				string currentScene = SceneManager.GetActiveScene().name;
				SceneManager.LoadScene(currentScene);
			}
		}
	}
}