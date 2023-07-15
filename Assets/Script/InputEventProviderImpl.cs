using UnityEngine;

public class InputEventProviderImpl : IInputEventProviders
{
    /// <summary>
    /// 水平方向の入力
    /// </summary>
    /// <returns></returns>
    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// 垂直方向の入力
    /// </summary>
    /// <returns></returns>
    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }

    /// <summary>
    /// アイテムを持ち上げる・放すの入力
    /// </summary>
    /// <returns></returns>
    public bool GetCatchAndReleaseInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
