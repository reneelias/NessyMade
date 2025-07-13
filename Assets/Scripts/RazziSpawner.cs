using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazziSpawner : MonoBehaviour
{
    public Transform RazziPrefab;
    public EdgeCollider2D LakeEdge;
    public float SpawnRate = 2f;
    public float ToEdgeConst = 0.5f;
    [Range(0f, 2f)] public float FromEdgeSpawnMin;
    [Range(0f, 2f)] public float FromEdgeSpawnMax;
    [Range(0f, 2f)] public float RazziSpeedMin;
    [Range(0f, 2f)] public float RazziSpeedMax;
    [Range(1f, 5f)] public float FlashTimerMin;
    [Range(1f, 5f)] public float FlashTimerMax;
    [Range(1f, 5f)] public float FilmMin;
    [Range(1f, 5f)] public float FilmMax;
    

    private Transform lake;
    private Vector2 lakeCenter;
    List<Razzi> razzies = new();
    

    void Start()
    {
        lake = LakeEdge.gameObject.transform;
        lakeCenter = new Vector2(0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            spawnRazzi();
        }
    }

    private void spawnRazzi()
    {
        var lakeEdgeDestination = LakeEdge.points[Random.Range(0, LakeEdge.points.Length)];
        lakeEdgeDestination *= lake.transform.lossyScale;
        lakeEdgeDestination = new Vector2(lakeEdgeDestination.x + lake.position.x, lakeEdgeDestination.y + lake.position.y);

        var destinationPoint = lakeEdgeDestination;
        var direction = (lakeEdgeDestination - lakeCenter).normalized;
        var distance = (lakeEdgeDestination - lakeCenter).normalized.magnitude;
        distance += Random.Range(FromEdgeSpawnMin, FromEdgeSpawnMax);
        var spawnPoint = lakeEdgeDestination + direction * distance;


        var newRaz = Instantiate(RazziPrefab, transform, false).GetComponent<Razzi>();
        newRaz.Initialize(
            spawnPoint,
            destinationPoint,
            Random.Range(RazziSpeedMin, RazziSpeedMax),
            Random.Range(FlashTimerMin, FlashTimerMax),
            (int)Random.Range(FilmMin, FilmMax));
        razzies.Add(newRaz);
    }


}
