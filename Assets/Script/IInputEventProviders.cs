public interface IInputEventProviders
{
    /// <summary>
    /// ���������̓���
    /// </summary>
    /// <returns></returns>
    float GetHorizontalInput();

    /// <summary>
    /// ���������̓���
    /// </summary>
    /// <returns></returns>
    float GetVerticalInput();

    /// <summary>
    /// �A�C�e���������グ��E�����̓���
    /// </summary>
    /// <returns></returns>
    bool GetCatchAndReleaseInput();
}
