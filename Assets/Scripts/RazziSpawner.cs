using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazziSpawner : MonoBehaviour
{
    public Transform RazziPrefab;
    public EdgeCollider2D LakeEdge;
    public float SpawnRate = 2f;
    public float ToEdgeConst = 0.5f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            spawnRazzi();
        }
    }

    private void spawnRazzi()
    {
        var center = LakeEdge.bounds.center;
        var lake = LakeEdge.gameObject;
        
        
        var targetPoint = LakeEdge.points[Random.Range(0, LakeEdge.points.Length)];
        targetPoint *= lake.transform.lossyScale;
        targetPoint = new Vector2(targetPoint.x, targetPoint.y + lake.transform.position.y);
        
        
        
        
        var newRaz = Instantiate(RazziPrefab, transform, false);
        newRaz.localPosition = Vector3.zero;
        newRaz.localRotation = Quaternion.identity;

        // var direction = (targetPoint - new Vector2(0,0)).normalized;
        // newRaz.localPosition = targetPoint + direction * ToEdgeConst;

        newRaz.localPosition = targetPoint;
    }


}
