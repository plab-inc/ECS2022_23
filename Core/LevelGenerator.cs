using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS2022_23.Core;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using TiledCS;

namespace MonoGameLevelGenerator.Core;

static class LevelGenerator
{
    private static int possibleStarts = Directory.GetFiles("Content/rooms","start*.xnb").Length;
    private static readonly Random _random = new((int)DateTime.Now.Ticks);
    

    public static Level GenerateLevel(int minRooms, int maxRooms)
    {
        List<Room> rooms = new();
        List<Rectangle> collisionLayer = new();
        List<Door> deadEndDoors = new();
        
        //Build start room
        var startMapName = "start" + _random.Next(possibleStarts).ToString("000");
        Room start = new Room(startMapName, new Point(0, 0));
        
        collisionLayer.AddRange(start.CollisionLayer);
        rooms.Add(start);

        //Get all roomnames in rooms folder
        var roomNames = GetAllRoomNames();
        
        //Que of open doors
        var openDoors = new Queue<Door>(start.Doors);
        
        var generatedRooms = 0;
        var trys = 0;
        
        while (generatedRooms < maxRooms)
        {

            Door currentDoor;
            
            if(openDoors.Count > 0){
                currentDoor = openDoors.Peek();
            }else
            {
                break;
            }

            if (roomNames.Count == 0)
            {
                roomNames = GetAllRoomNames();
            }

            
            var tryRoomNumber = _random.Next(0, roomNames.Count);
            var tryRoomName = roomNames[tryRoomNumber];
            roomNames.RemoveAt(tryRoomNumber);
            
            
            var tryMap = ContentLoader.Tilemaps[tryRoomName];

            Console.Write(trys++ + ": Generating..." + tryRoomName);
            
            if (RoomsCanConnect(currentDoor, tryMap))
            {
                var newRenderPos = CalculateRenderPos(currentDoor, tryMap);
                var newRoom = new Room(tryRoomName, newRenderPos);
                
                if (!RoomsIntersect(rooms,newRoom))
                {
                    rooms.Add(newRoom);
                    collisionLayer.AddRange(newRoom.CollisionLayer);
                    
                    foreach (var newRoomDoor in newRoom.Doors)
                    {
                        if (newRoomDoor.Direction != (~currentDoor.Direction + 1))
                        {
                            openDoors.Enqueue(newRoomDoor);
                        }
                    }
                    generatedRooms++;
                    openDoors.Dequeue();
                    Console.Write(" Success \n");
                }
                else
                {
                    deadEndDoors.Add(openDoors.Dequeue());
                }
                
            }
        }
        Console.Write("Done");
        deadEndDoors.AddRange(openDoors);
        Console.Write(deadEndDoors.Count);

        foreach (var deadEndDoor in deadEndDoors)
        {
            var rect = collisionLayer.Find(x => x.Contains(deadEndDoor.marker));
            collisionLayer.Remove(rect);
        }
        
        
        return new Level(rooms, collisionLayer);
    }
    
    private static bool RoomsCanConnect(Door exitDoor, TiledMap connectingMap)
    {
        var exitDoorOpposite = ~exitDoor.Direction+1;
        var connectingDoors = connectingMap.Layers.First(layer => layer.name == "Doors").objects;

        foreach (var connectingDoor in connectingDoors)
        {
            var connectingDoorDirection = Enum.Parse<Direction>(connectingDoor.name);
            
            if (connectingDoorDirection == exitDoorOpposite)
            {
                return true;
            }
        }

        Console.Write(" Failed: Rooms can't connect \n");
        return false;

    }
    private static bool RoomsIntersect(List<Room> rooms, Room newRoom)
    {
        if (!rooms.Any(room => room.Rectangle.Intersects(newRoom.Rectangle))) return false;
        
        Console.Write("Failed: Rooms intersect \n");
        return true;

    }
    
    private static List<string> GetAllRoomNames()
    {
        //TODO Maybe move this into Contentmanager
        
        var roomNames = Directory.GetFiles("Content/rooms", "room*.xnb").
            Select(Path.GetFileNameWithoutExtension).ToList();
        
        return roomNames;
    }
    
    private static Point CalculateRenderPos(Door exitDoor, TiledMap connectingMap)
    {
        int connectingDoorX;
        int connectingDoorY;
        int renderPosX;
        int renderPosY;
        
        var connectingMapDoors = connectingMap.Layers.First(layer => layer.name == "Doors").objects;
        
        switch (exitDoor.Direction)
        {
            case Direction.Up:
                
                connectingDoorX = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Down").x / 16);
                
                renderPosX =  exitDoor.x - connectingDoorX;
                renderPosY = exitDoor.room.Position.Y - connectingMap.Height;
                
                break;
                            
            case Direction.Down:

                connectingDoorX = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Up").x / 16);
                
                renderPosX = exitDoor.x - connectingDoorX;
                renderPosY = exitDoor.room.Position.Y + exitDoor.room._map.Height;
                
                break;
            
            case Direction.Right:

                connectingDoorY = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Left").y / 16);

                renderPosY = exitDoor.y - connectingDoorY; 
                renderPosX = exitDoor.room.Position.X + exitDoor.room._map.Width;

                break;
                            
            case Direction.Left:
                
                connectingDoorY = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Right").y / 16);
                
                renderPosY = exitDoor.y - connectingDoorY;
                renderPosX = exitDoor.room.Position.X - connectingMap.Width;
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new Point(renderPosX, renderPosY);

    }
    
}