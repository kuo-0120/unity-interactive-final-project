using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;

    public float interval = 1.0f;
    public float bombRate = 0.3f;

    private float timer = 0;
    private GameDirector gameDirector;

    void Start()
    {
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    void Update()
    {
        if (gameDirector != null && gameDirector.IsGameOver)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0;
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        float x = Random.Range(-1, 2);
        float z = Random.Range(-1, 2);

        Vector3 spawnPosition = new Vector3(x, 5.0f, z);

        float randomValue = Random.value;

        if (randomValue < bombRate)
        {
            Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(applePrefab, spawnPosition, Quaternion.identity);
        }
    }
}