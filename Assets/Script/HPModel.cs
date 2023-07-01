using UniRx;

public class HPModel : IItemHPModel, IPlayerHPModel
{
    /// <summary>
    /// 最大HP
    /// </summary>
    public int MaxHP { private set; get; }

    /// <summary>
    /// 残っているHP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HP => _hp;

    private readonly IntReactiveProperty _hp = new IntReactiveProperty();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="maxHP">最大HP</param>
    /// <param name="defaultHP">ゲーム開始のHP</param>
    public HPModel(int maxHP, int defaultHP)
    {
        MaxHP = maxHP;
        _hp.Value = defaultHP;
    }

    /// <summary>
    /// ダメージ受けた時の処理
    /// </summary>
    /// <param name="decreasePoint">減らす量</param>
    public void GetDamage(int decreasePoint)
    {
        _hp.Value -= decreasePoint;
    }

    /// <summary>
    /// 回復するときの処理
    /// </summary>
    /// <param name="decreasePoint">増やす量</param>
    public void GetRecovery(int increasePoint)
    {
        _hp.Value += increasePoint;
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    public void OnDestroy()
    {
        _hp.Dispose();
    }
}
