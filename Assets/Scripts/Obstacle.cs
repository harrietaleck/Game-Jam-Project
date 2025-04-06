using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement; // Reference to the PlayerMovement script
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>(); // Find the PlayerMovement script in the scene   
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: This method will be used to kill the player, not being used at the moment
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
