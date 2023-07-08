using UnityEngine;
using UniRx;

public class ItemHPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPView _hPView;
    [SerializeField] private Transform _rayOrigin;
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
    private const int MAX_HP = 20;

    /// <summary>
    /// 1回で受ける回復する量
    /// </summary>
    private const int RECOVERY = 1;

    /// <summary>
    /// RaycastのmaxDistance
    /// </summary>
    private const float RAYCAST_MAX_DISTANCE = 500f;
    #endregion 定数

    private void Start()
    {
        _itemHPModel = new HPModel(MAX_HP, 0);
        _itemHPModel.HP.Subscribe(_ => _hPView.SetGuage(_itemHPModel.MaxHP, _itemHPModel.HP.Value)).AddTo(this);
    }

    private void Update()
    {
        // 近くにあるアイテムを取得
        if (Physics.Raycast(_rayOrigin.position, Vector3.down, out RaycastHit hit, RAYCAST_MAX_DISTANCE) == false) return;

        if (hit.collider.CompareTag("Fire") == false) return;

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
