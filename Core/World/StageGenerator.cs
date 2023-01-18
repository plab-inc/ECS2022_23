using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using TiledCS;

namespace ECS2022_23.Core.World;

internal static class StageGenerator
{
    private static readonly int PossibleStarts = Directory.GetFiles("Content/world/rooms","start*.xnb").Length;
    private static readonly Random Random = new((int)DateTime.Now.Ticks);
    
    public static Stage GenerateStage(int minimumRooms, int maximumRooms)
    {
        List<Room> rooms = new();
        List<Rectangle> groundLayer = new();
        List<Rectangle> waterLayer = new();
        List<Door> deadEndDoors = new();
        Room bossRoom;

        var triesToGenerateLevel = 0;
        var bossRoomGenerations = 1;
        var roomGenerations = 1;
        
        Debug.WriteLine("Level generation started.");
        do
        {
            triesToGenerateLevel++;
            
            rooms.Clear();
            deadEndDoors.Clear();
            groundLayer.Clear();
            waterLayer.Clear();
            
            ConnectRooms(maximumRooms, rooms, groundLayer, waterLayer, deadEndDoors);
            bossRoom = CreateBossRoom(deadEndDoors,rooms);
            
            if (bossRoom != null)
            {
                rooms.Add(bossRoom);
                groundLayer.AddRange(bossRoom.GroundLayer);
                groundLayer.AddRange(bossRoom.WaterLayer);
                waterLayer.AddRange(bossRoom.WaterLayer);
            }
            
            //For debugging
            if (bossRoom == null)
            {
                bossRoomGenerations++;
            }
            if (rooms.Count < minimumRooms)
            {
                roomGenerations++;
            }
            
        } while (rooms.Count < minimumRooms || bossRoom == null);
        
        Debug.WriteLine("Level generation successful after {0} tries.\n" +
                          "{1} Tries to generate rooms\n" +
                          "{2} Tries to generate boosroom\n" +
                          "Closing doors now.",triesToGenerateLevel,roomGenerations,bossRoomGenerations);
        
        foreach (var deadEndDoor in deadEndDoors)
        {
            var rect = groundLayer.Find(x => x.Contains(deadEndDoor.Position));
            groundLayer.Remove(rect);
            CloseDoor(deadEndDoor);
        }
        
        Debug.WriteLine("Open doors closed. \nDone!");

        
        return new Stage(rooms, groundLayer, waterLayer);
    }
    private static Room CreateBossRoom(List<Door> openDoors,List<Room> rooms)
    {
        
        foreach (var (key, map) in WorldLoader.Tilemaps)
        {
            
            if (!key.Contains("boss")) continue;
            
            foreach (var door in openDoors)
            {
                Debug.Write("Finding matching Bossdoor:");
                if (!CanRoomsConnect(door, map)) continue;
                
                    Debug.Write(" Sucess! \nTest if there is enough space:");
                    var renderPos = CalculateNewRenderPos(door, map);

                    if (DoRoomsIntersect(rooms, renderPos, map)) continue;
                        openDoors.Remove(door);
                        Debug.WriteLine(" Sucess! \nBossroom generation successful");
                        return new Room(key, renderPos);
            }
        }
        Debug.WriteLine("Bossroom generation failed");
        return null;
    }
    private static void ConnectRooms(int maximumRooms, List<Room> rooms, List<Rectangle> groundLayer, List<Rectangle> waterLayer, List<Door> deadEndDoors)
    {
        var startMapName = "start" + Random.Next(PossibleStarts).ToString("000");
        var start = new Room(startMapName, new Point(0, 0));
        groundLayer.AddRange(start.GroundLayer);
        groundLayer.AddRange(start.WaterLayer);
        waterLayer.AddRange(start.WaterLayer);
        
        rooms.Add(start);
        
        var openDoors = new Queue<Door>(start.Doors);
        var generatedRooms = 0;
        var trys = 0;

        var maps = WorldLoader.Tilemaps;
        
        while (generatedRooms < maximumRooms)
        {
            Door currentDoor;
            
            if(openDoors.Count > 0){
                currentDoor = openDoors.Peek();
            }else
            {
                break;
            }

            var map = maps.Where(pair => pair.Key.Contains("room"))
                .ElementAt(Random.Next(0, maps.Count(pair => pair.Key.Contains("room"))));
            
            Debug.Write(trys++ + ": Generating..." + map.Key);

            if (!CanRoomsConnect(currentDoor, map.Value)) continue;
            
            var renderPos = CalculateNewRenderPos(currentDoor, map.Value);
            
            if (!DoRoomsIntersect(rooms,renderPos,map.Value))
            {
                var newRoom = new Room(map.Key, renderPos);
                rooms.Add(newRoom);
                groundLayer.AddRange(newRoom.GroundLayer);
                groundLayer.AddRange(newRoom.WaterLayer);
                waterLayer.AddRange(newRoom.WaterLayer);
                    
                foreach (var newRoomDoor in newRoom.Doors)
                {
                    if (newRoomDoor.Direction != (~currentDoor.Direction + 1))
                    {
                        openDoors.Enqueue(newRoomDoor);
                    }
                }
                generatedRooms++;
                openDoors.Dequeue();
                Debug.Write(" Success!\n");
            }
            else
            {
                deadEndDoors.Add(openDoors.Dequeue());
            }
        }
        deadEndDoors.AddRange(openDoors);
    }
    private static bool CanRoomsConnect(Door exitDoor, TiledMap connectingMap)
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

