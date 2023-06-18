using UnityEngine;
using UniRx;

public class HPModel : MonoBehaviour, IHPModel
{
    /// <summary>
    /// �ő�HP
    /// </summary>
    public int MaxHP { private set; get; } = 100;

    /// <summary>
    /// �c���Ă���HP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HP => _hp;

    private readonly IntReactiveProperty _hp = new IntReactiveProperty();

    private void Start()
    {
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

    private void OnDestroy()
    {
        _hp.Dispose();
    }
}
