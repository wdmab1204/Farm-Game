using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowSystem : MonoBehaviour
{
    public int maxGrowTime;
    private Clock clock;
    private float startTime;
    private SpriteRenderer sprite;
    private Vector3 originalPosition;
    private Collider2D col2d;
    private int growPhase = 1;
    private Animator ac;
    void Start()
    {
        clock = FindObjectOfType<Clock>();
        startTime = clock.currentTime;
        ac = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        if (sprite.sprite.rect.size.y > 16f)
        {
            originalPosition = new Vector3(originalPosition.x, originalPosition.y - 0.5f, originalPosition.z);
        }

        col2d = transform.parent.gameObject.GetComponent<Collider2D>();
    }


    void Update()
    {
        if (growPhase < 3)
        {
            float t = clock.currentTime - startTime;
            if (maxGrowTime / (3 - growPhase) <= t)
            {
                growPhase++;
                ac.SetInteger("GrowPhase", growPhase);
                col2d.isTrigger = true;
            }
        }
        else if (growPhase >= 3) col2d.isTrigger = false;
        CheckSpriteWidth();
    }

    void CheckSpriteWidth()
    {
        Vector2 spriteSize = sprite.sprite.rect.size;
        if (spriteSize.y > 16)
        {
            transform.position = new Vector2(originalPosition.x, originalPosition.y + 0.5f);
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}
