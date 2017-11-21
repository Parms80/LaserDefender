using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float padding = 1;
	public GameObject laserPrefab;
	public float laserSpeed;
	public float firingRate = 0.2f;
	public float health = 250.0f;
	public AudioClip shootSound;
	public AudioClip destroyedSound;
	public ParticleSystem explosionPrefab;

	private GameObject laser;
	float xmin;
	float xmax;

	void Start() {
		CalculateScreenBoundaries ();
	}

	void CalculateScreenBoundaries() {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xmin = leftBoundary.x + padding;
		xmax = rightBoundary.x - padding;
	}
		

	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		RestrictPlayerToGameSpace ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}

	void RestrictPlayerToGameSpace() {
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void Fire() {
		Vector3 offset = new Vector3 (0, 1, 0);
		laser = Instantiate (laserPrefab, transform.position + offset, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0, laserSpeed));
		Vector3 cameraZPos = new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z);
		AudioSource.PlayClipAtPoint (shootSound, cameraZPos, 5.0f);
	}

	void OnTriggerEnter2D(Collider2D collider) {

		Projectile laser = collider.gameObject.GetComponent<Projectile> ();
		if (laser) {
			Debug.Log ("Laser hit player");

			health -= laser.GetDamage();
			laser.Hit ();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die() {
		StartExplosion ();
		gameObject.SetActive(false);
		CancelInvoke ("Fire");
		Invoke("LoadLevel", 2);
	}

	void StartExplosion() {
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Vector3 cameraZPos = new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z);
		AudioSource.PlayClipAtPoint (destroyedSound, cameraZPos, 2.0f);
	}		

	void LoadLevel() {
		Debug.Log ("LoadLevel");
		Destroy (gameObject);
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		man.LoadLevel ("Win Screen");
	}
}
