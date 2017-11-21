using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health = 150.0f;
	public GameObject laserPrefab;
	public float laserSpeed;
	public float shotsPerSecond = 0.5f;
	public int pointsWhenDestroyed = 150;
	public AudioClip shootSound;
	public AudioClip destroyedSound;
	public ParticleSystem explosionPrefab;

	private GameObject laser;
	private double timeTilNextShot;
	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		Projectile laser = col.gameObject.GetComponent<Projectile> ();
		if (laser) {
			health -= laser.GetDamage();
			laser.Hit ();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die() {
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
		scoreKeeper.Score (pointsWhenDestroyed);
		Vector3 cameraZPos = new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z);
		AudioSource.PlayClipAtPoint (destroyedSound, cameraZPos, 2.0f);
	}

	void Update() {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void Fire() {
		laser = Instantiate (laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0, -laserSpeed));
		Vector3 cameraZPos = new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z);
		AudioSource.PlayClipAtPoint (shootSound, cameraZPos, 4.0f);
	}
}
