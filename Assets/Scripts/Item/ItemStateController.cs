using UnityEngine;

public class ItemStateController : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// 表示させるもの
    /// </summary>
    [SerializeField] private GameObject[] _displayThings = null;

    /// <summary>
    /// HP
    /// </summary>
    [SerializeField] private GameObject _hP = null;

    /// <summary>
    /// アニメーター
    /// </summary>
    [SerializeField] private Animator _animator = null;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// プレイヤーの入力
    /// </summary>
    private IInputEventProviders _playerInput;

    /// <summary>
    /// 接しているプレイヤーのCollider
    /// </summary>
    private Collider _attachPlayer = null;
    #endregion private変数

    #region public変数
    /// <summary>
    /// 表示させるもの
    /// </summary>
    public GameObject[] DisplayThings => _displayThings;

    /// <summary>
    /// HP
    /// </summary>
    public GameObject HP => _hP;

    /// <summary>
    /// アニメーター
    /// </summary>
    public Animator Animator => _animator;

    /// <summary>
    /// Defaultステート
    /// </summary>
    public ItemDefaultState DefaultState { get; private set; }

    /// <summary>
    /// アイテム内にアイテムが入れられたステート
    /// </summary>
    public ItemInItemState InItemState { get; private set; }

    /// <summary>
    /// アイテムが料理中のステート
    /// </summary>
    public ItemCookingState CookingState { get; private set; }

    /// <summary>
    /// アイテム料理完了ステート
    /// </summary>
    public ItemCookedCompleteState CompleteState { get; private set; }

    /// <summary>
    /// アイテム料理一時停止ステート
    /// </summary>
    public ItemCookingPauseState PauseState { get; private set; }

    /// <summary>
    /// 現在のステート
    /// </summary>
    public IItemState CurrentState { get; private set; }
    #endregion publlic変数

    private void Awake()
    {
        DefaultState = new ItemDefaultState(this);
        InItemState = new ItemInItemState(this);
        CookingState = new ItemCookingState(this);
        CompleteState = new ItemCookedCompleteState(this);
        PauseState = new ItemCookingPauseState(this);
        CurrentState = DefaultState;
        _playerInput = new InputEventProviderImpl();
        Initialize();
    }

    private void Update()
    {
        if (_playerInput.GetThrowAndMixInput() == false)
        {
            return;
        }

        if (_attachPlayer == null)
        {
            return;
        }

        var currentItemID = _attachPlayer.transform.parent.GetComponent<PlayerCatchAndRelease>().PlayerItemStateController.CurrentItemID;
        if (currentItemID != 0)
        {
            return;
        }

        if(CurrentState == InItemState || CurrentState == PauseState)
        {
            ChangeState(CookingState);
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        foreach (var displayThing in _displayThings)
        {
            displayThing.SetActive(false);
        }

        _hP.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") == true)
        {
            _attachPlayer = collider;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player") == true)
        {
            _attachPlayer = null;
            if(CurrentState == CookingState)
            {
                ChangeState(PauseState);
            }
        }
    }

    /// <summary>
    /// ステート変更
    /// </summary>
    /// <param name="nextState">次のステート</param>
    public void ChangeState(IItemState nextState)
    {
        if(CurrentState == nextState)
        {
            return;
        }
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
}
