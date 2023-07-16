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
        if (_policeCollisionTriggerEventProvider.InSight.Value == true) return;
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
