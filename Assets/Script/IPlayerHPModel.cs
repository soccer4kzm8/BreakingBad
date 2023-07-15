public interface IPlayerHPModel : IHPModel
{
    /// <summary>
    /// ダメージ受けた時の処理
    /// </summary>
    /// <param name="decreasePoint">減らす量</param>
    void GetDamage(int decreasePoint);
}
