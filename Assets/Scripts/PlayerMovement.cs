using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to use UI elements
using UnityEngine.SceneManagement; // Import the SceneManagement namespace to manage scenes
using System.Collections;
using TMPro; // Import the Collections namespace for IEnumerator

public class PlayerMovement: MonoBehaviour
{
    bool isAlive = true; // This is a flag to check if the player is alive
    [SerializeField] float speed = 5f; // This is the speed of the player
    [SerializeField] new Rigidbody rigidbody; // Reference to the Rigidbody component

    private int health = 0; // This is the health of the player
    private int level = 1;

    float horizontalInput;
    [SerializeField] float horizontalSpeedMultiplier = 1.5f; // Multiplier for horizontal speed

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI levelText;

    private void FixedUpdate()
    {
        if (!isAlive) return; // If the player is not alive, do not move

        Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMovement = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalSpeedMultiplier;
        rigidbody.MovePosition(rigidbody.position + forwardMovement + horizontalMovement);
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -5) // Check if the player falls below a certain height
        {
            KillPlayer(); // Call the KillPlayer method
        }
        
        // Quite the game if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key is pressed
        {
            Application.Quit(); // Quit the application
        }
    }


    public void KillPlayer()
    {

        isAlive = false; // Set the player to not alive
        // Restart the Game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public int TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            KillPlayer();
        } else
        {
            level++;
            //ciunt
            //obstacle(count),
            UpdateUI();
        }

        return health;
    }

    public void AddHealth(int healthToAdd)
    {
        health += healthToAdd; // Add health to the player's health
    }


    public void UpdateUI()
    {
        healthText.text = "Rock Power: " + health; // Update the health text
        levelText.text = "Level: " + level; // Update the level text
    }
}
