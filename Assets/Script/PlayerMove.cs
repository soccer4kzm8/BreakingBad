using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private Rigidbody _bodyRigidbody;
    #endregion SerializeField
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

    private GameObject _currentItem;

    private bool _isHoldingItem;
    #endregion private変数

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
    }

    private void FixedUpdate()
    {
        // キーの入力を取得
        float horizontalInput = _playerInput.GetHorizontalInput();
        float verticalInput = _playerInput.GetVerticalInput();
        bool catchAndReleaseInput = _playerInput.GetCatchAndReleaseInput();

        Move(horizontalInput, verticalInput);

        CatchAndReleaseItem(catchAndReleaseInput);
    }

    /// <summary>
    /// 移動と回転
    /// </summary>
    /// <param name="horizontalInput"></param>
    /// <param name="verticalInput"></param>
    private void Move(float horizontalInput, float verticalInput)
    {
        // 入力ベクトルを正規化
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        // 移動量
        Vector3 movement = movementDirection * _moveSpeed;
        _bodyRigidbody.velocity = movement;

        // 回転処理
        if (movement == Vector3.zero) return;

        Quaternion toRotation = Quaternion.LookRotation(movement);
        _bodyRigidbody.rotation = Quaternion.RotateTowards(_bodyRigidbody.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// アイテムを拾う・放す
    /// </summary>
    /// <param name="catchAndReleaseInput"></param>
    private void CatchAndReleaseItem(bool catchAndReleaseInput)
    {
        if (catchAndReleaseInput == false) return;

        if(_isHoldingItem == true)
        {
            ReleaseItem();
        }
        else
        {
            CatchItem();
        }
    }
    /// <summary>
    /// アイテムを拾う
    /// </summary>
    private void CatchItem()
    {
        // 近くにあるアイテムを取得
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f) == false) return;

        if (hit.collider.CompareTag("Item") == false) return;

        //Debug.DrawRay(transform.position, transform.forward.normalized, Color.cyan);
        _isHoldingItem = true;
        _currentItem = hit.collider.gameObject;
        _currentItem.transform.SetParent(transform);
        _currentItem.transform.localPosition = new Vector3(0f, 1f, 1f);
        var rigidBody = _currentItem.GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.isKinematic = true;
    }

    /// <summary>
    /// アイテムを放す
    /// </summary>
    private void ReleaseItem()
    {
        // アイテムを離す処理
        _isHoldingItem = false;
        _currentItem.transform.SetParent(null);
        var rigidBody = _currentItem.GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rigidBody.isKinematic = false;
        _currentItem = null;
    }
}
