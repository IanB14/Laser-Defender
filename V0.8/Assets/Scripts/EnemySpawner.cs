using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject Position;

    public float width = 10f;
    public float height = 5f;

    private bool moveRight = false;

    public float speed = 5f;

    private float xMax;
    private float xMin;

    public float spawnDelay = 0.3f;

	// Use this for initialization
	void Start () {
        // On startup, instanciate an enemy GameObject
        //GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;


        // Attaches the transform of the enemy GameObject to the EnemyFormation's transform
        // Doesn't appear inside the EnemySpawner hierarchy, but I think that's a unity 2017 problem
        // enemy.transform.parent = transform;



        // On startup, spawn the enemies
        RespawnAll();


        // Screen edge detection
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));

        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        xMax = rightEdge.x;
        xMin = leftEdge.x;



    }

    // draw a wirecube on the formation
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {

        // Note the two ways of moving the formation - both are valid (Vector3.right is a unit vector)
        // Delta time is to make sure the movement is framerate independant
        if (moveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }


        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
        {
            // If true, go false - If false, go true
            //moveRight = !moveRight;

            moveRight = true;
        }else if(rightEdgeOfFormation > xMax)
        {
            moveRight = false;
        }



        if (AllMembersDead())
        {
            Debug.Log("Empty Formation - Respawning");

            RespawnOneByOne();
        }
	}

    Transform NextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }

        return null;
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {

            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
            
        }
        return true;
    }



    void RespawnAll()
    {
        // Loop through every child inside the EnemyFormation game object.
        // Both of the lines above are used here to spawn an enemy on each position object
        foreach (Transform child in transform)
        {
            // Note the slight change to these lines: designed to add enemies under each position rather than on the EnemyFormation
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }


    void RespawnOneByOne()
    {
        Transform nextPosition = NextFreePosition();

        if (nextPosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, nextPosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = nextPosition;
        }
        if (NextFreePosition()) { 
            Invoke("RespawnOneByOne", spawnDelay);
        }

        if (nextPosition == null) { 
        CancelInvoke("RespawnOneByOne");
        }
    }
}
