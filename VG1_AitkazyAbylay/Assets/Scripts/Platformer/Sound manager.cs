using UnityEngine;
 
namespace Platformer {
	public class SoundManager : MonoBehaviour {
		public static SoundManager instance;
 
		// Outlets
		AudioSource audioSource;
		public AudioClip missSound;
		public AudioClip hitSound;
		
		// Methods
		void Awake() {
			instance = this;
		}
 
		void Start() {
			audioSource = GetComponent<AudioSource>();
		}
		public void PlaySoundHit() {
			audioSource.PlayOneShot(hitSound);
		}
 
		public void PlaySoundMiss() {
			audioSource.PlayOneShot(missSound);
		}
	}
}