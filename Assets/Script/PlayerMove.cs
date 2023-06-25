using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region private変数
    /// <summary>
    /// 動く速さ
    /// </summary>
    private float _moveSpeed = 5f;
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

        // 入力に基づいて移動量を計算
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * _moveSpeed * Time.deltaTime;
        Vector3 newPosition = _rigidbody.position + movement;
        // 移動量を現在の位置に加算
        _rigidbody.MovePosition(newPosition);
    }
}
