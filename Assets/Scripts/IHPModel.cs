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
    /// 破棄処理
    /// </summary>
    void OnDestroy();
}
