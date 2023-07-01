using UnityEngine;

public class InputEventProviderImpl : IInputEventProviders
{
    /// <summary>
    /// ���������̓���
    /// </summary>
    /// <returns></returns>
    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// ���������̓���
    /// </summary>
    /// <returns></returns>
    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }

    /// <summary>
    /// �A�C�e���������グ��E�����̓���
    /// </summary>
    /// <returns></returns>
    public bool GetCatchAndReleaseInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
