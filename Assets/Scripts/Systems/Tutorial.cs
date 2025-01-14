using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TutorialManager manager;
    [Header("Step 2")]
    [SerializeField] GameObject step2Object;
    [SerializeField] PlayerBattleHandler player;
    [Header("Step 4")]
    [SerializeField] GameObject step4Object;
    [Header("Step 5")]
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] EnemyHealth enemyHealth;

    private void OnDisable()
    {
        UserManager.Instance.data.tutorialCompleted = true;
    }

    void Start()
    {
        manager.steps = new List<TutorialStep>
        {
            new TutorialStep
            {
                instructionText = "Welcome to Phrase Fighters!" +
                "\nClick to continue...",
                onStepStart = () =>
                {
                    BattleSystem.Instance.enabled = false;
                },
                waitCondition = () => Input.GetMouseButtonDown(0),
                onStepEnd = () =>
                {
                    BattleSystem.Instance.enabled = true;
                },
                delayBeforeNext = 0.1f
            },
            new TutorialStep
            {
                instructionText = "You have time where you need to select a phrase",
                onStepStart = () => step2Object.SetActive(true),
                waitCondition = null,
                onStepEnd = () => step2Object.SetActive(false),
                delayBeforeNext = 5f
            },
            new TutorialStep
            {
                instructionText = "Try it yourself!",
                onStepStart = () =>
                {
                    BattleSystem.Instance.enabled = true;
                },
                waitCondition = () => player.PlayerMadeChoice,
                delayBeforeNext = 3f
            },
            new TutorialStep
            {
                instructionText = "Good job!\n" +
                "Each phrase has its own status effect.\n" +
                "Click to proceed...",
                onStepStart = () =>
                {
                    step4Object.SetActive(true);
                },
                waitCondition = () => Input.GetMouseButtonDown(0),
                onStepEnd = () =>
                {
                    step4Object.SetActive(false);
                },
                delayBeforeNext = 0
            },
            new TutorialStep
            {
                instructionText = "You have got the basics down now finish the fight!",
                onStepStart = () =>
                {
                    enemyHealth = BattleSystem.Instance.enemyHandler.GetComponentInChildren<EnemyHealth>();
                },
                waitCondition = () => enemyHealth.IsDead() || playerHealth.IsDead(),
                onStepEnd = () =>
                {
                    if (playerHealth.IsDead())
                    {
                        Debug.Log(playerHealth.IsDead());
                    }
                    else
                    {
                        Debug.Log(enemyHealth.IsDead());
                    }
                }
            },
        };
        manager.StartTutorial();
    }
}
