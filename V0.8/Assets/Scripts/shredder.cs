using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shredder : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collisionDetected)
    {
        // Note the lowercase g
        Destroy(collisionDetected.gameObject);
    }
}
