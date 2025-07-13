using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nessy : MonoBehaviour
{
    public float movementTimeMin = .25f;
    public float movementTimeMax = 2f;
    public float movementSpeedMin = 20f;
    public float movmentSpeedMax = 40f;
    private float movementTime = .25f;
    private float movementDT = 0f;
    private float movementSpeed = 20f;
    private Vector2 movementDirection = Vector2.zero;
    public float idleTimeMin = 1f;
    public float idleTimeMax = 3f;
    private float idleTime = 1f;
    private float idleDT = 0f;
    public enum NessyState { Idle, Moving }
    public NessyState nessyState
    {
        protected set;
        get;
    } = NessyState.Idle;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public GameObject waterSprite;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        SetNessyState(NessyState.Idle);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        switch (nessyState)
        {
            case NessyState.Idle:
                UpdateIdle();
                break;
            case NessyState.Moving:
                UpdateMoving();
                break;
        }


        waterSprite.transform.position = transform.position;
        waterSprite.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
        waterSprite.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder;
    }

    void UpdateIdle()
    {
        idleDT += Time.deltaTime;

        if (idleDT >= idleTime)
        {
            SetNessyState(NessyState.Moving);
        }
    }

    void UpdateMoving()
    {
        movementDT += Time.deltaTime;
        rb.AddForce(movementDirection * movementSpeed);

        if (movementDT >= movementTime)
        {
            SetNessyState(NessyState.Idle);
        }
    }

    public void SetNessyState(NessyState nS)
    {
        nessyState = nS;

        print($"NessyState: {nessyState.ToString()}");

        switch (nessyState)
        {
            case NessyState.Idle:
                animator.SetTrigger("Idle");
                idleTime = Random.Range(idleTimeMin, idleTimeMax);
                idleDT = 0f;
                break;
                
            case NessyState.Moving:
                animator.SetTrigger("Idle");
                movementTime = Random.Range(movementTimeMax, movementTimeMax);
                movementDT = 0f;
                float moveAngle = Random.Range(0f, Mathf.PI * 2f);
                movementDirection = new Vector2(Mathf.Cos(moveAngle), Mathf.Sin(moveAngle));
                movementSpeed = Random.Range(movementSpeedMin, movmentSpeedMax);
                spriteRenderer.flipX = movementDirection.x > 0;
                break;
        }
    }
}
