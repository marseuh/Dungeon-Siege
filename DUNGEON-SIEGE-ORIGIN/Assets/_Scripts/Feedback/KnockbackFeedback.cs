using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private float _strengthMultiplier = 1.0f;

    private Rigidbody _rb;
    private ProtoHealth _protoHealth;
    private GameObject _player;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _protoHealth = GetComponent<ProtoHealth>();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    //private void OnEnable()
    //{
    //    _protoHealth._onHitEvent += PlayFeedback;
    //}
    //
    //private void OnDisable()
    //{
    //    _protoHealth._onHitEvent -= PlayFeedback;
    //}

    private void PlayFeedback( )
    {
        StopAllCoroutines();
        _rb.isKinematic = false;
        Vector3 direction = (transform.position - _player.transform.position).normalized;
        _rb.AddForce(direction * _strengthMultiplier, ForceMode.Impulse);
        StartCoroutine(COReset());
    }

    private IEnumerator COReset()
    {
        yield return new WaitForSeconds(0.1f);
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
    }
}
