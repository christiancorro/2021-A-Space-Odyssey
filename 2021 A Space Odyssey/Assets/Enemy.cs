using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] float attackPause = 8;
    [SerializeField] float demageToStarshipOnCollision = 10;
    [SerializeField] float demageToEnemyOnStarshipCollision = 90;
    [SerializeField] float demageToEnemyOnEnemyCollision = 100;
    [SerializeField] float demageFactorToEnemyOnAsteroidCollision = 2;
    [SerializeField] float demageFactorToEnemyOnPlanetCollision = 3;
    [SerializeField] Slider healthBar;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] AudioSource blasterAudio;
    [SerializeField] AudioSource explosionAudio;

    private Transform starship;
    private Collider enemyCollider;
    private Rigidbody rb;
    private bool isExploded = false;

    private float attackTimer;
    private float shootPause;

    private void OnEnable() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0].transform;
    }

    private void Start() {
        shootPause = Random.Range(attackPause - 2, attackPause + 2);
        attackTimer = shootPause / 2;
        rb = GetComponent<Rigidbody>();
        EnemiesManager.increaseEnemyCounter();
    }

    void Update() {

        Attack();

        if(Vector3.Distance(starship.position, this.transform.position) > 300){
            DestroyEnemy();
        }

        if (health <= 0 && !isExploded) {
            isExploded = true;
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
            healthBar.enabled = false;
            GetComponent<Outline>().enabled = false;
            StartCoroutine(Explode());
        }

        healthBar.value = Mathf.Lerp(healthBar.value, health, Time.deltaTime * 6);
    }

    public void ApplyDamage(float demage) {
        health -= demage;
    }

    IEnumerator Explode() {
        explosionParticles.Play();
        explosionAudio.Play();
        yield return new WaitForSeconds(1.1f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        DestroyEnemy();
    }

    private void DestroyEnemy(){
        EnemiesManager.decreaseEnemyCounter();
        Destroy(gameObject);
    }

    private void Attack() {
        if (attackTimer < shootPause + 1) {
            attackTimer += Time.deltaTime;
        }

        if (attackTimer > shootPause) {
            // Debug.Log("Searching");
            Vector3 directionToTarget = transform.position - starship.position;
            float angle = Vector3.Angle(-transform.forward, directionToTarget);
            float distance = directionToTarget.magnitude;

            if (Mathf.Abs(angle) < 30 && distance < 60) {
                // Debug.Log("Shoot");
                attackTimer = 0;
                blasterAudio.Play();
                StartCoroutine(Shoot(2));
            }
        }
    }

    IEnumerator Shoot(int numberOfBullets) {
        for (int i = 0; i < numberOfBullets; i++) {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Starship") {
            if (other.relativeVelocity.magnitude > 20) {
                Starship.ApplyDamage(demageToStarshipOnCollision);
                this.ApplyDamage(demageToEnemyOnStarshipCollision);
            }
        }

        if (other.gameObject.tag == "Asteroids") {
            if (other.relativeVelocity.magnitude > 20) {
                // Debug.Log("Enemy-Asteroid Collision: " + other.relativeVelocity.magnitude);
                ApplyDamage(demageFactorToEnemyOnAsteroidCollision * other.relativeVelocity.magnitude);
            }
        } else if (other.gameObject.tag == "Planet" || other.gameObject.tag == "Vesta" || other.gameObject.tag == "Mars") {
            // Debug.Log("Enemy-Planet Collision: " + other.relativeVelocity.magnitude);
            if (other.relativeVelocity.magnitude > 20) {
                ApplyDamage(demageFactorToEnemyOnPlanetCollision * other.relativeVelocity.magnitude);
            }
        }

        if (other.gameObject.tag == "Enemy") {
            // Debug.Log("Enemy-Enemy Collision: " + other.relativeVelocity.magnitude);
            if (other.relativeVelocity.magnitude > 57) {
                Debug.Log("Enemy-Enemy Destroy");
                ApplyDamage(100);
                other.gameObject.GetComponent<Enemy>().ApplyDamage(demageToEnemyOnEnemyCollision);
            }
        }
    }
}
