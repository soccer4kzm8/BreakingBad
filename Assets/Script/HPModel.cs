using UniRx;

public class HPModel : IHPModel
{
    /// <summary>
    /// �ő�HP
    /// </summary>
    public int MaxHP { private set; get; }

    /// <summary>
    /// �c���Ă���HP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HP => _hp;

    private readonly IntReactiveProperty _hp = new IntReactiveProperty();

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="maxHP">�ő�HP</param>
    public HPModel(int maxHP)
    {
        MaxHP = maxHP;
        _hp.Value = MaxHP;
    }

    /// <summary>
    /// �_���[�W�󂯂����̏���
    /// </summary>
    /// <param name="decreasePoint">���炷��</param>
    public void GetDamage(int decreasePoint)
    {
        _hp.Value -= decreasePoint;
    }

    /// <summary>
    /// �j������
    /// </summary>
    public void OnDestroy()
    {
        _hp.Dispose();
    }
}
