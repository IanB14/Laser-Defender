using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthKeeper : MonoBehaviour {

    public static float health = 5;
    private Text HP;

	// Use this for initialization
	void Start () {
        HP = GetComponent<Text>();
        ResetHealth();
	}

    public void DecrementHealth(float damage)
    {
        Debug.Log("Hit: Health - " + damage);
        health -= damage;
        HP.text = "HP: " + health.ToString();
    }

    public static void ResetHealth()
    {
        health = 5;
    }
}
