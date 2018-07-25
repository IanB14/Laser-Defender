using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

    private void OnDrawGizmos()
    {
        // Used to always show the location of this position object
        Gizmos.DrawWireSphere(this.transform.position, 1);
    }
}
