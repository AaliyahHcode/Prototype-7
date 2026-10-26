using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnVillager();
    }

    // Update is called once per frame
    public void SpawnVillager()
    {
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        GameObject randomHouse = houses[Random.Range(0, houses.Length)];
        Vector3 spawnPos = randomHouse.transform.position + new Vector3(1f, 0f, 0f);

        Instantiate(villagerPrefab, spawnPos, Quaternion.identity);
    }
}
