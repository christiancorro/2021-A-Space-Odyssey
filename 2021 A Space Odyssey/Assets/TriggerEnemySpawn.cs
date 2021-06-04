using UnityEngine;

public class TriggerEnemySpawn : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int numberOfEnemies;
    [SerializeField] bool triggerOnlyOnce = true;
    private bool triggered = false;

    void Start() {
        // InvokeRepeating("SpawnEnemy", 0, 1);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {

            if (triggerOnlyOnce && !triggered) {
                triggered = true;
                SpawnEnemy();
            }

            if (!triggerOnlyOnce) {
                triggered = true;
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy() {
        Debug.DrawLine(new Vector3(transform.position.x - 300, transform.position.y, 0), new Vector3(transform.position.x + 300, transform.position.y, 0), Color.green, 2);

        for (int i = 0; i < numberOfEnemies; i++) {
            Vector3 position = new Vector3(spawnPoint.position.x + (-numberOfEnemies / 2 + i), spawnPoint.transform.position.y, 0);
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }
}
