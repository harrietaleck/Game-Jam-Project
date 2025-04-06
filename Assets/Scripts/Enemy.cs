using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    PlayerMovement playerMovement;

    [SerializeField] int enemyHealth = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.GetComponent<Enemy>() == null) return;
        if (other.gameObject.name != "Player") return;
        Debug.Log("Player has collided with the enemy!");
        int playerHealth = playerMovement.TakeDamage(enemyHealth);
        if (playerHealth > 0 )
        {
            // Destroy the Enemy Object
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
