public interface IInputEventProviders
{
    /// <summary>
    /// 水平方向の入力
    /// </summary>
    /// <returns></returns>
    float GetHorizontalInput();

    /// <summary>
    /// 垂直方向の入力
    /// </summary>
    /// <returns></returns>
    float GetVerticalInput();

    /// <summary>
    /// アイテムを持ち上げる・放すの入力
    /// </summary>
    /// <returns></returns>
    bool GetCatchAndReleaseInput();
}
