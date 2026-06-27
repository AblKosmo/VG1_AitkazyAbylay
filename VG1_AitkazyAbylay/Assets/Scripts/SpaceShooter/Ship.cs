using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


 
namespace SpaceShooter {
	public class Ship : MonoBehaviour
	{
		// Outlets
        public GameObject projectilePrefab;
        public Image imageHealthBar;
		Rigidbody2D _rigidbody2D;
		public TMP_Text hullUpgradeText;
		public TMP_Text fireSpeedUpgradeText;
		
		// State Tracking
        public float healthMax = 100f;
        public float health = 100f;		
		public float firingDelay = 1f;

        void Update() {
    if(health > 0) {
        float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
        transform.position = new Vector2(0, yPosition);
    }
}
		void Die() {
    StopCoroutine("FiringTimer");

    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0;
    rb.bodyType = RigidbodyType2D.Dynamic;
}
		void TakeDamage(float damageAmount) {
    health -= damageAmount;

    if(health <= 0) {
        Die();
    }

    imageHealthBar.fillAmount = health / healthMax;
}

		void OnCollisionEnter2D(Collision2D other) {
    if(other.gameObject.GetComponent<Asteroid>()) {
        TakeDamage(10f);
    }
}

		public void UpgradeFireSpeed() {
    int cost = 100 + Mathf.RoundToInt((1f - firingDelay) * 100f);

    if(GameController.instance.money >= cost) {
        GameController.instance.money -= cost;

        firingDelay -= 0.05f;

        int newCost = 100 + Mathf.RoundToInt((1f - firingDelay) * 100f);
        fireSpeedUpgradeText.text = "Fire Speed $" + newCost;
    }
}

		public void UpgradeHull() {
    int cost = Mathf.RoundToInt(healthMax);

    if(GameController.instance.money >= cost) {
        GameController.instance.money -= cost;

        health += 50;
        healthMax += 50;
        imageHealthBar.fillAmount = health / healthMax;

        hullUpgradeText.text = "Hull Strength $" + Mathf.RoundToInt(healthMax);
    }
}
		public void RepairHull() {
    int cost = 100;

    if(GameController.instance.money >= cost && health < healthMax && health > 0) {
        GameController.instance.money -= cost;

        health = healthMax;
        imageHealthBar.fillAmount = health / healthMax;
    }
}
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