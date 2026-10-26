using UnityEngine;
using TMPro;
using System.ComponentModel;

public class VillagerScript : MonoBehaviour
{
    // Villager is highlighted in the scene using light2d so teh player knows where to go to help someone
    //villager should hover close to the houses with "house" tag and destroy after the player gives them the item they wanted
    // when the player collides with the villager (both have tags) the villager says what item they want (treasure [coins], tree [wood or fruit], well [water])
    // if its treasure or tree then the player keeps their pickaxe, if its the well then the pickaxe tool changes to a bucket sprite that should be a child under the player

    public PlayerScript.ItemType wantedItem;
    public TextMeshProUGUI requestText;
    private bool playerNearby;
    private PlayerScript player;
    private VillagerManager villagerManager;

    void Start()
    {

        villagerManager = FindAnyObjectByType<VillagerManager>();

        int randomRequest = Random.Range(0, 4);
        switch (randomRequest) 
        { 
            case 0:
                wantedItem = PlayerScript.ItemType.Fruit; break;

            case 1:
                wantedItem = PlayerScript.ItemType.Wood; break;

            case 2:
                wantedItem= PlayerScript.ItemType.Coin;break;

            case 3:
                wantedItem = PlayerScript.ItemType.Water; break;
        }
        requestText.text = "Need: " + wantedItem;
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.D))
        {
            GiveItem();
        } 
    }

    void GiveItem()
    {
        if (player.currentItem == wantedItem)
        {
            Debug.Log("Villager helped");
            player.currentItem = PlayerScript.ItemType.None;
            player.EquipPickaxe();
            villagerManager.SpawnVillager();

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Wrong Item");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.GetComponent<PlayerScript>();
            requestText.text = "Excuse me Chief, please help. I need " + wantedItem;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}

