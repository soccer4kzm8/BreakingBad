using UnityEngine;
using UniRx;

public class HPModel : MonoBehaviour, IHPModel
{
    /// <summary>
    /// 最大HP
    /// </summary>
    public int MaxHP { private set; get; } = 100;

    /// <summary>
    /// 残っているHP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HP => _hp;

    private readonly IntReactiveProperty _hp = new IntReactiveProperty();

    private void Start()
    {
        _hp.Value = MaxHP;
    }

    /// <summary>
    /// ダメージ受けた時の処理
    /// </summary>
    /// <param name="decreasePoint">減らす量</param>
    public void GetDamage(int decreasePoint)
    {
        _hp.Value -= decreasePoint;
    }

    private void OnDestroy()
    {
        _hp.Dispose();
    }
}
