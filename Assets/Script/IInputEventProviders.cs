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
}
