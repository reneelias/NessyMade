
using System;
using DefaultNamespace;
using UnityEngine;


// TODO: Anim Events
// TODO: Trigger Collider



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
    private Animator anim;
    private SpawnDirection spawnDirection;
    private bool flashing;

    private const float CLOSE_ENOUGH = 0.1f;
    private const float OUT_OF_FILM_DESTROY = 2f;
    
    public enum RezziState { Idle, Moving, Flashing }
    public RezziState rezziState
    {
        protected set;
        get;
    } = RezziState.Idle;
    
    public void Initialize(
        Vector2 spawnPosition, 
        Vector2 destination, 
        float speed, 
        float flashRate, 
        int film,
        SpawnDirection spawnDirection)
    {
        startPosition = spawnPosition;
        endPosition = destination;
        this.speed = speed;
        this.flashRate = flashRate;
        this.film = film;
        this.spawnDirection = spawnDirection;

        anim = GetComponent<Animator>();
        transform.localPosition = spawnPosition;
        moving = true;

        if (spawnDirection == SpawnDirection.Right)
        {
            this.spawnDirection = SpawnDirection.Side;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (spawnDirection == SpawnDirection.Left)
        {
            this.spawnDirection = SpawnDirection.Side;
        }
        SetRezziState(RezziState.Moving);
    }

    // Move the razzi
    void Update()
    {
        if (flashing) return;
        
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
        
        SetRezziState(RezziState.Moving);
        move(startPosition);
    }

    void move(Vector2 destination)
    {
        var newPos = Vector2.Lerp(transform.localPosition, destination, Time.deltaTime * speed);
        transform.localPosition = newPos;
        var diff = ((Vector2)transform.localPosition - destination).magnitude;
        if (diff < CLOSE_ENOUGH)
        {
            moving = false;
            SetRezziState(RezziState.Idle);
        }
    }

    void flash()
    {
        flashing = true;
        Debug.Log($"FLASH");
        FlashEvent?.Invoke(this, new FlashEventArgs(this, 0));
        SetRezziState(RezziState.Flashing);
        film -= 1;

        if (film == 0)
        {
            Debug.Log($"OUT OF FILM");
            Destroy(gameObject, OUT_OF_FILM_DESTROY);
        }
    }

    public void SetRezziState(RezziState newState)
    {
        if (newState == rezziState) return;
        
        Debug.Log($"RezziState: {rezziState}");
        
        rezziState = newState;
        switch (rezziState)
        {
            case RezziState.Idle:
                anim.SetTrigger($"Idle{spawnDirection}");
                flashing = false;
                break;
            case RezziState.Moving:
                anim.SetTrigger($"Walk{spawnDirection}");
                flashing = false;
                break;
            case RezziState.Flashing:
                anim.SetTrigger($"Shoot{spawnDirection}");
                break;
        }
    }

    void OnDestroy()
    {
        RazziSpawner.RemovedRazzi();
    }
}
