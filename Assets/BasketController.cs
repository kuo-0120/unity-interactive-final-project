using UnityEngine;

public class BasketController : MonoBehaviour
{
    [Header("九宮格移動設定")]
    public float cellSize = 2.0f;
    public float moveSpeed = 12.0f;

    [Header("舞台設定")]
    public Vector3 gridCenter = Vector3.zero;
    public float stagePlaneY = 0.0f;

    [Header("音效設定")]
    public AudioClip appleSE;
    public AudioClip bombSE;

    private AudioSource audioSource;
    private Vector3 targetPosition;
    private GameDirector gameDirector;

    void Start()
    {
        targetPosition = transform.position;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

#if UNITY_2023_1_OR_NEWER
        gameDirector = FindFirstObjectByType<GameDirector>();
#else
        gameDirector = FindObjectOfType<GameDirector>();
#endif

        if (gameDirector == null)
        {
            Debug.LogWarning("場景中找不到 GameDirector，分數不會更新，但籃子仍可移動與播放音效。");
        }
    }

    void Update()
    {
        if (gameDirector != null && gameDirector.IsGameOver)
        {
            return;
        }

        Vector2 inputPosition;
        bool hasInput = false;

        // iPhone 觸控
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            inputPosition = Input.GetTouch(0).position;
            hasInput = true;
        }
        // Unity 電腦測試用滑鼠
        else if (Input.GetMouseButtonDown(0))
        {
            inputPosition = Input.mousePosition;
            hasInput = true;
        }
        else
        {
            inputPosition = Vector2.zero;
        }

        if (hasInput)
        {
            MoveBasketByWorldPosition(inputPosition);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    void MoveBasketByWorldPosition(Vector2 screenPosition)
    {
        if (Camera.main == null)
        {
            Debug.LogWarning("找不到 Main Camera，請確認 Camera 的 Tag 是 MainCamera。");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        Plane stagePlane = new Plane(
            Vector3.up,
            new Vector3(0, stagePlaneY, 0)
        );

        if (stagePlane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);

            float localX = worldPoint.x - gridCenter.x;
            float localZ = worldPoint.z - gridCenter.z;

            int column = Mathf.RoundToInt(localX / cellSize);
            int row = Mathf.RoundToInt(localZ / cellSize);

            column = Mathf.Clamp(column, -1, 1);
            row = Mathf.Clamp(row, -1, 1);

            float targetX = gridCenter.x + column * cellSize;
            float targetZ = gridCenter.z + row * cellSize;

            targetPosition = new Vector3(
                targetX,
                transform.position.y,
                targetZ
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            Debug.Log("接到蘋果，加 100 分");

            if (appleSE != null)
            {
                audioSource.PlayOneShot(appleSE);
            }

            if (gameDirector != null)
            {
                gameDirector.CatchApple();
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bomb"))
        {
            Debug.Log("接到炸彈，分數減半");

            if (bombSE != null)
            {
                audioSource.PlayOneShot(bombSE);
            }

            if (gameDirector != null)
            {
                gameDirector.CatchBomb();
            }

            Destroy(other.gameObject);
        }
    }
}