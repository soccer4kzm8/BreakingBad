using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region private変数
    /// <summary>
    /// 動く速さ
    /// </summary>
    private readonly float _moveSpeed = 5f;

    /// <summary>
    /// 回転速度
    /// </summary>
    private readonly float _rotationSpeed = 3600f;

    private Rigidbody _bodyRigidbody;

    private IInputEventProviders _playerInput;

    private GameObject _currentItem;

    private bool _isHoldingItem;

    /// <summary>
    /// 拾い上げられたアイテムの位置
    /// </summary>
    private readonly Vector3 _caughtItemPosition = new Vector3(0f, 1f, 1f);
    #endregion private変数

    #region 定数
    /// <summary>
    /// RaycastのmaxDistance
    /// </summary>
    private const float RAYCAST_MAX_DISTANCE = 2f;
    #endregion 定数
    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
        _bodyRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // キーの入力を取得
        bool catchAndReleaseInput = _playerInput.GetCatchAndReleaseInput();

        CatchAndReleaseItem(catchAndReleaseInput);
    }

    private void FixedUpdate()
    {
        // キーの入力を取得
        float horizontalInput = _playerInput.GetHorizontalInput();
        float verticalInput = _playerInput.GetVerticalInput();

        Move(horizontalInput, verticalInput);
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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, RAYCAST_MAX_DISTANCE) == false) return;

        if (hit.collider.CompareTag("Item") == false) return;

        _isHoldingItem = true;
        _currentItem = hit.collider.gameObject;
        _currentItem.transform.SetParent(transform);
        _currentItem.transform.localPosition = _caughtItemPosition;
        var rigidbody = _currentItem.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        var collider = _currentItem.GetComponent<Collider>();
        collider.enabled = false;
    }

    /// <summary>
    /// アイテムを放す
    /// </summary>
    private void ReleaseItem()
    {
        // アイテムを離す処理
        _isHoldingItem = false;
        _currentItem.transform.SetParent(null);
        var rigidbody = _currentItem.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        var collider = _currentItem.GetComponent<Collider>();
        collider.enabled = true;
        _currentItem = null;
    }
}
