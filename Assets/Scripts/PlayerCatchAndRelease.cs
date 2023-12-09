using UnityEngine;

public class PlayerCatchAndRelease : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// アイテム当たり判定Collider
    /// </summary>
    [SerializeField] private GameObject _collider;

    [SerializeField] private ItemStateController _itemStateController;
    #endregion SerializeField

    #region private変数
    private IInputEventProviders _playerInput;

    /// <summary>
    /// 接しているアイテムのCollider
    /// </summary>
    private Collider _attachItem = null;
    #endregion private変数

    #region public変数
    public ItemStateController ItemStateController => _itemStateController;
    #endregion public変数

    /// <summary>
    /// 拾い上げられたアイテムの定位置
    /// </summary>
    private readonly Vector3 CAUGT_ITEM_POSITION = new Vector3(0f, 1f, 1f);

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
        _itemStateController.Initialize(transform);
    }

    private void Update()
    {
        CatchAndRelease();
    }

    private void OnTriggerStay(Collider collider)
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

        if (ItemStateController.CurrentItemID == 0)
        {
            CatchItem(_attachItem);
        }
        else if (IsInfrontOfTagObject("MortarPestle") != null)
        {
            DestoryItem(_attachItem.gameObject);
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
        ItemStateController.ChangeState(ItemStateController.ItemHeldState, collider.gameObject, CAUGT_ITEM_POSITION);
    }

    private void DestoryItem(GameObject itemGameObject)
    {
        Destroy(itemGameObject);
        _attachItem = null;
        ItemStateController.CurrentItemID = 0;
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

        ItemStateController.ChangeState(ItemStateController.ItemOnGroundState, collider.gameObject, settingPosition);
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
            if(hit.collider.gameObject.GetInstanceID() == ItemStateController.CurrentItemID)
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
