using UnityEngine;
using UnityEngine.UI;

public class HPView : MonoBehaviour
{
    /// <summary>
    /// �X���C�_�[�{��
    /// </summary>
    [SerializeField] private Slider _hpGuage;

    /// <summary>
    /// �Q�[�W�w�i
    /// </summary>
    [SerializeField] private Image _background;

    /// <summary>
    /// �Q�[�W���܂镔���̃C���[�W
    /// </summary>
    [SerializeField] private Image _fill;

    /// <summary>
    /// HP�Q�[�W�̐ݒ�
    /// </summary>
    /// <param name="maxHP">�ő�HP</param>
    /// <param name="hp">�c���Ă���HP</param>
    public void SetGuage(int maxHP, float hp)
    {
        _hpGuage.value = hp / maxHP;
    }

    /// <summary>
    /// �Q�[�W�̕\���E��\��
    /// </summary>
    /// <param name="active"></param>
    public void SetInvisible(bool active)
    {
        Debug.LogError("������");
        _background.enabled = active;
        _fill.enabled = active;
    }
}
