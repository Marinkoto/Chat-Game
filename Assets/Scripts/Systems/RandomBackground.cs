using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<Sprite> backgrounds;

    void Start()
    {
        Sprite randomSprite = ListUtils.GetRandomItem(backgrounds);

        if (TrySetSpriteRenderer(randomSprite)) return;
        if (TrySetImage(randomSprite)) return;

    }

    private bool TrySetSpriteRenderer(Sprite sprite)
    {
        if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            spriteRenderer.sprite = sprite;
            return true;
        }
        return false;
    }

    private bool TrySetImage(Sprite sprite)
    {
        if (TryGetComponent<Image>(out var image))
        {
            image.sprite = sprite;
            return true;
        }
        return false;
    }
}
