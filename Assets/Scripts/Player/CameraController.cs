using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 _deadZone = Vector2.zero;
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        Vector2 posDifference = (transform.position - _cameraTransform.position);
        if (posDifference.x > _deadZone.x) _cameraTransform.position = new Vector3
                (_cameraTransform.position.x + posDifference.x - _deadZone.x, _cameraTransform.position.y, _cameraTransform.position.z);
        if (posDifference.x < -_deadZone.x) _cameraTransform.position = new Vector3
                (_cameraTransform.position.x + posDifference.x + _deadZone.x, _cameraTransform.position.y, _cameraTransform.position.z);

        if (posDifference.y > _deadZone.y) _cameraTransform.position = new Vector3
                (_cameraTransform.position.x  , _cameraTransform.position.y + posDifference.y - _deadZone.y, _cameraTransform.position.z);
        if (posDifference.y <  -_deadZone.y) _cameraTransform.position = new Vector3
                (_cameraTransform.position.x  , _cameraTransform.position.y + posDifference.y + _deadZone.y, _cameraTransform.position.z);
    }
}
