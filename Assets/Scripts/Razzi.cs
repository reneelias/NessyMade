
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Razzi : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool moving;
    private float speed;

    private const float CLOSE_ENOUGH = 0.04f;
    
    public void Initialize(Vector2 destination, float speed)
    {
        startPosition = transform.localPosition;
        endPosition = destination;
        this.speed = speed;
        moving = true;
    }

    // Move the razzi
    void Update()
    {
        if (!moving) return;
        var newPos = Vector2.Lerp(transform.localPosition, endPosition, Time.deltaTime * speed);
        transform.localPosition = newPos;
        var diff = ((Vector2)transform.localPosition - endPosition).magnitude;
        if (diff < CLOSE_ENOUGH)
            moving = false;
    }
}