        Debug.Write(" Failed: Rooms can't connect \n");
        return false;

    }
    private static bool DoRoomsIntersect(List<Room> rooms, Point renderPos, TiledMap map)
    {
        Rectangle rect = new(renderPos.X, renderPos.Y, map.Width * map.TileHeight, map.Height * map.TileHeight);
        
        if (!rooms.Any(room => room.Rectangle.Intersects(rect))) return false;
        
        Debug.Write(" Failed: Rooms would intersect \n");
        
        return true;

    }
    private static void CloseDoor(Door door)
    {
        const string layer = "walls";
        var room = door.Room;
        var roomHeight = room.Map.Height;
        var roomWidth = room.Map.Width;
        
        //Calculate positions on the plain map based on the room/door cordinates
        var x = (door.Position.X - room.Position.X) / room.Map.TileWidth;
        var y = (door.Position.Y - room.Position.Y) / room.Map.TileHeight;
        
        switch (door.Direction)
        {
            //Hell
            case Direction.Up:
                for (int i = 0; i < 3; i++)
                { 
                    room.ChangeTile(x - 1 + i , y, 2, layer);
                    room.ChangeTile(x - 1 + i , y+1, 14, layer);
                    room.ChangeTile(x - 1 + i , y+2, 14, layer);
                    
                    if (roomHeight > 8)
                    {
                        room.ChangeTile(x - 1 + i , y+3, 20, layer);
                    }
                    
                }
                if (x == roomWidth - 2)
                {
                    for (int i = 0; i < 4; i++)
                    { 
                        room.ChangeTile(x + 1 , i, 16, layer);
                    }
                    room.ChangeTile(roomWidth - 1 , 0, 3, layer);
                    room.ChangeTile(x , 3, 20, layer);
                }
                if (x == 1) //door on most left corner
                {
                    room.ChangeTile(0 , 0, 0, layer);
                    room.ChangeTile(0 , 1, 13, layer);
                    room.ChangeTile(0 , 2, 13, layer);

                    //Shadows
                    room.ChangeTile(1,3,22,layer);
                    room.ChangeTile(2,3,20,layer);
                }
                break;
            case Direction.Left:
                
                for (int i = 0; i < 5; i++)
                {
                    room.ChangeTile(x,y + 1 - i,13,layer);
                }
                if (y < 4) 
                {
                    //door on highest corner
                    room.ChangeTile(0,0,0,layer);
                    room.ChangeTile(1,3,22,layer);
                    room.ChangeTile(1,4,35,layer);
                }else if (y == roomHeight - 2)
                {
                    //door on lowest corner
                    room.ChangeTile(x,roomHeight - 1,39,layer);
                    room.ChangeTile(x+1,roomHeight - 1,40,layer);
                    room.ChangeTile(x+1,roomHeight - 2,35,layer);
                }
                else
                {
                    room.ChangeTile(x+1,y,35,layer);
                    room.ChangeTile(x+1,y+1,35,layer);

                    if (roomHeight > 7)
                    { 
                        room.ChangeTile(x+1,y-1,35,layer);
                    }
                }
                break;
            case Direction.Right:
                
                for (int i = 0; i < 5; i++)
                {
                    room.ChangeTile(x,y + 1 - i,16,layer);
                }

                if (y < 4)
                { 
                    //door on highest corner
                    room.ChangeTile(x,y-3,3,layer);
                    room.ChangeTile(x,y+1,42,layer); 
                }
                if (y > roomHeight - 3)
                { 
                    //door on lowest corner
                    room.ChangeTile(x,roomHeight-1,42,layer); 
                }
                break;
            case Direction.Down:
                
                for (int i = 0; i < 3; i++)
                { 
                    room.ChangeTile(x - 1 + i , y, 40, layer);
                }

                if (x >= roomWidth - 2)
                {
                    room.ChangeTile(roomWidth - 1 , roomHeight - 1, 42, layer);
                }

                if (x < 2 && room.GetTileGid(0, roomHeight -2, layer) != 20)
                {
                    room.ChangeTile(0 , roomHeight-1, 39, layer);
                }
                
                break;
        }
    }
    private static Point CalculateNewRenderPos(Door exitDoor, TiledMap connectingMap)
    {
        int connectingDoorX;
        int connectingDoorY;
        int renderPosX;
        int renderPosY;
        
        var connectingMapDoors = connectingMap.Layers.First(layer => layer.name == "Doors").objects;
        
        switch (exitDoor.Direction)
        {
            case Direction.Up:
                
                connectingDoorX = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Down").x / connectingMap.TileWidth) * connectingMap.TileWidth;
                
                renderPosX = exitDoor.Position.X - connectingDoorX;
                renderPosY = exitDoor.Room.Position.Y - connectingMap.Height * connectingMap.TileHeight;
                
                break;
                            
            case Direction.Down:

                connectingDoorX = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Up").x / connectingMap.TileWidth) * connectingMap.TileWidth;
                
                renderPosX = exitDoor.Position.X - connectingDoorX;
                renderPosY = exitDoor.Room.Position.Y + exitDoor.Room.Height;
                
                break;
            
            case Direction.Right:

                connectingDoorY = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Left").y / connectingMap.TileHeight) * connectingMap.TileWidth;

                renderPosY = exitDoor.Position.Y - connectingDoorY; 
                renderPosX = exitDoor.Room.Position.X + exitDoor.Room.Width;

                break;
                            
            case Direction.Left:
                
                connectingDoorY = (int) Math.Floor(connectingMapDoors.First(door => door.name == "Right").y / connectingMap.TileHeight) * connectingMap.TileWidth;
                
                renderPosY = exitDoor.Position.Y - connectingDoorY;
                renderPosX = exitDoor.Room.Position.X - connectingMap.Width * connectingMap.TileWidth;
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return new Point(renderPosX, renderPosY);

    }
}