using UniRx;

public class HPModel : IHPModel
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
    public HPModel(int maxHP)
    {
        MaxHP = maxHP;
        _hp.Value = MaxHP;
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
    /// 破棄処理
    /// </summary>
    public void OnDestroy()
    {
        _hp.Dispose();
    }
}
