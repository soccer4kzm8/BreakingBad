using UnityEngine;
using UniRx;

public class PlayerHPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPView _hPView;
    private IPlayerHPModel _playerHPModel;
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
    private const int MAX_HP = 180;

    /// <summary>
    /// 1回のダメージで受けるダメージ
    /// </summary>
    private const int DAMAGE = 1;
    #endregion 定数

    private void Start()
    {
        _playerHPModel = new HPModel(MAX_HP, MAX_HP);
        _playerHPModel.HP.Subscribe(_ => _hPView.SetGuage(_playerHPModel.MaxHP, _playerHPModel.HP.Value)).AddTo(this);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _span)
        {
            _playerHPModel.GetDamage(DAMAGE);
            _currentTime = 0f;
        }
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    private void OnDestroy()
    {
        _playerHPModel.OnDestroy();
    }
}
