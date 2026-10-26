using UnityEngine;

public class GetFruit : MonoBehaviour
{
    // when the player hits g, near the tree tag (get fruit or wood) or treasure chest (get coin)  or well (get water)note then grab the item and press D to drop the item off to the hightlighted villager.
    private GameObject nearObj;
    private PlayerScript player;

    void Start()
    {
        player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GatherItem();
        }
    }

    void GatherItem()
    {
        // check if already carrying smthn
        if (player.currentItem != PlayerScript.ItemType.None)
        {
            Debug.Log("Already carrying something!");
            return;
        }

        if (nearObj == null)
            return;

        if (nearObj.CompareTag("Tree"))
        {
            if(Random.Range(0,2) == 0)
            {
                player.SetItem(PlayerScript.ItemType.Fruit);
                Debug.Log("Got Fruit");

            }
            else
            {
                player.SetItem(PlayerScript.ItemType.Wood);
                Debug.Log("Got Wood");
            }
        }

        else if (nearObj.CompareTag("Treasure"))
        {
            player.SetItem(PlayerScript.ItemType.Coin);
            Debug.Log("Got Coin");

        }

        else if (nearObj.CompareTag("Well"))
        {
            player.EquipBucket();
            player.SetItem(PlayerScript.ItemType.Water);
            Debug.Log("Got Water");

        }
    }

    private void OnTriggerEnter2D(Collider2D other) //set near object to the closest tagged obg
    {
        if (other.CompareTag("Tree") || other.CompareTag("Treasure") || other.CompareTag("Well"))
        {
            nearObj = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == nearObj)
        {
            nearObj = null;
        }
    }
}
