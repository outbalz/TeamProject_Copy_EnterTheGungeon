using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CMapSpawnTest : MonoBehaviour
{
    [SerializeField]
    GameObject _spwaner;
   //[SerializeField]
   //GameObject _room1;
   //[SerializeField]
   //GameObject _room2;

    [SerializeField]
    GameObject[] _rooms;

    GameObject _currentRoom;
    Tilemap _currentTilemap;

    string _name = "DoorSpawnSite";
    [SerializeField]
    int _seed = 100;
    Transform ranDoor = null;
    Collider2D _colRoom = null;
    private void Start()
    {

        
        
        //½Ãµå
        //Random.InitState(_seed);
        if (ranDoor == null)
        {
            ranDoor = GetComponent<Transform>();
        }
        if (_colRoom == null)
        {
            _colRoom = GetComponent<Collider2D>();
        }

        
        for (int i = 0; i < _rooms.Length; i++)
        {
            
            if(_colRoom == null)
            {
                _currentRoom = Instantiate(_rooms[i], ranDoor.position , ranDoor.rotation, _spwaner.transform);
            }
            else
            {
                _currentRoom = Instantiate(_rooms[i], ranDoor.position + (ranDoor.position - _colRoom.bounds.center), ranDoor.rotation, _spwaner.transform);
            }
                
            _colRoom = _currentRoom.GetComponentInChildren<TilemapCollider2D>();
            _currentTilemap = _colRoom.GetComponent<Tilemap>();

            while (true)
            {
                int ranNum = Random.Range(1, 10);
                Debug.Log(ranNum);
                string _nameNum = _name + ranNum;

                
                ranDoor = _currentRoom.transform.Find(_nameNum);

                if (ranDoor != null)
                {
                    break;
                }
            }
            _currentTilemap.SetTile(_currentTilemap.LocalToCell(ranDoor.position),null); 
        }
    }

 
}
