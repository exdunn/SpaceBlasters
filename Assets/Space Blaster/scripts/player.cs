﻿using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public int lives = 1;
	public int health = 3;
	public GameObject[] spawner;
    public int playerNum;
    public int kills = 0;

    // Use this for initialization
    public void Start()
    {
        spawner = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
		projectileDamage P = col.GetComponent<projectileDamage> ();
		if (col.tag == "Instant Death")
        {
			health = 0;
		}
		else if (P != null && P.shooter != this.transform)
        {
            takeDamage(col.GetComponent<projectileDamage>().damage);
            if (health <= 0)
            {
                col.GetComponent<projectileDamage>().shooter.GetComponent<player>().kills += 1;
            }
        }
	}

	// Update is called once per frame
	void Update () 
	{
		if (health <= 0){
			health = 0;
			respawn();
		}
	}

	private void takeDamage(int dmg)
	{
		health -= dmg;
		if (health < 0) {
			health = 0;
		}
	}

    private void respawn()
    {
        if (optionsMenu.gameMode == "Stock" && lives <= 0)
        {
            gameTracker.playersDead += 1;
            Destroy(gameObject);
        }
        else
        {
            lives -= 1;
            health = 3;
            transform.position = spawner[playerNum].transform.position;
        }
    }
}
