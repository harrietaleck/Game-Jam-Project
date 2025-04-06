using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner; // Reference to the GroundSpawner script
    [SerializeField] GameObject obstaclePrefab; // Prefab for the obstacle

    [SerializeField] GameObject coinPrefab; // Prefab for the coin, {HealthCoin, SheinCoin, SpeedUpCoin, PointsCoin}

    [SerializeField] GameObject enemyPrefab;

    /*[SerializeField] GameObject treePrefab;// added to the inspector (to spawn as obstacles) 

    [SerializeField] GameObject bushPrefab; // added to the inspector (to spawn as obstacles)

    [SerializeField] GameObject batPrefab;*/ // added to the inspector (to spawn as obstacles)


    private bool enemySpawned = false;
    
    void Start()
    {
        groundSpawner = GameObject.FindAnyObjectByType<GroundSpawner>(); // Find the GroundSpawner script in the scene
        if (!enemySpawned) // Only spawn obstacles and coins if an enemy has not spawned yet
        {
            SpawnObstacle(); // Call the SpawnObstacle method to spawn an obstacle
            SpawnCoins(); // Call the SpawnCoins method to spawn coins
        }
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(); // Call the SpawnTile method in the GroundSpawner script when the player exits the trigger
        Destroy(gameObject, 2); // Destroy the ground tile after 2 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle ()
    {
        int randomSpawnIndex = Random.Range(2, 5); // Randomly select a spawn index for the obstacle
        Transform spawnPoint = transform.GetChild(randomSpawnIndex).transform; // Get the spawn point from the child of the ground tile
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform); // Instantiate the obstacle prefab at the spawn point
    }

    void SpawnCoins () {
        int coinsToSpawn = Random.Range(1, 4); // Randomly select the number of coins to spawn
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform); // Instantiate the coin prefab
            temp.transform.position = GetRandomCoinPointCollider(GetComponent<Collider>()); // Set the position of the coin to a random point within the collider
            PickUpCoin coinScript = temp.GetComponent<PickUpCoin>();
            if (coinScript != null) {
                int typeCount = System.Enum.GetValues(typeof(PickUpCoin.PickUpType)).Length;
                coinScript.pickUpType = (PickUpCoin.PickUpType)Random.Range(0, typeCount);
            }
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is not assigned!");
            return;
        }

        // Set enemy spawn position at the center of the tile
        Vector3 spawnPosition = transform.position + new Vector3(0f, 1f, 0f); // Positioning enemy 1 unit above the tile center

        // Instantiate the enemy at the center of the tile
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);

        // Set the flag to prevent further spawning of coins or obstacles
        enemySpawned = true;
    }

    Vector3 GetRandomCoinPointCollider (Collider collider)
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        ); // Generate a random point within the bounds of the collider
        if (randomPoint != collider.ClosestPoint(randomPoint)){
            randomPoint = GetRandomCoinPointCollider(collider); // Recursively call the method until a valid point is found
        }
        randomPoint.y = 1; // Set the Y coordinate to 1, matching it to the ground level
        return randomPoint;
    }
}
