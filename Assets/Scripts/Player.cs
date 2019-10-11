using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    // Configuration Parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.2f;


    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax;
    float rotateAngel = 0;
    float deltaZ = 0;
    float timer = 0;


    // Start is called before the first frame update
    void Start() {
        SetUpMoveBoundaries();

        if (SceneManager.GetActiveScene().buildIndex == 20 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            var newPos = transform.position;
            GameObject laser = Instantiate(laserPrefab, newPos, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
        }
    }

    // Update is called once per frame
    void Update() {
        Move();
        Fire();


    }
    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }

    public int GetHealth() {
        return health;
    }

    public void MoreHealth() {
        health += 1000;
    }


    private void Fire() {

        if (SceneManager.GetActiveScene().buildIndex != 8 && SceneManager.GetActiveScene().buildIndex != 20)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);
            }
        }
        

    }

    IEnumerator FireContinuously() {


        while (true) {
            timer += Time.deltaTime * moveSpeed * 2;

           
            //Debug.LogError(timer);

            if (SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 16) {



                var newPos = transform.position;
           

                GameObject laser = Instantiate(laserPrefab, newPos, Quaternion.Euler(0, 0, timer)) as GameObject;
                GameObject laser1 = Instantiate(laserPrefab, newPos, Quaternion.Euler(0, 0, timer)) as GameObject;
                GameObject laser2 = Instantiate(laserPrefab, newPos, Quaternion.Euler(0, 0, timer)) as GameObject;
                GameObject laser3 = Instantiate(laserPrefab, newPos, Quaternion.Euler(0, 0, timer)) as GameObject;
                GameObject laser4 = Instantiate(laserPrefab, newPos, Quaternion.Euler(0, 0, rotateAngel)) as GameObject;


                laser.GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Sin((timer+rotateAngel+90) * Math.PI / 180) * projectileSpeed, (float)Math.Cos(timer * Math.PI / 18) * projectileSpeed);
                laser1.GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Sin((-timer + rotateAngel-90) * Math.PI / 180) * projectileSpeed, (float)Math.Cos(timer * Math.PI / 18) * projectileSpeed);
                laser2.GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Sin((-timer + rotateAngel-90) * Math.PI / 180) * projectileSpeed, (float)Math.Cos(timer * Math.PI / 18) * -projectileSpeed);
                laser3.GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Sin((timer + rotateAngel+90) * Math.PI / 180) * projectileSpeed, (float)Math.Cos(timer * Math.PI / 18) *-projectileSpeed);
                laser4.GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Sin(-rotateAngel * Math.PI / 180) * projectileSpeed, (float)Math.Cos(rotateAngel * Math.PI / 180) * projectileSpeed);

                AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
                yield return new WaitForSeconds(projectileFiringPeriod);

            } else if (SceneManager.GetActiveScene().buildIndex == 200) {

                var newPos = transform.position;

                GameObject laser = Instantiate(laserPrefab, newPos, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
                //yield return new WaitForSeconds(projectileFiringPeriod);
                //break;
                yield break;



            }
            else {
                var newPos = transform.position;
                GameObject laser = Instantiate(laserPrefab, newPos, Quaternion.Euler(0, 0, rotateAngel)) as GameObject;

                laser.GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Sin(-rotateAngel * Math.PI / 180) * projectileSpeed, (float)Math.Cos(rotateAngel * Math.PI / 180) * projectileSpeed);

                AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }


        }
    }


    private void Move() {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        deltaZ = Input.GetAxis("HorizontalRotate") * Time.deltaTime * moveSpeed * 10;
        rotateAngel += deltaZ;

        if(rotateAngel > 360)
        {
            rotateAngel = rotateAngel - 360;
        } else if(rotateAngel < -360)
        {
            rotateAngel = 360 + rotateAngel;
        }


        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        transform.position = new Vector2(newXPos, newYPos);
        transform.rotation = Quaternion.Euler(0, 0, rotateAngel);

    }

    private void SetUpMoveBoundaries() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
