using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region private�ϐ�
    /// <summary>
    /// ��������
    /// </summary>
    private float _moveSpeed = 5f;

    /// <summary>
    /// ��]���x
    /// </summary>
    private float _rotationSpeed = 360f;
    private IInputEventProviders _playerInput;
    private Rigidbody _rigidbody;
    #endregion private�ϐ�

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // WASD�L�[�̓��͂��擾
        float horizontalInput = _playerInput.GetHorizontalInput();
        float verticalInput = _playerInput.GetVerticalInput();

        // ���̓x�N�g���𐳋K��
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        // �ړ���
        Vector3 movement = movementDirection * _moveSpeed;
        _rigidbody.velocity = movement;

        // ��]����
        if (movement == Vector3.zero) return;

        Quaternion toRotation = Quaternion.LookRotation(movement);
        _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }
}
