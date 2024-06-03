using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClockManager : MonoBehaviour
{
    public static ClockManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    [SerializeField] private BoxCollider2D _arena;
    [SerializeField] private GameObject _clockPrefab;
    [SerializeField] private float _fallRadius;

    public void SpawnClock()
    {
        print("clock spawn");
        Vector3 spawnPoint = GetRandomSpawnPoint();
        Instantiate(_clockPrefab, spawnPoint, Quaternion.identity);
    }
    public void SpawnClock(Vector3 position)
    {
        print("clock fall");
        Vector3 spawnPoint = GetRandomFallPoint(position);
        Instantiate(_clockPrefab, spawnPoint, Quaternion.identity);
    }


    private Vector3 GetRandomSpawnPoint()
    {
        float x = Random.Range(_arena.bounds.min.x, _arena.bounds.max.x);
        float y = Random.Range(_arena.bounds.min.y, _arena.bounds.max.y);
        return new Vector3(x, y, -1);
    }

    private Vector3 GetRandomFallPoint(Vector3 center)
    {
        float randomx = Random.Range(_fallRadius * -1, _fallRadius);
        float randomy = Random.Range(_fallRadius * -1, _fallRadius);

        if (Mathf.Abs(randomx) <= 3)
        {
            randomx = randomx <= 0 ? randomx - 3 : randomx + 3;
        }

        if (Mathf.Abs(randomy) <= 3)
        {
            randomy = randomy <= 0 ? randomy - 3 : randomy + 3;
        }

        randomx = center.x + randomx < _arena.bounds.min.x ? _arena.bounds.min.x - center.x : randomx;
        randomx = center.x + randomx > _arena.bounds.max.x ? _arena.bounds.max.x - center.x : randomx;

        randomy = center.y + randomy < _arena.bounds.min.y ? _arena.bounds.min.y - center.y : randomy;
        randomy = center.y + randomy > _arena.bounds.max.y ? _arena.bounds.max.y - center.y : randomy;

        float x = center.x + randomx;
        float y = center.y + randomy;

        if (Mathf.Abs(x - center.x) <= 3)
        {
            x = center.x - randomx;
        }
        if (Mathf.Abs(y - center.y) <= 3)
        {
            y = center.y - randomy;
        }

        return new Vector3(x, y, -1);
    }
}
