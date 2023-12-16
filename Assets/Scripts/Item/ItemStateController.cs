using UnityEngine;

public class ItemStateController : MonoBehaviour
{
    [SerializeField] private GameObject _displayThing = null;
    private ItemDefaultState _defaultState;
    private ItemInItemState _inItemState;

    public ItemDefaultState DefaultState => _defaultState;
    public ItemInItemState InItemState => _inItemState; 
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
