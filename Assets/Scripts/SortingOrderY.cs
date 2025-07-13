using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderY : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float orderScaler = 3f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() 
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * orderScaler);
    }
}
