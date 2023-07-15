public interface IItemHPModel : IHPModel
{
    /// <summary>
    /// 回復する時の処理
    /// </summary>
    /// <param name="increasePoint">増やす量</param>
    void GetRecovery(int increasePoint);
}
