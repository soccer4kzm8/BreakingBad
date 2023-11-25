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

    /// <summary>
    /// 現在持っているアイテム
    /// </summary>
    private GameObject _currentItem = null;

    /// <summary>
    /// 現在持っているアイテムID
    /// </summary>
    private int _currentItemID = 0;

    /// <summary>
    /// 拾い上げられたアイテムの位置
    /// </summary>
    private readonly Vector3 _caughtItemPosition = new Vector3(0f, 1f, 1f);

    /// <summary>
    /// 接しているアイテムのCollider
    /// </summary>
    private Collider _attachItem = null;
    #endregion private変数

    #region public変数
    /// <summary>
    /// 現在持っているアイテム
    /// </summary>
    public GameObject CurrentItem => _currentItem;
    #endregion public変数

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
    }

    private void Update()
    {
        CatchAndRelease();
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

        if(_attachItem == null)
        {
            return;
        }

        if (_currentItem == null)
        {
            CatchItem(_attachItem);
        }
        else if (IsInfrontOfTagObject("MortarPestle") != null)
        {
            ReleaseItem(_attachItem);
        }
        else if (IsInfrontOfTagObject("Item") == null)
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
        _currentItemID = _currentItem.GetInstanceID();
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
        var gridSystem = hitCollider.GetComponent<GridSystem>();
        var closestPosition = gridSystem.GetClosestPos(transform.position);
        gridSystem.SetItem(closestPosition, collider.name);
        var settingPosition = new Vector3(closestPosition.x, closestPosition.y + collider.transform.localScale.y * (collider.transform.localScale.y * 0.5f), closestPosition.z);
        _currentItem.transform.position = settingPosition;
        _currentItem.GetComponent<Rigidbody>().isKinematic = false;
        // アイテムが地面に着いている設定
        _currentItem.GetComponent<ItemConstraintsManager>().SetIsItemOnGround(true);
        _currentItem.GetComponent<Collider>().isTrigger = false;
        _currentItem.transform.SetParent(null);
        _currentItem = null;
        _currentItemID = 0;
    }

    /// <summary>
    /// 指定タグオブジェクト前に立っている場合、指定オブジェクトのColliderを返す
    /// </summary>
    /// <param name="tagName">指定タグ</param>
    /// <returns></returns>
    private Collider IsInfrontOfTagObject(string tagName)
    {
        Vector3 rayOrigin = transform.position + transform.up * 1f + transform.forward * 1f;
        //Debug.DrawRay(rayOrigin, -this.transform.up * 20f, Color.cyan, 2f);
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, -this.transform.up, 2f);
        foreach (var hit in hits)
        {
            if(hit.collider.gameObject.GetInstanceID() == _currentItemID)
            {
                continue;
            }
            if (hit.collider.CompareTag(tagName))
            {
                return hit.collider;
            }
        }
        return null;
    }
}
