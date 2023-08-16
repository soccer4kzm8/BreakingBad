using UnityEngine;
using UnityEngine.AI;
using UniRx;

public class PoliceMove : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private PoliceCollisionTriggerEventProviderImpl _policeCollisionTriggerEventProvider;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// Policeが動く間隔
    /// </summary>
    private float _span = 3f;

    /// <summary>
    /// Policeが動いてから何秒経ったか
    /// </summary>
    private float _currentTime = 0f;

    /// <summary>
    /// ステージのX軸最小値
    /// </summary>
    private float _stageXMinRange = -1f;

    /// <summary>
    /// ステージ上のX軸最大値
    /// </summary>
    private float _stageXMaxRange = 21f;

    /// <summary>
    /// ステージ上のZ軸最小値
    /// </summary>
    private float _stageZMinRange = -13f;

    /// <summary>
    /// ステージ上のZ軸最大値
    /// </summary>
    private float _stageZMaxRange = 13f;
    #endregion private 変数

    #region 定数
    /// <summary>
    /// 首振りスピード
    /// </summary>
    private const float ROTATION_SPEED = 0.1f;
    #endregion 定数

    private void Start()
    {
        _policeCollisionTriggerEventProvider.InSight.Subscribe(isInSight => 
        {
            _navMeshAgent.isStopped = isInSight;
        }).AddTo(this);
        _currentTime = _span;
    }

    private void Update()
    {
        if (_policeCollisionTriggerEventProvider.InSight.Value == true)
        {
            if (_policeCollisionTriggerEventProvider.InSightObj == null) return;

            // ターゲット方向のベクトルを取得
            Vector3 relativePos = _policeCollisionTriggerEventProvider.InSightObj.transform.position - this.transform.position;
            // 方向を、回転情報に変換
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // 現在の回転情報と、ターゲット方向の回転情報を補完する
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, ROTATION_SPEED);
            return;
        }
        _currentTime += Time.deltaTime;
        if (_currentTime > _span)
        {
            Vector3 randomPos = new Vector3(Random.Range(_stageXMinRange, _stageXMaxRange), 0, Random.Range(_stageZMinRange, _stageZMaxRange));
            //SamplePositionは設定した場所から半径5の範囲で最も近い距離のBakeされた場所をnavMeshHitに探す。
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit navMeshHit, 5, NavMesh.AllAreas))
            {
                _navMeshAgent.SetDestination(navMeshHit.position);
            }
            _currentTime = 0f;
        }
    }
}
