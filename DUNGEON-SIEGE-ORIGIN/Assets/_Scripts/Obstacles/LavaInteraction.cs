using System.Collections;
using UnityEngine;

public class LavaInteraction : MonoBehaviour
{
    private ICharacterHealth healthComponent;
    private Coroutine _runningCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<ICharacterHealth>();
    }

    public void StartDotCoroutine(int _damage, float _timeBetweenTwoTick)
    {
        _runningCoroutine = StartCoroutine(DealDamagePerSecond(_damage, _timeBetweenTwoTick));
    }

    public void StopDotCoroutine()
    {
        StopCoroutine(_runningCoroutine);
    }

    IEnumerator DealDamagePerSecond(int _damage, float _timeBetweenTwoTick)
    {
        while (true)
        {
            healthComponent.TakeDamage(_damage);
            yield return new WaitForSeconds(_timeBetweenTwoTick);
        }
    }
}
