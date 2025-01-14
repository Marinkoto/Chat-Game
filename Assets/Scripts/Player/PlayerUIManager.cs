using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerPhraseManager), typeof(PlayerHealth))]
public class PlayerUIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] Image iconImage;
    [SerializeField] Image[] phraseIcons;
    [SerializeField] public Button[] phraseButtons;
    [SerializeField] Transform[] phrasesInfo;
    [SerializeField] Slider healthBar;
    [SerializeField] Animator shield;
    [Header("References")]
    [SerializeField] PlayerPhraseManager phraseSystem;
    [SerializeField] PlayerHealth healthSystem;

    private void OnEnable()
    {
        PlayerHealth.OnHit.AddListener(() => ManageShield(healthSystem.Hittable));
    }

    public void Initialize()
    {
        SliderUtils.SetupSlider(healthBar, healthSystem.currentHealth, healthSystem.currentHealth);
    }

    public void HealthBarAnimation()
    {
        SliderUtils.SmoothAnimation(healthBar, 2, healthSystem.currentHealth);
    }

    public void ManagePanel(bool state)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool("IsOpen", state);
    }

    public void ManageShield(bool state)
    {
        if (shield == null)
            return;
        shield.SetBool("Active", !state);
    }
    public void SetPhraseInfo(int index)
    {
        phraseButtons[index].GetComponentInChildren<TextMeshProUGUI>().text =
            CharacterDataManager.Instance.selectedCharacter.phrases[index].phrase;
        phraseIcons[index].sprite = CharacterDataManager.Instance.selectedCharacter.phrases[index].icon;
        phrasesInfo[index].GetComponentInChildren<TextMeshProUGUI>(true).text = 
            CharacterDataManager.Instance.selectedCharacter.phrases[index].description;
    }

    public void SetPlayerIcon()
    {
        iconImage.sprite = phraseSystem.selectedCharacter.icon;
    }

    public void SetupPhrasesUI()
    {
        CharacterDataManager.Instance.selectedCharacter.phrases.Shuffle();
        int phraseCount = Mathf.Min(phraseButtons.Length, CharacterDataManager.Instance.selectedCharacter.phrases.Count);
        for (int i = 0; i < phraseCount; i++)
        {
            SetPhraseInfo(i);
        }
    }
}
