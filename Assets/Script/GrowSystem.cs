using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowSystem : MonoBehaviour
{
    public float maxGrowTime;
    public Item item;
    public GameObject itemPrefab;
    public int minDropCount = 1;
    public int maxDropCount = 1;

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
            //originalPosition = new Vector3(originalPosition.x, originalPosition.y - 0.5f, originalPosition.z);
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

    private void CheckSpriteWidth()
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

    public bool Harvest()
    {

        Item crop = ItemManager.Instance.GetItem(item.id + 100);
        if (crop != null && growPhase >= 3)
        {
            for (int i = 0; i < GetRandomInteger(); i++)
            {
                GameObject obj = new GameObject(crop.name);
                obj.transform.position = transform.parent.position;
                SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = crop.Icon;
                spriteRenderer.sortingLayerName = "Object";
                BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
                col.enabled = false;
                col.isTrigger = true;
                obj.AddComponent<SmoothMove>().speed = 3f;
                obj.tag = "DropItem";

                DropItem di = obj.AddComponent<DropItem>();
                di.item = crop;
            }
        }
        return true;
    }

    private int GetRandomInteger() {
        int value = UnityEngine.Random.Range(minDropCount, maxDropCount);
        return value;
    }

}
