using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform bulletSpawn2;

    public AudioSource audioData;
    public AudioClip bang;

    public GameObject CC;

    public float currentSpeed;

    float counter;

    public override void OnStartLocalPlayer()
    {
        //bang = audioData.GetComponent<AudioClip>();
        audioData.clip = bang;

        Debug.Log(Camera.main);
        CC = Camera.main.transform.parent.gameObject;
        transform.parent = Camera.main.transform;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        

        PlayerMove();

        // var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        // var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        // transform.Rotate(0, x, 0);
        // transform.Translate(0, 0, z);

        if (Input.GetMouseButton(0))
        {
            if(counter < 0.25f){
                counter+=Time.deltaTime;
            }
            else{
                counter = 0;
                CmdFire();
            }
        }

        transform.localPosition = Vector3.zero;
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        var bullet2 = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn2.position,
            bulletSpawn2.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 10;
        bullet2.GetComponent<Rigidbody>().velocity = bullet.transform.up * 10;

        NetworkServer.Spawn(bullet);
        NetworkServer.Spawn(bullet2);

        // Destroy the bullet after 3 seconds
        Destroy(bullet, 3.0f);
        Destroy(bullet2, 3.0f);
        Debug.Log(audioData.clip);
        audioData.Play();
    }

    void PlayerMove()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {   Debug.Log(hit.transform);
            if (hit.transform.name == "Ground")
            {
                Vector3 points = Camera.main.transform.forward;
                points.y = 0;
                // CC.transform.position += points * currentSpeed * Time.deltaTime;
                CC.transform.Translate(points * currentSpeed * Time.deltaTime);
                CC.transform.position = new Vector3(CC.transform.position.x,1, CC.transform.position.z);
            }
        }
    }
}
