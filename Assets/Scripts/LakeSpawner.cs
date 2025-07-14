using UnityEngine;

public class LakeSpawner : MonoBehaviour
{

    public Transform SpawnPrefab;
    public KeyCode InputKey;
    public Transform CenterSpawnPoint;
    
    [Range(0f, 3f)] public float DistanceMin;
    [Range(0f, 3f)] public float DistanceMax;
    
    public bool AutoSpawn;
    public float SpawnRate = 2f;
    private float spawnTimer;
    public Nessy nessy;
    
    void Update()
    {
        if (!AutoSpawn) return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= SpawnRate)
        {
            spawnTimer -= (SpawnRate + Random.Range(0f, 1f));
            spawn();
        }
    }

    private void spawn()
    {
        var direction = Random.insideUnitCircle.normalized;

        var distance = Random.Range(DistanceMin, DistanceMax);
        var spawnPoint = (Vector2)CenterSpawnPoint.position + distance * direction;
        var newSpawn = Instantiate(SpawnPrefab, CenterSpawnPoint, false);
        newSpawn.localPosition = spawnPoint;
        if (nessy != null)
        {
            nessy.ChangeNessyHealth(-5);
        }
    }
}
