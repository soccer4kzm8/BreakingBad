using UnityEngine;
public interface IPlayerItemState
{
    /// <summary>
    /// Stateに入った時の処理
    /// </summary>
    /// <param name="itemGameObject">アイテムGameObject</param>
    /// <param name="moveToPosition">移動先のポジション</param>
    void Enter(GameObject itemGameObject, Vector3 moveToPosition);
}
