using System.Collections;
using UnityEngine;
 
namespace SpaceShooter {
	public class GameController : MonoBehaviour
	{
		public static GameController instance;

			// Outlets
		public Transform[] spawnPoints;
		public GameObject[] asteroidPrefabs;
		public GameObject explosionPrefab;

				// Configuration
		public float maxAsteroidDelay = 2f;
		public float minAsteroidDelay = 0.2f;


		
		// State Tracking
		public float timeElapsed;
		public float asteroidDelay;
		
		// Methods
		void Awake() {
			instance = this;
		}
		void Start() {
			StartCoroutine("AsteroidSpawnTimer");
		}		
 
		void Update() {
			// Increment the passage of time for each frame of the game
			timeElapsed += Time.deltaTime;
			// Compute Asteroid Delay
			asteroidDelay = maxAsteroidDelay - (maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed;
			asteroidDelay = Mathf.Clamp(asteroidDelay, minAsteroidDelay, maxAsteroidDelay);			
		}


		void SpawnAsteroid() {
			// Pick random spawn points and random asteroid prefabs
			int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
			Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
			int randomAsteroidIndex = Random.Range(0, asteroidPrefabs.Length);
			GameObject randomAsteroid = asteroidPrefabs[randomAsteroidIndex];
			
			// Spawn
			Instantiate(randomAsteroid, randomSpawnPoint.position, Quaternion.identity);
		}
		IEnumerator AsteroidSpawnTimer() {
			// Wait
			yield return new WaitForSeconds(asteroidDelay);
 
			// Spawn
			SpawnAsteroid();
 
			// Repeat
			StartCoroutine("AsteroidSpawnTimer");
		}	
	}
}