using UnityEngine;

public class ItemStateController : MonoBehaviour
{
    #region SerializeFiled
    /// <summary>
    /// アイテムを持っていないステート
    /// </summary>
    [SerializeField] private ItemOnGroundState _itemOnGroundState;

    /// <summary>
    /// アイテムを持っているステート
    /// </summary>
    [SerializeField] private ItemHeldState _itemHeldState;
    #endregion SerializeFiled

    #region pulic変数
    public ItemOnGroundState ItemOnGroundState => _itemOnGroundState;
    public ItemHeldState ItemHeldState => _itemHeldState;

    /// <summary>
    /// 現在のステート
    /// </summary>
    public IItemState CurrentState { get; private set; }

    /// <summary>
    /// アイテムID
    /// </summary>
    public int CurrentItemID { get; set; } = 0;
    #endregion pulic変数

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="moveToTransform">アイテムを持った時に移動させるTransform</param>
    public void Initialize(Transform moveToTransform)
    {
        _itemOnGroundState.Initialize(this);
        _itemHeldState.Initialize(moveToTransform);
    }

    /// <summary>
    /// ステート変更
    /// </summary>
    /// <param name="nextState">次のステート</param>
    /// <param name="moveToPosition">アイテムの移動ポジション</param>
    public void ChangeState(IItemState nextState, GameObject itemGameObject, Vector3 moveToPosition)
    {
        CurrentState = nextState;
        CurrentItemID = itemGameObject.GetInstanceID();
        nextState.Enter(itemGameObject, moveToPosition);
    }
}
