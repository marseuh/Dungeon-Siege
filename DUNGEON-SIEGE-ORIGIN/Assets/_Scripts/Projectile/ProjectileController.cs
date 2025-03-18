using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _damages;
    
    public void Launch(Vector3 destination, float damages, float speed, float lifeTime)
    {
        _damages = damages;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = speed * (destination - transform.position).normalized;
        StartCoroutine(HandleLifeTime(lifeTime));
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        transform.position = new Vector3(1000f, 1000f, 1000f);
        gameObject.SetActive(false);
    }

    IEnumerator HandleLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ProjectilePool.Instance.ClearOneProjectile(this.gameObject);
        yield return null;
    }

    private void OnCollisionEnter(Collision other)
    {
        ProjectilePool.Instance.ClearOneProjectile(this.gameObject);
        if (other.gameObject.TryGetComponent<ICharacterHealth>(out var health))
        {
            health.TakeDamage(_damages);
        }
    }
}
