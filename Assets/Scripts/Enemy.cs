using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[Header("Enemy Stats")]
	[SerializeField] float health = 100f;
	[SerializeField] int scoreValue = 150;

	[Header("Shooting")]
	float shotCounter;
	[SerializeField] float minTimeBetweenShots = 0.2f;
	[SerializeField] float maxTimeBetweenShots = 3f;
	[SerializeField] GameObject laserPrefab;
	[SerializeField] float projectileSpeed = 10f;

	[Header("Sound Effects")]
	[SerializeField] GameObject explosionEffect;
	[SerializeField] float explosionTime = 1f;
	[SerializeField] AudioClip deathSFX;
	[SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
	[SerializeField] AudioClip shootSFX;
	[SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;

	// Use this for initialization
	void Start () {
		shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update () {
		CountDownAndShoot();
	}

	private void CountDownAndShoot() {
		shotCounter -= Time.deltaTime;
		if (shotCounter <= 0f) {
			Fire();
			shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
		}
	}

	private void Fire() {
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
		if (!damageDealer) {
            return;
        }
		ProcessHit(damageDealer);
	}

	private void ProcessHit(DamageDealer damageDealer) {
		health -= damageDealer.GetDamage();
		damageDealer.Hit();

		if (health <= 0) {
			Die();
		}
	}

	private void Die() {
		FindObjectOfType<GameSession>().AddToScore(scoreValue);
		Destroy(gameObject);
		GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
		Destroy(explosion, explosionTime);
		AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
	}
}
