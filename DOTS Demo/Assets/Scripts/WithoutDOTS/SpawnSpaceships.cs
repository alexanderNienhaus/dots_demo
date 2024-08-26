using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSpaceships : MonoBehaviour
{
    [SerializeField] private GameObject spaceshipPrefab;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private int amountToSpawn;

    private List<GameObject> spaceshipInstanceList;
    private int spawnRange = 950;

    private void Start()
    {
        spaceshipInstanceList = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                TrySpawn();
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && spaceshipInstanceList.Count >= amountToSpawn)
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                Destroy(spaceshipInstanceList[spaceshipInstanceList.Count - 1]);
                spaceshipInstanceList.RemoveAt(spaceshipInstanceList.Count - 1);
            }
        }
        textField.SetText("Number of Spaceships: " + spaceshipInstanceList.Count);
    }

    private void TrySpawn()
    {
        Vector3 randomPosInRange = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange));

        if (!Physics.CheckBox(randomPosInRange, spaceshipPrefab.GetComponent<BoxCollider>().bounds.extents / 2 * spaceshipPrefab.transform.localScale.x))
        {
            GameObject spaceShipInstance = Instantiate(spaceshipPrefab, randomPosInRange, Quaternion.identity);
            spaceshipInstanceList.Add(spaceShipInstance);

        } else
        {
            TrySpawn();
        }
    }
}
