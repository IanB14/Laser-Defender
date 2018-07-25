using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = 0.5f;

    float xMin = -5;
    float xMax = 5;

    public GameObject projectile;
    public float projectileSpeed = 5;

    public float fireRate = 0.2f;

    public float playerHealth = 250f;

    public AudioClip playerFire;


    private void Start()
    {
        // Distance between the camera and the playrt
        float zDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistance));

        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 1, 0);

        // 'as GameObject' is used because instantiate returns an object, so we need to cast it to a GameObject
        GameObject laserBeam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(playerFire, transform.position);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Initial, dirty code
            // transform.position += new Vector3(x: -speed * Time.deltaTime, y: 0, z: 0);

            // new code
            transform.position += speed * Time.deltaTime * Vector3.left;

        }else if(Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(x: speed * Time.deltaTime, y: 0, z: 0);
            transform.position += speed * Time.deltaTime * Vector3.right;
        }

        // Restrict the player to the playspace
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);



        // Quaternion.identity is used to make sure there's no rotation
        if (Input.GetKeyDown(KeyCode.Space)){
            // You have to call the method as a string in InvokeRepeating
            // Second value is to prevent a bug - needs to be greater than zero,
            // so it's set to 0.000001
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }





    void OnTriggerEnter2D(Collider2D laserHit)
    {
        // The Projectile component was added to the laser prefab. This method
        // checks to see if the laser has that component, then logs it to the console
        // on hit detection
        Projectile laser = laserHit.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            Debug.Log("You were hit!");
            playerHealth -= laser.GetDamage();
            laser.Hit();
            if (playerHealth <= 0)
            {
                Debug.Log("DED!");
                Destroy(gameObject);
            }
        }
    }

}
