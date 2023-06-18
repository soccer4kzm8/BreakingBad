using UnityEngine;
using UnityEngine.UI;


public class HPView : MonoBehaviour
{
    [SerializeField] private Slider _hpGuage;

    /// <summary>
    /// HPƒQ[ƒW‚Ìİ’è
    /// </summary>
    /// <param name="maxHP">Å‘åHP</param>
    /// <param name="hp">c‚Á‚Ä‚¢‚éHP</param>
    public void SetGuage(int maxHP, float hp)
    {
        _hpGuage.value = hp / maxHP;
    }
}
