using UnityEngine;

public class ItemStateController : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// 表示させるもの
    /// </summary>
    [SerializeField] private GameObject _displayThing = null;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// Defaultステート
    /// </summary>
    private ItemDefaultState _defaultState;

    /// <summary>
    /// InItemステート
    /// </summary>
    private ItemInItemState _inItemState;
    #endregion private変数

    #region public変数
    /// <summary>
    /// Defaultステート
    /// </summary>
    public ItemDefaultState DefaultState => _defaultState;

    /// <summary>
    /// InItemステート
    /// </summary>
    public ItemInItemState InItemState => _inItemState;
    #endregion publlic変数

    /// <summary>
    /// 現在のステート
    /// </summary>
    public IItemState CurrentState { get; private set; }

    private void Awake()
    {
        _defaultState = new ItemDefaultState(_displayThing);
        _inItemState = new ItemInItemState(_displayThing);
        ChangeState(DefaultState);
    }

    /// <summary>
    /// ステート変更
    /// </summary>
    /// <param name="nextState">次のステート</param>
    public void ChangeState(IItemState nextState)
    {
        CurrentState = nextState;
        nextState.Enter();
    }
}
