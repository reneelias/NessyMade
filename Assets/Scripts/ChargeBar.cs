using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChargeBar : MonoBehaviour
{
    public float FillPercent
    {
        get;
        set;
    } = 1f;

    // [SerializeField] private AudioSource progressAudioSource;
    [SerializeField] private GameObject innerBar;
    [SerializeField] private float fillSpeed = .5f;
    public bool stopsWhenFilled = true;
    public UnityEvent BarFilled = new UnityEvent();
    private float velocity = 0f;
    float xScale;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        xScale = FillPercent;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        UpdateBarWidth();
    }

    void UpdateBarWidth()
    {
        xScale = Mathf.SmoothDamp(xScale, FillPercent, ref velocity, fillSpeed);
        innerBar.transform.localScale = new Vector3(xScale, innerBar.transform.localScale.y, innerBar.transform.localScale.z);
    }

    public void AddToFillPercent(float addition){
        if(FillPercent == 1f && stopsWhenFilled){
            return;
        }
        // if (!progressAudioSource.isPlaying)
        // {
        //     progressAudioSource.Play();
        // }

        FillPercent = Mathf.Clamp(FillPercent + addition, 0f, 1f);

        if(1f - FillPercent <= .005f){
            BarFilled.Invoke();
            FillPercent = 1f;
        }

        if (FillPercent == 0)
        {
            gameManager.GameOver();
        }
    }

    public void SetExactPercentage(float percentage, bool setWidthImmediate = false)
    {
        FillPercent = percentage;


        if (1f - FillPercent <= .005f)
        {
            BarFilled.Invoke();
            FillPercent = 1f;
        }

        if (setWidthImmediate)
        {
            innerBar.transform.localScale = new Vector3(FillPercent, innerBar.transform.localScale.y, innerBar.transform.localScale.z);
        }

        if (FillPercent == 0)
        {
            gameManager.GameOver();
        }
    }
}
