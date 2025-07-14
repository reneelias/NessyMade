using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRazzi : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        transform.position = player.transform.position;
        spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder;
    }

    public void SetSpriteFlip(bool flip)
    {
        spriteRenderer.flipX = flip;
    }
}
