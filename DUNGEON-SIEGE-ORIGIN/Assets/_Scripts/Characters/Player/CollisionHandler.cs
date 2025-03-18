using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CollisionHandler : MonoBehaviour
{
    [Layer]
    [SerializeField] private int layerToCollideWith;

    private Dictionary<Collider, Vector3> _contactNormalByColliderHitMap = new();

    public void AlterMovement(ref Vector3 movement)
    {
        int nbConstraints = _contactNormalByColliderHitMap.Count;
        if (nbConstraints == 1 || nbConstraints == 2)
        {
            Dictionary<Collider, Vector3>.Enumerator enumerator = _contactNormalByColliderHitMap.GetEnumerator();
            enumerator.MoveNext();
            Vector3 firstConstraint = enumerator.Current.Value;

            if (nbConstraints == 1)
            {
                Convex2DObjectsCollision.ComputeConstrainedMove(ref movement, firstConstraint);
            }
            else
            {
                enumerator.MoveNext();
                Vector3 secondConstraint = enumerator.Current.Value;
                Convex2DObjectsCollision.ComputeConstrainedMove(ref movement, firstConstraint, secondConstraint);
            }
        }
        else if (nbConstraints > 2)
        {
            Debug.LogWarning("MOVE : unhandled constraint state, more than 2 constraints applied on move");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != layerToCollideWith) return;

        Vector3 contactNormal = ComputeContactNormalMean(collision);
        if (_contactNormalByColliderHitMap.TryAdd(collision.collider, contactNormal))
        {
            return;
        }

        Debug.LogWarning("ENTER : Trying to add already colliding collider" + contactNormal);
    }

    private void OnCollisionExit(Collision collision)
    {
        _contactNormalByColliderHitMap.Remove(collision.collider);
    }

    private Vector3 ComputeContactNormalMean(Collision collision)
    {
        ContactPoint[] contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);

        Vector3 normalMean = Vector3.zero;
        foreach (ContactPoint contact in contacts)
        {
            normalMean += contact.normal;
        }

        normalMean /= collision.contactCount;
        return normalMean;
    }
}