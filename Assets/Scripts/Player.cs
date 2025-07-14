using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected float speed = 5.0f;
    private Rigidbody2D rb;
    public enum PlayerState { Idle, FishCarry, TrashCarry, RazziCarry, FishFeed, TrashToss, RazziFeed }
    public PlayerState playerState
    {
        protected set;
        get;
    } = PlayerState.Idle;
    private int trashAmount = 0;
    private int fishAmount = 0;
    [SerializeField] protected BoatFish boatFish;
    [SerializeField] protected BoatTrash boatTrash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                MovementControls();
                break;
            case PlayerState.FishCarry:
                MovementControls();
                break;
            case PlayerState.TrashCarry:
                MovementControls();
                break;
            case PlayerState.RazziCarry:
                MovementControls();
                break;
            case PlayerState.FishFeed:
                break;
            case PlayerState.RazziFeed:
                break;
            case PlayerState.TrashToss:
                break;
        }

        UpdateDirection();
    }

    void Update()
    {
        UpdateFishCount();
        UpdateTrashCount();
    }

    void UpdateDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = rb.velocity.x < 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
        boatFish.SetSpriteFlip(rb.velocity.x < 0);
        boatTrash.SetSpriteFlip(rb.velocity.x < 0);
    }

    void UpdateFishCount()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (--fishAmount <= 0)
            {
                fishAmount = 0;
                SetPlayerState(PlayerState.Idle);
            }
            boatFish.SetAmount(fishAmount);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (++fishAmount > 4)
            {
                fishAmount = 4;
            }
            if (playerState != PlayerState.FishCarry)
            {
                SetPlayerState(PlayerState.Idle);
            }

            boatFish.SetAmount(fishAmount);
        }
    }
    void UpdateTrashCount()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (--trashAmount <= 0)
            {
                trashAmount = 0;
                SetPlayerState(PlayerState.Idle);
            }
            boatTrash.SetAmount(trashAmount);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (++trashAmount > 4)
            {
                trashAmount = 4;
            }
            if (playerState != PlayerState.TrashCarry)
            {
                SetPlayerState(PlayerState.Idle);
            }

            boatTrash.SetAmount(trashAmount);
        }
    }

    void MovementControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed * Time.deltaTime);
        }
    }

    public void SetPlayerState(PlayerState ps) {
        playerState = ps;
    }
}
