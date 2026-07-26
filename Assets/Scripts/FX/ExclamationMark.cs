using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    private Transform _cameraTransform;
    private Vector3 _initialPos;

    private void OnEnable()
    {
        _cameraTransform = FindAnyObjectByType<Camera>().transform;
        _initialPos = transform.position;
    }

    private void LateUpdate()
    {
        Vector2 dif = _initialPos - _cameraTransform.position;
        float distance = dif.magnitude;
        Vector2 direction = dif.normalized;
        float alpha = Mathf.Atan2(direction.y, direction.x);

        float newDist = Mathf.Min(distance , Mathf.Sqrt(Mathf.Pow(Mathf.Cos(alpha)*8.5f,2) + Mathf.Pow(Mathf.Sin(alpha)*4.25f,2)) );

        transform.position = new Vector3(_cameraTransform.position.x + newDist * direction.x, _cameraTransform.position.y + newDist * direction.y, 0f);
    }
}
