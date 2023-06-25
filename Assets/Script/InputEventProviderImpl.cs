using UnityEngine;

public class InputEventProviderImpl : IInputEventProviders
{
    /// <summary>
    /// …•½•ûŒü‚Ì“ü—Í
    /// </summary>
    /// <returns></returns>
    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// ‚’¼•ûŒü‚Ì“ü—Í
    /// </summary>
    /// <returns></returns>
    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }
}
