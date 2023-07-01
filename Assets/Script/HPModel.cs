using UniRx;

public class HPModel : IItemHPModel, IPlayerHPModel
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
    /// <param name="defaultHP">�Q�[���J�n��HP</param>
    public HPModel(int maxHP, int defaultHP)
    {
        MaxHP = maxHP;
        _hp.Value = defaultHP;
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
    /// �񕜂���Ƃ��̏���
    /// </summary>
    /// <param name="decreasePoint">���₷��</param>
    public void GetRecovery(int increasePoint)
    {
        _hp.Value += increasePoint;
    }

    /// <summary>
    /// �j������
    /// </summary>
    public void OnDestroy()
    {
        _hp.Dispose();
    }
}
