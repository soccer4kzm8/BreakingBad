using UniRx;

public interface IHPModel
{
    /// <summary>
    /// �ő�HP
    /// </summary>
    int MaxHP { get; }

    /// <summary>
    /// �c���Ă���HP
    /// </summary>
    IReadOnlyReactiveProperty<int> HP { get; }

    /// <summary>
    /// �j������
    /// </summary>
    void OnDestroy();
}
