using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region private�ϐ�
    /// <summary>
    /// ��������
    /// </summary>
    private float _moveSpeed = 5f;
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

        // ���͂Ɋ�Â��Ĉړ��ʂ��v�Z
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * _moveSpeed * Time.deltaTime;
        Vector3 newPosition = _rigidbody.position + movement;
        // �ړ��ʂ����݂̈ʒu�ɉ��Z
        _rigidbody.MovePosition(newPosition);
    }
}
