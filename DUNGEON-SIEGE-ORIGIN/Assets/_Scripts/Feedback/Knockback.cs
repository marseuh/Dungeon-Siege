using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void DoKnockback (Vector3 direction, float strengthMultiplier)
    {
        //StopCoroutine(COResetVelocity());
        _rb.isKinematic = false;
        _rb.AddForce(direction * strengthMultiplier, ForceMode.Impulse);
        StartCoroutine(COResetVelocity());

    }

    public IEnumerator COResetVelocity()
    {
        yield return new WaitForSeconds(0.1f);
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
    }

}
