using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPView _hPView;
    private IHPModel _hPModel;
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
    private const int MAX_HP = 100;
    #endregion 定数

    private void Start()
    {
        _hPModel = new HPModel(MAX_HP);
        _hPModel.HP.Subscribe(_ => _hPView.SetGuage(_hPModel.MaxHP, _hPModel.HP.Value)).AddTo(this);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _span)
        {
            _hPModel.GetDamage(1);
            _currentTime = 0f;
        }
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    private void OnDestroy()
    {
        _hPModel.OnDestroy();
    }
}
