using UniRx;

public interface IHPModel
{
    /// <summary>
    /// 最大HP
    /// </summary>
    int MaxHP { get; }

    /// <summary>
    /// 残っているHP
    /// </summary>
    IReadOnlyReactiveProperty<int> HP { get; }

    /// <summary>
    /// ダメージ受けた時の処理
    /// </summary>
    /// <param name="decreasePoint">減らす量</param>
    public void GetDamage(int decreasePoint);
}
