using UnityEngine;

public class CameraManagerMinimized : MonoBehaviour
{
    #region SerializeField Private Fields

    [SerializeField] private GameObject _followTarget;

    [SerializeField] private float _maxRotate;
    [SerializeField] private float _minRotate;

    [SerializeField] private float _rotationPower;

    [SerializeField] private Vector3 _look;

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _look.x = Input.GetAxis("Mouse X");
        _look.y = -Input.GetAxis("Mouse Y");

        _followTarget.transform.rotation *= Quaternion.AngleAxis(_look.x * _rotationPower, Vector3.up);
        _followTarget.transform.rotation *= Quaternion.AngleAxis(_look.y * _rotationPower, Vector3.right);

        var angles = _followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = _followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < _maxRotate)
        {
            angles.x = _maxRotate;
        }
        else if (angle < 180 && angle > _minRotate)
        {
            angles.x = _minRotate;
        }

        _followTarget.transform.localEulerAngles = angles;
    }

    #endregion
}
