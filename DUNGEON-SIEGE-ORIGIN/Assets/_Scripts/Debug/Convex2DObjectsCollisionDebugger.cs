using NaughtyAttributes;
using UnityEngine;

public class Convex2DObjectsCollisionDebugger : MonoBehaviour
{
    [BoxGroup("Constraint 0")]
    [OnValueChanged("OnMoveConstraint0AngleChanged")]
    [Range(0.0f, 360.0f)]
    [SerializeField] private float _constraint0Angle = 0.0f;
    [BoxGroup("Constraint 0")]
    [ReadOnly]
    [SerializeField] private Vector3 _constraint0 = Vector3.right;

    [BoxGroup("Constraint 1")]
    [OnValueChanged("OnMoveConstraint1AngleChanged")]
    [Range(0.0f, 360.0f)]
    [SerializeField] private float _constraint1Angle = 0.0f;
    [BoxGroup("Constraint 1")]
    [ReadOnly]
    [SerializeField] private Vector3 _constraint1 = Vector3.right;

    [BoxGroup("Move Direction")]
    [OnValueChanged("OnMoveDirectionAngleChanged")]
    [Range(0.0f, 360.0f)]
    [SerializeField] private float _moveDirectionAngle = 0.0f;
    [BoxGroup("Move Direction")]
    [ReadOnly]
    [SerializeField] private Vector3 _moveDirection = Vector3.right;
    [ReadOnly]
    [SerializeField] private Vector3 _constrainedMove = Vector3.right;

    [SerializeField] private float _smallVectorsMagnitude = 1.0f;
    [SerializeField] private float _bigVectorsMagnitude = 2.0f;

    private Color _moveDirectionColor = Color.green;

    private Vector3 _constraint0Right = Vector3.forward;
    private Vector3 _constraint0Fwd = Vector3.forward;
    private Vector3 _constraint0Left = Vector3.left;
    private Vector3 _constraint0Back = Vector3.back;

    private Vector3 _constraint1Right = Vector3.forward;
    private Vector3 _constraint1Fwd = Vector3.forward;
    private Vector3 _constraint1Left = Vector3.left;
    private Vector3 _constraint1Back = Vector3.back;

    private void OnMoveConstraint0AngleChanged()
    {
        OnAngleChanged(_constraint0Angle, ref _constraint0, ref _constraint0Right, ref _constraint0Fwd, ref _constraint0Left, ref _constraint0Back);
        _constrainedMove = _moveDirection;
        Convex2DObjectsCollision.ComputeConstrainedMove(ref _constrainedMove, _constraint0, _constraint1);
        ChooseColor();
    }
    
    private void OnMoveConstraint1AngleChanged()
    {
        OnAngleChanged(_constraint1Angle, ref _constraint1, ref _constraint1Right, ref _constraint1Fwd, ref _constraint1Left, ref _constraint1Back);
        _constrainedMove = _moveDirection;
        Convex2DObjectsCollision.ComputeConstrainedMove(ref _constrainedMove, _constraint0, _constraint1);
        ChooseColor();
    }
    
    private void OnMoveDirectionAngleChanged()
    {
        OnAngleChanged(_moveDirectionAngle, ref _moveDirection);
        _moveDirection = _smallVectorsMagnitude / _bigVectorsMagnitude * _moveDirection;
        _constrainedMove = _moveDirection;
        Convex2DObjectsCollision.ComputeConstrainedMove(ref _constrainedMove, _constraint0, _constraint1);
        ChooseColor();
    }

    private void ChooseColor()
    {
        float finalMagnitude = _constrainedMove.magnitude;
        if (finalMagnitude == 0)
        {
            _moveDirectionColor = Color.red;
        }
        else if (finalMagnitude == _moveDirection.magnitude)
        {
            _moveDirectionColor = Color.green;
        }
        else
        {
            _moveDirectionColor = Color.blue;
        }
    }

    private void OnAngleChanged(float newAngle, ref Vector3 newDirection, ref Vector3 newDirectionRight, ref Vector3 newDirectionFwd, ref Vector3 newDirectionLeft, ref Vector3 newDirectionBack)
    {
        Vector3[] vectorsToUpdate = { newDirectionRight, newDirectionFwd, newDirectionLeft, newDirectionBack };
        for (int i = 0; i < 4; ++i)
        {
            OnAngleChanged(newAngle, ref vectorsToUpdate[i]);
            newAngle += 90.0f;
        }

        newDirection = _smallVectorsMagnitude / _bigVectorsMagnitude * vectorsToUpdate[0];
        newDirectionRight = vectorsToUpdate[0];
        newDirectionFwd = vectorsToUpdate[1];
        newDirectionLeft = vectorsToUpdate[2];
        newDirectionBack = vectorsToUpdate[3];
    }

    private void OnAngleChanged(float newAngle, ref Vector3 newDirection)
    {
        newDirection.x = Mathf.Cos(Mathf.Deg2Rad * newAngle);
        newDirection.z = Mathf.Sin(Mathf.Deg2Rad * newAngle);
        newDirection *= _bigVectorsMagnitude;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position;

        Gizmos.color = Color.grey;
        Gizmos.DrawRay(origin, _constraint0Right);
        Gizmos.DrawRay(origin, _constraint0Left);

        Gizmos.DrawRay(origin, _constraint1Right);
        Gizmos.DrawRay(origin, _constraint1Left);

        float sin = Mathf.Sin(Mathf.Deg2Rad * Vector3.SignedAngle(_constraint0, _constraint1, Vector3.down));
        if (sin >= 0)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawRay(origin, _constraint0Back);
            Gizmos.DrawRay(origin, _constraint1Fwd);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(origin, _constraint0Fwd);
            Gizmos.DrawRay(origin, _constraint1Back);
        }
        else
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawRay(origin, _constraint0Fwd);
            Gizmos.DrawRay(origin, _constraint1Back);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(origin, _constraint0Back);
            Gizmos.DrawRay(origin, _constraint1Fwd);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, _constraint0);
        Gizmos.DrawRay(origin, _constraint1);

        Gizmos.color = _moveDirectionColor;
        Gizmos.DrawRay(origin, _moveDirection);
        Gizmos.DrawRay(origin, _constrainedMove);
    }
}
