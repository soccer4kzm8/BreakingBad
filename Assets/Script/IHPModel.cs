using UniRx;

public interface IHPModel
{
    /// <summary>
    /// Å‘åHP
    /// </summary>
    int MaxHP { get; }

    /// <summary>
    /// c‚Á‚Ä‚¢‚éHP
    /// </summary>
    IReadOnlyReactiveProperty<int> HP { get; }

    /// <summary>
    /// ”jŠüˆ—
    /// </summary>
    void OnDestroy();
}
