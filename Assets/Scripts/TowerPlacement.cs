using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Tilemap buildableTilemap;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject towerPrefab;

    private void Awake()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    private void TryPlaceTower()
    {
        if (mainCamera == null
            || gridManager == null
            || buildableTilemap == null
            || towerPrefab == null)
        {
            Debug.LogError("TryPlaceTower requirements not met");
            return;
        }

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        Vector3Int cell = gridManager.WorldToCell(mouseWorld);

        if (!buildableTilemap.HasTile(cell) || gridManager.IsOccupied(cell))
        {
            return;
        }

        if(gridManager.TryOccupyCell(cell))
        {
            Vector3 position = gridManager.GetCellCenter(cell);
            Instantiate(towerPrefab, position + Vector3.back, Quaternion.identity);
        }
    }
}
