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
    public enum NessyState { Idle, Moving, Eating }
    public NessyState nessyState
    {
        protected set;
        get;
    } = NessyState.Idle;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public GameObject waterSprite;
    public int health = 100;
    [SerializeField] private ChargeBar chargeBar;
    [SerializeField] private float barrierOffset = 1f;
    public AudioClip eatingClip;
    public AudioClip eatingPersonClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        SetNessyState(NessyState.Idle);
        chargeBar.SetExactPercentage(1f);
        audioSource = GetComponent<AudioSource>();
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

        UpdateWaterSprite();
    }

    void UpdateIdle()
    {
        idleDT += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            SetNessyState(NessyState.Eating);
        }

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

    void UpdateWaterSprite()
    {
        waterSprite.transform.position = transform.position;
        waterSprite.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder + 1;
        Vector3 scale = waterSprite.transform.localScale;
        scale.x = movementDirection.x > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        waterSprite.transform.localScale = scale;
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
                Vector3 scale = transform.localScale;
                scale.x = movementDirection.x > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                transform.localScale = scale;
                break;

            case NessyState.Eating:
                animator.SetTrigger("Eating");
                break;
        }
    }

    public void ChangeNessyHealth(int deltaHealth, bool eatingHuman = false)
    {
        health = Mathf.Clamp(health + deltaHealth, 0, 100);
        chargeBar.SetExactPercentage(health / 100f);
        if (deltaHealth > 0)
        {
            SetNessyState(NessyState.Eating);
            if (eatingHuman)
                audioSource.PlayOneShot(eatingClip);
            else
                audioSource.PlayOneShot(eatingPersonClip);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Lake_OuterBounds")
        {
            print("Laker OuterBounds collision");
            // rb.MovePosition(-transform.position.normalized * 3f);
            rb.transform.position -= transform.position.normalized * barrierOffset;
        }
    }
}
