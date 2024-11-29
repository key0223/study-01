using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform _target;

    public Vector3 _offset = new Vector3(0, 5, -10);
    public float _smoothSpeed = 0.125f;

    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(_target);
    }


}
