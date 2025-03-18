using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class DeathScaleToZero : MonoBehaviour
{
    [BoxGroup("Broadcast to")]
    [SerializeField] private GOSenderEventChannelSO _destroyObjectChannel;

    [SerializeField] private float _timeForScaling;
    private CharacterHealth _characterHealth;

    void Awake()
    {
        _characterHealth = GetComponent<CharacterHealth>();
    }

    private void OnEnable()
    {
        _characterHealth.OnDeathEvent += StartScalingCoroutine;
    }

    private void OnDisable()
    {
        _characterHealth.OnDeathEvent -= StartScalingCoroutine;
    }

    private void StartScalingCoroutine()
    {
        StartCoroutine(ChangeCharacterScaleToZero());
    }

    private IEnumerator ChangeCharacterScaleToZero()
    {
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;
        for (float i = 0; i <= _timeForScaling; i += Time.fixedDeltaTime)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.fixedDeltaTime * scaleX / _timeForScaling,
                                                    transform.localScale.y - Time.fixedDeltaTime * scaleY / _timeForScaling,
                                                    transform.localScale.z - Time.fixedDeltaTime * scaleZ / _timeForScaling);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        _destroyObjectChannel.RequestRaiseEvent(gameObject);
    }
}
