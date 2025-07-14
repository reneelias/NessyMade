using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionCollider : MonoBehaviour
{
    public enum InteractionType{Razzi, Trash, Nessy, Fish, TrashCan, None}
    public InteractionType interactionType = InteractionType.Razzi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
