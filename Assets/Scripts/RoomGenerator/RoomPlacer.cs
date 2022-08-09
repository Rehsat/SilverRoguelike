using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private Room[] _roomPrefabs;
    [SerializeField] private Transform _player;
    [SerializeField] private Room _firstRoom;
    [SerializeField] private Room _bossRoom;
    [SerializeField] private Room _shopRoom;

    private List<Room> spawnedRooms = new List<Room>();

    private void Start()
    {
        spawnedRooms.Add(_firstRoom);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var randomRoom = _roomPrefabs[Random.Range(0, _roomPrefabs.Length)];
                PlaceRoom(randomRoom);
            }
            PlaceRoom(_shopRoom);
        }
        PlaceRoom(_bossRoom);
    }
    private void PlaceRoom(Room room)
    {
        var randomRoom = _roomPrefabs[Random.Range(0, _roomPrefabs.Length)];
        var newRoom = Instantiate(room);

        var previousRoomExitPosition = spawnedRooms[spawnedRooms.Count - 1].Exit.position;
        newRoom.transform.position = previousRoomExitPosition - newRoom.Begin.position;

        spawnedRooms.Add(newRoom);
    }
}
