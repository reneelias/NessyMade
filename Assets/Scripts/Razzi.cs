
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FlashEventArgs : EventArgs
{
    public Razzi Razzi;
    public float DistanceFromNessy;
    public FlashEventArgs(Razzi razzi, float distanceFromNessy)
    {
        Razzi = razzi;
        DistanceFromNessy = distanceFromNessy;
    }
}

public class Razzi : MonoBehaviour
{
    public Transform FlashPrefab;
    public EventHandler<FlashEventArgs> FlashEvent;
    
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool moving;
    private float speed;
    private float flashRate;
    private float flashTimer;
    private int film;

    private const float CLOSE_ENOUGH = 0.1f;
    private const float OUT_OF_FILM_DESTROY = 2f;
    
    public void Initialize(Vector2 spawnPosition, Vector2 destination, float speed, float flashRate, int film)
    {
        startPosition = spawnPosition;
        endPosition = destination;
        this.speed = speed;
        this.flashRate = flashRate;
        this.film = film;

        transform.localPosition = spawnPosition;
        moving = true;
    }

    // Move the razzi
    void Update()
    {
        if (moving)
        {
            move(endPosition);
            return;
        }

        if (film > 0)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= flashRate)
            {
                flashTimer -= flashRate;
                flash();
            }

            return;
        }
        
        move(startPosition);
    }

    void move(Vector2 destination)
    {
        var newPos = Vector2.Lerp(transform.localPosition, destination, Time.deltaTime * speed);
        transform.localPosition = newPos;
        var diff = ((Vector2)transform.localPosition - destination).magnitude;
        if (diff < CLOSE_ENOUGH)
            moving = false;
    }

    void flash()
    {
        Debug.Log($"FLASH");
        FlashEvent?.Invoke(this, new FlashEventArgs(this, 0));
        var flash = Instantiate(FlashPrefab, transform, false);
        flash.transform.localPosition = Vector3.zero;
        flash.transform.localRotation = Quaternion.identity;
        Destroy(flash.gameObject, 0.5f);
        film -= 1;

        if (film == 0)
        {
            Debug.Log($"OUT OF FILM");
            Destroy(gameObject, OUT_OF_FILM_DESTROY);
        }
    }
}
