using UnityEngine;
using TMPro;
using Unity.Entities;

public class SpaceshipCount : MonoBehaviour
{
    public SpaceshipCountChangeEvent spaceshipCountChangeEvent;

    [SerializeField] private TextMeshProUGUI textField;

    private int spaceShipCount = 0;

    private void Start()
    {
        textField.SetText("Number of spaceships: " + spaceShipCount);

        SpawnSpaceshipSystem spawnSpaceshipSystem =
            World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SpawnSpaceshipSystem>();

        if (spawnSpaceshipSystem.onSpaceshipCountChange == null)
            spawnSpaceshipSystem.onSpaceshipCountChange = new SpaceshipCountChangeEvent();

        spawnSpaceshipSystem.onSpaceshipCountChange.AddListener(SpaceshipCountChange);
    }

    private void SpaceshipCountChange(int pChangeAmount)
    {
        spaceShipCount += pChangeAmount;
        textField.SetText("Number of spaceships: " + spaceShipCount);
    }
}
