using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PoliceCollisionTriggerEventProviderImpl : MonoBehaviour, IInSightEventProvider
{
    #region SerializeField
    /// <summary>
    /// 視界範囲
    /// </summary>
    [SerializeField] private GameObject _sightRange;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// 視界内に敵が居るかを監視
    /// </summary>
    private readonly ReactiveProperty<bool> _inSight = new ReactiveProperty<bool>();

    /// <summary>
    /// 視界内のオブジェクト
    /// </summary>
    private GameObject _inSightObj = null;

    /// <summary>
    /// 視界角度
    /// </summary>
    private float _sightAngle = 45f;
    #endregion private変数

    #region public変数
    /// <summary>
    /// 視界内に敵が居るか
    /// </summary>
    public IReadOnlyReactiveProperty<bool> InSight => _inSight;

    /// <summary>
    /// 視界内のオブジェクト
    /// </summary>
    public GameObject InSightObj => _inSightObj;
    #endregion public変数

    private void Start()
    {
        _sightRange.OnTriggerStayAsObservable()
            .Where(collider => collider.CompareTag("Player") == true)
            .Subscribe(collider =>
            {
                if(InSightCheck(collider, _sightAngle) == true)
                {
                    _inSight.Value = true;
                    _inSightObj = collider.gameObject;
                }
                else
                {
                    _inSight.Value = false;
                    _inSightObj = null;
                }
            }).AddTo(this);
        _sightRange.OnTriggerExitAsObservable()
            .Where(_ => _inSight.Value == true)
            .Subscribe(collider =>
            {
                _inSight.Value = false;
            }).AddTo(this);
    }

    /// <summary>
    /// 当たったオブジェクトが視界内かどうか
    /// </summary>
    /// <param name="collider">当たったオブジェクトのcollider</param>
    /// <param name="sightAngle">視界角度</param>
    /// <returns></returns>
    private bool InSightCheck(Collider collider, float sightAngle)
    {
        Vector3 posDelta = collider.transform.position - this.transform.position;
        float targetAngle = Vector3.Angle(this.transform.forward, posDelta);
        if (targetAngle <= sightAngle)
        {
            return true;
        }
        return false;
    }
}
