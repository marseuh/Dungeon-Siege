using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LogicSenderProjectile : MonoBehaviour
{
    [Tag]
    [SerializeField] private string _targetTag;
    [Tag]
    [SerializeField] private string _senderTag;
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string _hitableLayerName = "Hitable";

    private Rigidbody _rigidbody;
    private BaseAbilitySO _logicToSend;
    private AbilityBlackboard _dataToSend;

    public void Shoot(BaseAbilitySO logicToSend, AbilityBlackboard dataToSend, Vector3 direction, float speed)
    {
        _logicToSend = logicToSend;
        _dataToSend = dataToSend;
        _rigidbody.velocity = speed * direction;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Collision with ennemies with collision enter
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(_senderTag)) return;
        if (collision.transform.CompareTag(_targetTag))
        {
            _logicToSend.Use(ref _dataToSend);
        }
        Destroy(gameObject);
    }

    // Collision with ennemies shield with trigger enter
    private void OnTriggerEnter(Collider other)
    {
        SphereCollider collider = other.GetComponent<SphereCollider>();
        Debug.Log("SphereCollider Radius : " + collider.radius*other.transform.lossyScale.x);
        if (collider != null)
        {
            int layer = 1 << LayerMask.NameToLayer(_hitableLayerName);
            Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, collider.radius*other.transform.lossyScale.x, layer);
            Debug.Log("Length of Hitcollider : " + hitColliders.Length);
            if (hitColliders.Length == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
