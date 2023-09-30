using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerCatchAndRelease : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// アイテム当たり判定Collider
    /// </summary>
    [SerializeField] private GameObject _collider;
    #endregion SerializeField

    #region private変数
    private IInputEventProviders _playerInput;

    private Rigidbody _rigidbody;

    /// <summary>
    /// 拾う・放すの入力がされたかどうか
    /// </summary>
    private bool _getCatchAndReleaseInput = false;

    /// <summary>
    /// 投げる・混ぜるの入力がされたかどうか
    /// </summary>
    private bool _getThrowAndMixInput = false;

    /// <summary>
    /// 現在持っているアイテム
    /// </summary>
    private GameObject _currentItem = null;

    /// <summary>
    /// 拾い上げられたアイテムの位置
    /// </summary>
    private readonly Vector3 _caughtItemPosition = new Vector3(0f, 1f, 1f);
    #endregion private変数

    /// <summary>
    /// 現在持っているアイテム
    /// </summary>
    public GameObject CurrentItem => _currentItem;

    #region 定数
    /// <summary>
    /// プレイヤースピード抑制定数
    /// </summary>
    private const float VELOCITY_SUPPRESSION = 0.3f;

    /// <summary>
    /// デフォルトの投げる力
    /// </summary>
    private const float DEFAULT_THROW_FORCE = 7.0f;
    #endregion 定数

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
        _rigidbody = this.GetComponent<Rigidbody>();

        // アイテム拾う・放す
        _collider.OnTriggerStayAsObservable()
            .Where(collider => collider.CompareTag("Item"))
            .Where(_ => _getCatchAndReleaseInput == true)
            .Subscribe(collider =>
            {
                _getCatchAndReleaseInput = false;
                CatchAndReleaseItem(collider);
            }).AddTo(this);

        // アイテム投げる
        _collider.OnTriggerStayAsObservable()
            .Where(collider => collider.CompareTag("Item"))
            .Where(_ => _getThrowAndMixInput == true)
            .Where(_ => _currentItem != null)
            .Subscribe(collider =>
            {
                _getThrowAndMixInput = false;
                ThrowItem();
            }).AddTo(this);
    }

    private void Update()
    {
        if (_playerInput.GetCatchAndReleaseInput())
        {
            _getCatchAndReleaseInput = true;
        }
        else
        {
            _getCatchAndReleaseInput = false;
        }

        if (_playerInput.GetThrowAndMixInput())
        {
            _getThrowAndMixInput = true;
        }
        else
        {
            _getThrowAndMixInput = false;
        }
    }

    /// <summary>
    /// アイテムを拾う・放す
    /// </summary>
    /// <param name="catchAndReleaseInput"></param>
    private void CatchAndReleaseItem(Collider collider)
    {
        if (_currentItem != null)
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
        var rigidbody = _currentItem.GetComponent<Rigidbody>().isKinematic = true;
        // アイテムが地面に着いていない設定
        _currentItem.GetComponent<ItemConstraintsManager>().SetIsItemOnGround(false);
        _currentItem.GetComponent<Collider>().isTrigger = true;
    }

    /// <summary>
    /// アイテムを放す
    /// </summary>
    private void ReleaseItem()
    {
        // アイテムを離す処理
        _currentItem.GetComponent<Rigidbody>().isKinematic = false;
        _currentItem.GetComponent<Collider>().isTrigger = false;
        _currentItem.transform.SetParent(null);
        _currentItem = null;
    }

    /// <summary>
    /// アイテムを投げる
    /// </summary>
    private void ThrowItem()
    {
        _currentItem.GetComponent<Collider>().isTrigger = false;
        var rigidBody = _currentItem.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        Vector3 forceDirection = new Vector3(this.transform.forward.x, 0.5f, this.transform.forward.z);
        float forceMagnitude = _rigidbody.velocity.magnitude * VELOCITY_SUPPRESSION + DEFAULT_THROW_FORCE;
        Vector3 force = forceMagnitude * forceDirection;
        rigidBody.AddForce(force, ForceMode.Impulse);
        _currentItem.transform.SetParent(null);
        _currentItem = null;
    }
}
