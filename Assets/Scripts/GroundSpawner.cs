using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundTilePrefab; // Prefab for the ground tile
    [SerializeField] GameObject enemyPrefab; // Prefab for the enemy
    Vector3 nextSpawnPosition = Vector3.zero; // Initial spawn position
    int tileCount = 0; // Count the number of spawned tiles

    public void SpawnTile()
    {
        // Instantiate the ground tile prefab at the current position
        GameObject tempObj = Instantiate(groundTilePrefab, nextSpawnPosition, Quaternion.identity);

        // Update the next spawn position for the following tile
        nextSpawnPosition = tempObj.transform.GetChild(1).transform.position;

        tileCount++; // Increment the tile count

        // Pass whether this is the 20th tile to the GroundTile script
        if (tileCount == 20)
        {
            tempObj.GetComponent<GroundTile>().SpawnEnemy();
            resetTileCount();
        }
    }

    public void resetTileCount() // THis is a temporary function must be removed or replaced when you are ready to spawn safe house
    {
        tileCount = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++) // Spawn 10 ground tiles initially
        {
            SpawnTile();
        }
    }
}
