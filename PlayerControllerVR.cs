using UnityEngine;
using UnityEngine.Networking;


public class PlayerControllerVR : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public GameObject CC;
    public Transform bulletSpawn;


    public float currentSpeed = 5.0f;


     public override void OnStartLocalPlayer(){

        Debug.Log(Camera.main);
        //CC = Camera.main.transform.parent.gameObject;
        transform.parent = Camera.main.transform;
        transform.position = Vector3.zero;


    //GetComponent<Renderer>().material.color = Color.blue;
        
    }

    void Update()
    
    {

        if (!isLocalPlayer)
        {
            return;
        }

         RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        { 
            if (hit.transform.name == "Ground")
            {
                Vector3 points = Camera.main.transform.forward;
                points.y= 0;
                CC.transform.position += points * currentSpeed * Time.deltaTime;
                
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    }
    
    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

}
