using UniRx;
using UnityEngine;

public interface IInSightEventProvider
{
    /// <summary>
    /// 視界内かどうか
    /// </summary>
    IReadOnlyReactiveProperty<bool> InSight { get; }

    /// <summary>
    /// 視界内のオブジェクト
    /// </summary>
    GameObject InSightObj { get; }
}
