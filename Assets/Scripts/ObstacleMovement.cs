using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public int speed = 0;

    public static bool SpawnNextWall = false;
    private static int previousColorIndex = -1;

    void Start()
    {
        Color[] colors =
        {
            new Color(51f / 255f, 115f / 255f, 247f / 255f),
            new Color(209f / 255f, 56f / 255f, 79f / 255f),
            new Color(117f / 255f, 209f / 255f, 56f / 255f),
            new Color(209f / 255f, 146f / 255f, 56f / 255f),
            new Color(143f / 255f, 82f / 255f, 227f / 255f),
        };

        int randomColorIndex =
            Random.Range(0, colors.Length);

        while (randomColorIndex == previousColorIndex)
        {
            randomColorIndex =
                Random.Range(0, colors.Length);
        }

        previousColorIndex = randomColorIndex;
        Color randomColor = colors[randomColorIndex];

        Renderer[] wallRenderers =
            GetComponentsInChildren<Renderer>();

        foreach (Renderer wallRenderer in wallRenderers)
        {
            wallRenderer.material.color = randomColor;
        }
    }


    void FixedUpdate()
    {
        if (!Bird.gameStarted)
        {
            return;
        }

        Vector3 movement = 
            new Vector3(0.0f, 0.0f, -speed) * Time.deltaTime;

        transform.position += movement;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            Debug.Log("Wall Destroyed");
        }
        if (other.CompareTag("Respawn"))
        {
            SpawnNextWall = true;
        }
    }
}
