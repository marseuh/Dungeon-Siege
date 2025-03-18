using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    [SerializeField] private float _slowdown = 0.5f;

    [SerializeField] [Tag] private string _playerTag = "Player";
    [SerializeField] [Tag] private string _aiTag = "AI";
    private void OnTriggerEnter(Collider other)
    { 
        RiverInteraction riverInteractionComponent = other.GetComponent<RiverInteraction>();

        if (riverInteractionComponent != null)
        {
            if(other.gameObject.CompareTag(_playerTag))
                riverInteractionComponent.SlowPlayerSpeed(_slowdown);
            else if(other.gameObject.CompareTag(_aiTag))
                riverInteractionComponent.SlowAiSpeed(_slowdown);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RiverInteraction riverInteractionComponent = other.GetComponent<RiverInteraction>();
        if (riverInteractionComponent != null)
        {
            if (other.gameObject.CompareTag(_playerTag))
                riverInteractionComponent.RemoveSlowPlayerSpeed(_slowdown);
            else if (other.gameObject.CompareTag(_aiTag))
                riverInteractionComponent.RemoveSlowAiSpeed(_slowdown);
        }
    }
}
