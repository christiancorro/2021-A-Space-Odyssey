using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    [Range(0f, 100f)]
    public float maxOxygen = 100;
    [Range(0f, 100f)]
    public float maxFuel = 100;

    [Range(0f, 100f)]
    public float currentOxygen;
    [Range(0f, 100f)]
    public float currentFuel;

    public bool hasLava = false;
    public float lavaDamage = 0.1f;
    [Range(0f, 1f)]
    public float rotationSpeed = 0.2f;


    private Transform atmosphere;

    private GameObject starship;
    private Rigidbody starshipRigidbody;
    private float angle = 35;

    private ParticleSystem oxygenAndFuel;
    private AudioSource dockingAudio;

    private float timer = 0;

    void Start() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
        oxygenAndFuel = starship.transform.Find("Oxygen Collision").GetComponent<ParticleSystem>();
        atmosphere = transform.Find("Atmosphere").GetComponent<Transform>();
        starshipRigidbody = starship.GetComponent<Rigidbody>();
        dockingAudio = GetComponent<AudioSource>();
        dockingAudio.Stop();
        currentFuel = maxFuel;
        currentOxygen = maxOxygen;
    }

    void Update() {
        transform.Rotate(0, rotationSpeed, 0, Space.Self);
        if (timer < 30) {
            timer += Time.deltaTime;
        }

        if (timer >= 30) {
            if (currentOxygen < maxOxygen) {
                currentOxygen += Time.deltaTime * 10;
                atmosphere.localScale = Vector3.Lerp(atmosphere.localScale, new Vector3(1.26f, 1.26f, 1.26f), Time.deltaTime);
            }
            if (currentFuel < maxFuel) {
                currentFuel += Time.deltaTime * 10;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        dockingAudio.Play();
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Starship") {
            timer = 0;

            if (hasLava) {
                Starship.ApplyDamage(lavaDamage);
            }

            if (starshipRigidbody.velocity.magnitude < 5 && Vector3.Angle(-starship.transform.up, transform.position - starship.transform.position) < angle) {
                // Docking
                starshipRigidbody.velocity = Vector2.Lerp(starshipRigidbody.velocity, Vector3.zero, 2 * Time.deltaTime);

                if ((Starship.fuel < 100 || Starship.oxygen < 100 || Starship.health < 100) && (currentOxygen > 0 || currentFuel > 0)) {

                    oxygenAndFuel.Play();
                    if (dockingAudio != null) {
                        dockingAudio.UnPause();
                        dockingAudio.volume = 0.0178f;
                    }

                    if (currentOxygen > 0) {
                        Starship.oxygen += 8 * Time.deltaTime;
                        currentOxygen -= 8 * Time.deltaTime;
                        atmosphere.localScale = Vector3.Lerp(atmosphere.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 0.3f);
                    }
                    if (currentFuel > 0) {
                        Starship.fuel += 10 * Time.deltaTime;
                        currentFuel -= 10 * Time.deltaTime;
                    }

                    if (Starship.health < 100 && !hasLava) {
                        Starship.health += 8 * Time.deltaTime;
                    }

                } else {
                    oxygenAndFuel.Stop();
                    if (dockingAudio != null) {
                        dockingAudio.volume = 0f;
                        dockingAudio.Pause();
                    }
                }

            } else {
                Starship.ApplyDamage(0.23f);
                oxygenAndFuel.Pause();
                oxygenAndFuel.Stop();
                if (dockingAudio != null) {
                    dockingAudio.volume = 0f;

                }
            }
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Starship") {
            timer = 0;
            oxygenAndFuel.Stop();
            if (dockingAudio != null) {
                dockingAudio.Stop();
                dockingAudio.volume = 0f;
            }
        }
    }
}
