using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour
{

    public GameObject EbulletPrefab;
    public Transform EbulletSpawn;

    public float speed;

    int r;
    int m;
    float yRotation;

    bool tracking;

    GameObject[] players;

    GameObject istracked;

    // Use this for initialization
    void Start()
    {
        yRotation = Mathf.PerlinNoise(transform.position.x, transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }

        move();
		checkOutOfBounds();
    }

    void CmdEFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            EbulletPrefab,
            EbulletSpawn.position,
            EbulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 5f;

        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 3.0f);
    }

    void move()
    {

        if (!tracking)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            yRotation += (Mathf.PerlinNoise(transform.position.x, transform.position.z) - 0.5f) * 7f;
            //Debug.Log(players[0].name);
            if (players.Length > 0)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    var d = players[i].transform.position - transform.position;
                    if (d.magnitude < 7f)
                    {
                        tracking = true;
                        istracked = players[i];
                        break;
                    }
					else{
						tracking = false;
					}
                }
            }

            transform.position += transform.forward * speed;
        }
        else
        {
            transform.LookAt(istracked.transform, transform.up);
            r = Random.Range(0, 1000);
            var d = istracked.transform.position - transform.position;

            if (d.magnitude > 5f)
            {
                transform.position += transform.forward * speed;
            }

            if (r % 100 == 0)
            {
                CmdEFire();
            }

        }
    }

    void checkOutOfBounds()
    {
        if (transform.position.z > 39f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 39f);
        }
        else if (transform.position.z < -39f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -39f);
        }

        else if (transform.position.x < -39f)
        {
            transform.position = new Vector3(-39f, transform.position.y, transform.position.z);
        }

        else if (transform.position.x > 39f)
        {
            transform.position = new Vector3(39f, transform.position.y, transform.position.z);
        }
    }

}
