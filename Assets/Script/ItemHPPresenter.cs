using UnityEngine;
using UniRx;

public class ItemHPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPView _hPView;
    private IItemHPModel _itemHPModel;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// ダメージを受ける間隔
    /// </summary>
    private readonly float _span = 1f;

    /// <summary>
    /// ダメージを受けてから何秒経ったか
    /// </summary>
    private float _currentTime = 0f;
    #endregion private変数

    #region 定数
    /// <summary>
    /// 最大HP
    /// </summary>
    private const int MAX_HP = 10;

    /// <summary>
    /// 1回で受ける回復する量
    /// </summary>
    private const int RECOVERY = 1;
    #endregion 定数

    private void Start()
    {
        _itemHPModel = new HPModel(MAX_HP, 0);
        _itemHPModel.HP.Subscribe(_ => _hPView.SetGuage(_itemHPModel.MaxHP, _itemHPModel.HP.Value)).AddTo(this);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _span)
        {
            _itemHPModel.GetRecovery(RECOVERY);
            _currentTime = 0f;
        }
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    private void OnDestroy()
    {
        _itemHPModel.OnDestroy();
    }
}
