using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTrash : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        transform.position = player.transform.position;
        spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder;
    }

    public void SetAmount(int amt)
    {
        if (amt > 0)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = sprites[amt - 1];
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    public void SetSpriteFlip(bool flip)
    {
        spriteRenderer.flipX = flip;
    }
}
