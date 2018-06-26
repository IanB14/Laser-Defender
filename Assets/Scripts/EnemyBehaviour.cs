using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject projectile;
    public float laserSpeed = -10f;
    public float health = 150f;
    public float shotsPerSecond = 0.5f;

    public int scoreValue = 150;
    private ScoreKeeper scoreKeeper;

    public AudioClip enemyFire;
    public AudioClip deathRattle;


    private void Start()
    {
        scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        float probability = shotsPerSecond * Time.deltaTime;

        if (Random.value < probability) { 
        Fire();
        }
    }


    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);

        // Again, note the 'as' keyword - instantiate returns an object, but we want to cast it to a GameObject
        GameObject enemyMissle = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        enemyMissle.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
        AudioSource.PlayClipAtPoint(enemyFire, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D laserHit)
    {
        // The Projectile component was added to the laser prefab. This method
        // checks to see if the laser has that component, then logs it to the console
        // on hit detection
        Projectile laser = laserHit.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            Debug.Log("You hit the fren");
            health -= laser.GetDamage();
            laser.Hit();
            if(health <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(deathRattle, transform.position);
                scoreKeeper.Score(scoreValue);
            }
        }
    }
}
