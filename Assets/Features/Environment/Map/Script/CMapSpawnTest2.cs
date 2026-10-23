using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class CMapSpawnTest2 : MonoBehaviour
{
      
    [SerializeField]
    GameObject[] _fromGrid;
    
    [SerializeField]
    GameObject _toGrid;

    [SerializeField]
    GameObject _pathGrid;

    Tilemap[] _tilemapBase;
    Tilemap[] _tilemapPath; 

    Vector3Int _moveVector;

    Tilemap[] tilemaps;
    void Start()
    { 
        _tilemapBase = _toGrid.GetComponentsInChildren<Tilemap>();
        _tilemapPath = _pathGrid.GetComponentsInChildren<Tilemap>(); 
    }

    void MakeTile()
    { 
        for (int j = 0; j < _fromGrid.Length; j++)
        {
            tilemaps = _fromGrid[j].GetComponentsInChildren<Tilemap>();

            for (int i = 0; i < tilemaps.Length; i++)
            {
                tilemaps[i].CompressBounds();

                foreach (var pos in tilemaps[i].cellBounds.allPositionsWithin)
                {
                    TileBase tile = tilemaps[i].GetTile(pos);
                    _tilemapBase[i].SetTile(pos + _moveVector, tile);

                }

            }

            Tilemap walltile = _fromGrid[j].transform.Find("Wall").GetComponent<Tilemap>();

            walltile.CompressBounds();


            _moveVector += walltile.cellBounds.max + new Vector3Int(-4, (int)walltile.cellBounds.center.y, 0);


            MakePath();

            _moveVector += new Vector3Int((int)_tilemapPath[0].cellBounds.max.x - 4, -(int)walltile.cellBounds.center.y, 0);

        }
    }
    void MakePath()
    {
        for (int i = 0; i < _tilemapPath.Length; i++)
        {
            _tilemapPath[i].CompressBounds();

            foreach (var pos in _tilemapPath[i].cellBounds.allPositionsWithin)
            {

                TileBase tile = _tilemapPath[i].GetTile(pos);


                _tilemapBase[i].SetTile(pos + _moveVector, tile);

            }

        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            MakeTile();
        }
    }


}
