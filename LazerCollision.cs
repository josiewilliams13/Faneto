using UnityEngine;
using System.Collections;

public class LazerCollision : MonoBehaviour
{
    public int dmg;

    public bool baddy;

     void OnTriggerEnter(Collider collision)
    {

       // Debug.Log("HIT");
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        var name = hit.tag;

       // Debug.Log(name);
        //Debug.Log(hit);
        //Debug.Log(health);

        //ENEMY FIRE
        if (hit.CompareTag("Player") && baddy)
        {   Debug.Log("TESTTTTT");
            if (health != null && !health.isEnemy)
            {
                Debug.Log("HIT ENEMY");
                health.TakeDamage(dmg);
            }

        }
        
        //PLAYER FIRE
        if (hit.CompareTag("Enemy") && !baddy)
        {   //Debug.Log(health);
            //Debug.Log(health.isEnemy);
            if (health != null && health.isEnemy)
            {
                Debug.Log("HIT PLAYER");
                health.TakeDamage(dmg);
            }

        }


        Destroy(gameObject);
    }
}
