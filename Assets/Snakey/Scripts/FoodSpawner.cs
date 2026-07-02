using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Prefabs & References")]
    public GameObject FoodPrefab;
    public SnakeMovement SnakeInstance;

    [Header("Spawn Settings")]
    public float XLimit = 8f;
    public float ZLimit = 8f;
    public float MinSpawnTime = 1f;
    public float MaxSpawnTime = 4f;
    public float SafeRadius = 1.0f; 

    private GameObject _currentFood;
    private bool _isWaitingToSpawn = false;

    void Start()
    {
        StartCoroutine(SpawnFoodRoutine());
    }

    void Update()
    {
        if (_currentFood == null && !_isWaitingToSpawn)
        {
            StartCoroutine(SpawnFoodRoutine());
        }
    }

    IEnumerator SpawnFoodRoutine()
    {
        _isWaitingToSpawn = true;

        // 1. הגרלת זמן המתנה רנדומלי
        float waitTime = Random.Range(MinSpawnTime, MaxSpawnTime);
        yield return new WaitForSeconds(waitTime);

        // 2. הגרלת מיקום בטוח שאינו על הנחש
        Vector3 spawnPosition = GetSafeSpawnPosition();

        // 3. יצירת האוכל במרחב
        _currentFood = Instantiate(FoodPrefab, spawnPosition, Quaternion.identity);

        _isWaitingToSpawn = false;
    }

    Vector3 GetSafeSpawnPosition()
    {
        Vector3 potentialPosition = Vector3.zero;
        bool isPositionSafe = false;
        int attempts = 0;

        while (!isPositionSafe && attempts < 100)
        {
            attempts++;
            float randomX = Mathf.Round(Random.Range(-XLimit, XLimit));
            float randomZ = Mathf.Round(Random.Range(-ZLimit, ZLimit));
            potentialPosition = new Vector3(randomX, 0.5f, randomZ);
            isPositionSafe = true;
            
            var segments = SnakeInstance?.Segments;
            if (segments == null)
            {
                break;
            }

            foreach (Transform segment in segments)
            {
                if (Vector3.Distance(potentialPosition, segment.position) < SafeRadius)
                {
                    isPositionSafe = false;
                    break;
                }
            }
        }

        return potentialPosition;
    }
}