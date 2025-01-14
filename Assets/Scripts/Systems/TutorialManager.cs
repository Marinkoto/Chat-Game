using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class TutorialStep
{
    public string instructionText;
    public Action onStepStart;
    public Func<bool> waitCondition;
    public float delayBeforeNext;
    public Action onStepEnd;
}
public class TutorialManager : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public List<TutorialStep> steps = new List<TutorialStep>();
    [SerializeField] private TextMeshProUGUI tutorialText;

    public static bool IsTutorialComplete = false;
    public int CurrentStepIndex { get; set; } = 0;

    public void StartTutorial()
    {
        if (steps.Count > 0)
        {
            CurrentStepIndex = 0;
            StartCoroutine(HandleStep(steps[CurrentStepIndex]));
        }
        else
        {
            Debug.LogWarning("No tutorial steps defined!");
        }
    }

    private IEnumerator HandleStep(TutorialStep step)
    {
        tutorialText.text = step.instructionText;

        step.onStepStart?.Invoke();

        if (step.waitCondition != null)
        {
            yield return new WaitUntil(() => step.waitCondition());
        }

        if (step.delayBeforeNext > 0)
        {
            yield return new WaitForSecondsRealtime(step.delayBeforeNext);
        }

        step.onStepEnd?.Invoke();

        CurrentStepIndex++;
        if (CurrentStepIndex < steps.Count)
        {
            StartCoroutine(HandleStep(steps[CurrentStepIndex]));
        }
        else
        {
            EndTutorial();
        }
    }


    private void EndTutorial()
    {
        tutorialText.text = "Good job!\n" +
            "Continue onwards with your next game!";
        Debug.Log("Tutorial finished!");
        UserManager.Instance.data.tutorialCompleted = true;
    }
}
