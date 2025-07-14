using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class RazziSpawner : MonoBehaviour
{
    public Transform RazziPrefab;
    public EdgeCollider2D LakeEdge;
    public float ToEdgeConst = 0.5f;
    [Range(0f, 2f)] public float FromEdgeSpawnMin;
    [Range(0f, 2f)] public float FromEdgeSpawnMax;
    [Range(0f, 2f)] public float RazziSpeedMin;
    [Range(0f, 2f)] public float RazziSpeedMax;
    [Range(1f, 5f)] public float FlashTimerMin;
    [Range(1f, 5f)] public float FlashTimerMax;
    [Range(1f, 5f)] public float FilmMin;
    [Range(1f, 5f)] public float FilmMax;

    public int MaxRazzies = 6;
    public bool AutoSpawn;
    public float SpawnRate = 2f;
    

    private Transform lake;
    private Vector2 lakeCenter;
    private float spawnTimer;
    private Queue<int> recentSpawns = new();
    private int razziCount = 0;
    
    private static RazziSpawner instance;

    private const int RECENT_SPAWN_MEMORY = 15;
    

    void Start()
    {
        instance = this;
        lake = LakeEdge.gameObject.transform;
        lakeCenter = new Vector2(0, 0);
    }

    void Update()
    {
        if (razziCount >= MaxRazzies) return;
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            spawnRazzi();
        }

        if (!AutoSpawn) return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= SpawnRate)
        {
            spawnTimer -= SpawnRate;
            spawnRazzi();
        }
    }

    private void spawnRazzi()
    {
        razziCount++;
        
        var idx = 0;
        while (recentSpawns.Contains(idx))
            idx = Random.Range(0, LakeEdge.points.Length);
        recentSpawns.Enqueue(idx);
        if (recentSpawns.Count > RECENT_SPAWN_MEMORY)
            recentSpawns.Dequeue();
        
        var lakeEdgeDestination = LakeEdge.points[idx];
        lakeEdgeDestination *= lake.transform.lossyScale;
        lakeEdgeDestination = new Vector2(lakeEdgeDestination.x + lake.position.x, lakeEdgeDestination.y + lake.position.y);

        var destinationPoint = lakeEdgeDestination;
        var direction = (lakeEdgeDestination - lakeCenter).normalized;
        var distance = (lakeEdgeDestination - lakeCenter).normalized.magnitude;
        distance += Random.Range(FromEdgeSpawnMin, FromEdgeSpawnMax);
        var spawnPoint = lakeEdgeDestination + direction * distance;

        SpawnDirection spawnDirection = SpawnDirection.Side;
        if (pointInUp(destinationPoint))
            spawnDirection = SpawnDirection.Up;
        else if (pointInRight(destinationPoint))
            spawnDirection = SpawnDirection.Right;
        else if (pointInDown(destinationPoint))
            spawnDirection = SpawnDirection.Down;
        else if (pointInLeft(destinationPoint))
            spawnDirection = SpawnDirection.Left;
        else
            Debug.Log("UNKNOWN SPAWN DIRECTION");
        
        Debug.Log($"SPAWN DIRECTION {spawnDirection}");

        var newRaz = Instantiate(RazziPrefab, transform, false).GetComponent<Razzi>();
        newRaz.Initialize(
            spawnPoint,
            destinationPoint,
            Random.Range(RazziSpeedMin, RazziSpeedMax),
            Random.Range(FlashTimerMin, FlashTimerMax),
            (int)Random.Range(FilmMin, FilmMax),
            spawnDirection);
    }

    bool pointInDown(Vector2 point)
    {
        return pointInTriangle(
            point,
            new Vector2(-5,5),
            new Vector2(5,5),
            new Vector2(0,0));
    }
    
    bool pointInRight(Vector2 point)
    {
        return pointInTriangle(
            point,
            new Vector2(5,5),
            new Vector2(5,-5),
            new Vector2(0,0));
    }
    
    bool pointInUp(Vector2 point)
    {
        return pointInTriangle(
            point,
            new Vector2(5,-5),
            new Vector2(-5,-5),
            new Vector2(0,0));
    }
    
    bool pointInLeft(Vector2 point)
    {
        return pointInTriangle(
            point,
            new Vector2(-5,-5),
            new Vector2(-5,5),
            new Vector2(0,0));
    }
    
    
    
    bool pointInTriangle (Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = sign(pt, v1, v2);
        d2 = sign(pt, v2, v3);
        d3 = sign(pt, v3, v1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }
    
    float sign (Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    public static void RemovedRazzi()
    {
        instance.razziCount--;
    }
    
}
