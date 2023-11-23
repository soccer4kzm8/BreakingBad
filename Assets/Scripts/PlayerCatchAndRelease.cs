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

    /// <summary>
    /// 接しているアイテムのCollider
    /// </summary>
    private Collider _attachItem = null;
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
        CatchAndRelease();

        if (_playerInput.GetThrowAndMixInput())
        {
            _getThrowAndMixInput = true;
        }
        else
        {
            _getThrowAndMixInput = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Item") == true)
        {
            _attachItem = collider;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Item") == true)
        {
            _attachItem = null;
        }
    }

    /// <summary>
    /// アイテムを拾う・放すの処理
    /// </summary>
    private void CatchAndRelease()
    {
        if (_playerInput.GetCatchAndReleaseInput() == false)
        {
            return;
        }

        if(IsInfrontOfTagObject("ItemBox") != null)
        {
            return;
        }

        if (_currentItem == null)
        {
            CatchItem(_attachItem);
        }
        else
        {
            ReleaseItem(_attachItem);
        }
    }

    /// <summary>
    /// アイテムを拾う
    /// </summary>
    public void CatchItem(Collider collider)
    {
        _currentItem = collider.gameObject;
        _currentItem.transform.SetParent(transform);
        _currentItem.transform.localPosition = _caughtItemPosition;
        _currentItem.GetComponent<Rigidbody>().isKinematic = true;
        // アイテムが地面に着いていない設定
        _currentItem.GetComponent<ItemConstraintsManager>().SetIsItemOnGround(false);
        _currentItem.GetComponent<Collider>().isTrigger = true;
    }

    /// <summary>
    /// アイテムを放す
    /// </summary>
    /// <param name="collider">持っているアイテムのコライダー</param>
    private void ReleaseItem(Collider collider)
    {
        var hitCollider = IsInfrontOfTagObject("Wall");
        if (hitCollider == null) return;
        
        // Wall上のグリッドに分けられ箇所に配置
        var closestPosition = hitCollider.GetComponent<GridSystem>().GetClosestPos(transform.position);
        var settingPosition = new Vector3(closestPosition.x, closestPosition.y + collider.transform.localScale.y * (collider.transform.localScale.y * 0.5f), closestPosition.z);
        _currentItem.transform.position = settingPosition;
        _currentItem.GetComponent<Rigidbody>().isKinematic = false;
        // アイテムが地面に着いている設定
        _currentItem.GetComponent<ItemConstraintsManager>().SetIsItemOnGround(true);
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

    /// <summary>
    /// 指定タグオブジェクト前に立っているか
    /// </summary>
    /// <param name="tagName">指定タグ</param>
    /// <returns></returns>
    private Collider IsInfrontOfTagObject(string tagName)
    {
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, this.transform.forward, 1f);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag(tagName))
            {
                return hit.collider;
            }
        }
        return null;
    }
}
