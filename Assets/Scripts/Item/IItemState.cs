public interface IItemState
{
    /// <summary>
    /// Stateに入ったときの処理
    /// </summary>
    void Enter();

    /// <summary>
    /// Stateから出るときの処理
    /// </summary>
    void Exit();
}
