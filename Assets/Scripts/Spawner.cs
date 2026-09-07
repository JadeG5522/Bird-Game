using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public Transform spawnPoint;

    public Transform spawnPointStart;


    void Start()
    {
        int randomIndex = Random.Range(0, obstaclePrefab.Length);

        Instantiate(
            obstaclePrefab[randomIndex],
            spawnPoint.position,
            spawnPoint.rotation
        );

        int randomIndex2 = Random.Range(0, obstaclePrefab.Length);
        Instantiate(
            obstaclePrefab[randomIndex2],
            spawnPointStart.position,
            spawnPointStart.rotation
        );
    }

    void FixedUpdate()
    {
        if (ObstacleMovement.SpawnNextWall)
        {
            int randomIndex = Random.Range(0, obstaclePrefab.Length);

            Instantiate(
                obstaclePrefab[randomIndex],
                spawnPoint.position,
                spawnPoint.rotation
            );

            ObstacleMovement.SpawnNextWall = false;
        }
    }

}
