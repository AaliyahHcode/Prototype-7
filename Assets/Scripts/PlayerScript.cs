using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float hexWidth = 1.8f;
    public float hexHeight = 1.55f;

    public float jumpHeight = 0.3f;
    public float moveTime = 0.2f;

    private bool isMoving = false;

    public ItemType currentItem = ItemType.None;
    public GameObject pickaxeSprite;
    public GameObject bucketSprite;

    public void SetItem(ItemType item)
    {
        currentItem = item;
    }

    void Update()
    {
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToHex(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToHex(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToHex(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToHex(Vector2.down);
        }
    }

    void MoveToHex(Vector2 direction)
    {
        Vector3 target = transform.position;
        target.x += direction.x * hexWidth;
        target.y += direction.y * hexHeight;

        StartCoroutine(JumpMove(target));
    }

    //inventory
    public enum ItemType
    {
        None,Fruit, Wood, Coin, Water
    }

    public void EquipBucket()
    {
        pickaxeSprite.SetActive(false);
        bucketSprite.SetActive(true);
    }

    public void EquipPickaxe()
    {
        pickaxeSprite.SetActive(true);
        bucketSprite.SetActive(false);
    }

    IEnumerator JumpMove(Vector3 target)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            Vector3 pos = Vector3.Lerp(start, target, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;
            transform.position = pos;

            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
