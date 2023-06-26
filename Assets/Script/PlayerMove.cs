using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region private変数
    /// <summary>
    /// 動く速さ
    /// </summary>
    private float _moveSpeed = 5f;

    /// <summary>
    /// 回転速度
    /// </summary>
    private float _rotationSpeed = 360f;
    private IInputEventProviders _playerInput;
    private Rigidbody _rigidbody;
    #endregion private変数

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // WASDキーの入力を取得
        float horizontalInput = _playerInput.GetHorizontalInput();
        float verticalInput = _playerInput.GetVerticalInput();

        // 入力ベクトルを正規化
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        // 移動量
        Vector3 movement = movementDirection * _moveSpeed;
        _rigidbody.velocity = movement;

        // 回転処理
        if (movement == Vector3.zero) return;

        Quaternion toRotation = Quaternion.LookRotation(movement);
        _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }
}
