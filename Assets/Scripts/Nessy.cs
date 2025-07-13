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
    private float idleTimeMin = 1f;
    private float idleTimeMax = 3f;
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


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                break;
            case NessyState.Moving:
                break;
        }
    }

    public void SetNessyState(NessyState nS)
    {
        nessyState = nS;

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
                break;
        }
    }
}
