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
        PlayerHealth.OnHit += () => ManageShield(healthSystem.Hittable); 
    }

    private void OnDisable()
    {
        PlayerHealth.OnHit -= () => ManageShield(healthSystem.Hittable); 
    }

    public void Initialize()
    {
        InitializeHealthBar();
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

    private void InitializeHealthBar()
    {
        healthBar.maxValue = phraseSystem.selectedCharacter.maxHealth;
        healthBar.value = healthSystem.currentHealth;
    }
    
    public void SetPhraseInfo(int index)
    {
        phraseButtons[index].GetComponentInChildren<TextMeshProUGUI>().text =
            CharacterDataManager.instance.selectedCharacter.phrases[index].phrase;
        phraseIcons[index].sprite = CharacterDataManager.instance.selectedCharacter.phrases[index].icon;
        phrasesInfo[index].GetComponentInChildren<TextMeshProUGUI>(true).text = 
            CharacterDataManager.instance.selectedCharacter.phrases[index].description;
    }

    public void SetPlayerIcon()
    {
        iconImage.sprite = phraseSystem.selectedCharacter.icon;
    }

    public void SetupPhrasesUI()
    {
        CharacterDataManager.instance.selectedCharacter.phrases.Shuffle();
        int phraseCount = Mathf.Min(phraseButtons.Length, CharacterDataManager.instance.selectedCharacter.phrases.Count);
        for (int i = 0; i < phraseCount; i++)
        {
            SetPhraseInfo(i);
        }
    }
}
