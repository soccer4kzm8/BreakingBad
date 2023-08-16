using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerMove : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// アイテム当たり判定Collider
    /// </summary>
    [SerializeField] private GameObject _collider;
    #endregion SerializeField

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

    /// <summary>
    /// 現在持っているアイテム
    /// </summary>
    private GameObject _currentItem = null;

    /// <summary>
    /// 拾い上げられたアイテムの位置
    /// </summary>
    private readonly Vector3 _caughtItemPosition = new Vector3(0f, 1f, 1f);

    /// <summary>
    /// 拾う・放すの入力がされたかどうか
    /// </summary>
    private bool _getCatchAndReleaseInput = false;
    #endregion private変数

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
        _bodyRigidbody = GetComponent<Rigidbody>();
        _collider.OnTriggerStayAsObservable()
            .Where(collider => collider.CompareTag("Item"))
            .Where(_ => _getCatchAndReleaseInput == true)
            .Subscribe(collider => 
            {
                _getCatchAndReleaseInput = false;
                CatchAndReleaseItem(collider);
            }).AddTo(this);
    }

    private void Update()
    {
        if (_playerInput.GetCatchAndReleaseInput())
        {
            _getCatchAndReleaseInput = true;
        }
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
    private void CatchAndReleaseItem(Collider collider)
    {
        if(_currentItem != null)
        {
            ReleaseItem();
        }
        else
        {
            CatchItem(collider);
        }
    }
    /// <summary>
    /// アイテムを拾う
    /// </summary>
    private void CatchItem(Collider collider)
    {
        _currentItem = collider.gameObject;
        _currentItem.transform.SetParent(transform);
        _currentItem.transform.localPosition = _caughtItemPosition;
        _currentItem.GetComponent<Rigidbody>().isKinematic = true;
        _currentItem.GetComponent<Collider>().isTrigger = true;
    }

    /// <summary>
    /// アイテムを放す
    /// </summary>
    private void ReleaseItem()
    {
        // アイテムを離す処理
        _currentItem.transform.SetParent(null);
        _currentItem.GetComponent<Rigidbody>().isKinematic = false;
        _currentItem.GetComponent<Collider>().isTrigger = false;
        _currentItem = null;
    }
}
