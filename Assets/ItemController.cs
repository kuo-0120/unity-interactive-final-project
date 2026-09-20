using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float dropSpeed = 4.0f;

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

        transform.Translate(0, -dropSpeed * Time.deltaTime, 0);

        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}