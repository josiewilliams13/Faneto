using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;

    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public bool isEnemy;
    public bool isPlayer;

    bool dead = false;

    public RectTransform healthBar;

    

    void Start(){
        //Debug.Log(this);
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;
        
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            } 
            else
            {
                currentHealth = maxHealth;
                dead = true;

                // called on the Server, will be invoked on the Clients
                RpcRespawn();
            }
            
        }
    }

    void OnChangeHealth (int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {        if (isLocalPlayer)
        {
            // Set the player’s position to origin
            transform.position = Vector3.zero;
        }
    }
}
