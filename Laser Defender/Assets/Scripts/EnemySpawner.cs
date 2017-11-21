using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10.0f;
	public float height = 5.0f;
	public float speed = 1;
	public float spawnDelay = 0.5f;

	private float xmin;
	private float xmax;
	private bool movingRight = true;

	void Start () {
		SpawnUntilFull ();
		CalculateScreenBoundaries ();
	}

	void AddEnemiesToFormation() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
			enemy.transform.Rotate(new Vector3(0,0,180));
		}
	}

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}


	void CalculateScreenBoundaries() {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xmin = leftBoundary.x + width/2;
		xmax = rightBoundary.x - width/2;
	}

	void Update () {
		MoveLeftOrRight ();
		ReverseDirectionAtBoundaries ();

		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}

	void MoveLeftOrRight() {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	void ReverseDirectionAtBoundaries() {
		if (transform.position.x > xmax) {
			movingRight = false;
		} else if (transform.position.x < xmin) {
			movingRight = true;
		}
	}

	Transform NextFreePosition() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
