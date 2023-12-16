using UnityEngine;

public class PlayerItemStateController : MonoBehaviour
{
    #region SerializeFiled
    /// <summary>
    /// アイテムを持っていないステート
    /// </summary>
    [SerializeField] private PlayerItemNotHoldState _playerItemNotHoldState;

    /// <summary>
    /// アイテムを持っているステート
    /// </summary>
    [SerializeField] private PlayerItemHoldState _playerItemHoldState;
    #endregion SerializeFiled

    #region pulic変数
    public PlayerItemNotHoldState PlayerItemNotHoldState => _playerItemNotHoldState;
    public PlayerItemHoldState PlayerItemHoldState => _playerItemHoldState;

    /// <summary>
    /// 現在のステート
    /// </summary>
    public IPlayerItemState CurrentState { get; private set; }

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
        _playerItemNotHoldState.Initialize(this);
        _playerItemHoldState.Initialize(moveToTransform);
    }

    /// <summary>
    /// ステート変更
    /// </summary>
    /// <param name="nextState">次のステート</param>
    /// <param name="moveToPosition">アイテムの移動ポジション</param>
    public void ChangeState(IPlayerItemState nextState, GameObject itemGameObject, Vector3 moveToPosition)
    {
        CurrentState = nextState;
        CurrentItemID = itemGameObject.GetInstanceID();
        nextState.Enter(itemGameObject, moveToPosition);
    }
}
