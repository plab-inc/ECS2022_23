using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using TiledCS;

namespace MonoGameLevelGenerator.Core;

public class LevelGenerator
{
    private int maxRooms;
    private int minRooms;

    private int possibleRooms = Directory.GetFiles("Content/rooms", "room*.xnb").Length;
    private int possibleStarts = Directory.GetFiles("Content/rooms","start*.xnb").Length;

    private Room startRoom;

    public List<Room> rooms = new List<Room>();
    
    private Random random = new Random((int)DateTime.Now.Ticks);


    public LevelGenerator(int maxRooms, int minRooms)
    {
        this.maxRooms = maxRooms;
        this.minRooms = minRooms;
    }

    public void generateLevel()
    {
        //Build start room
        //var startMapName = "start" + random.Next(1,possibleStarts).ToString("000");
        var startMapName = "start001";
        
        Room start = new Room(startMapName, new Point(0, 0));
        rooms.Add(start);
        
        //Get all roomnames in rooms folder
        var allRooms = getAllRoomNames().ToArray();
        
        //Que of open doors
        var openDoors = new Queue<Door>(start.Doors);
        
        var generatedRooms = 0;
        var trys = 0;
        
        while (generatedRooms < maxRooms)
        {
            Console.Write(" Generating...Try " + trys++);
            
            var currentDoor = openDoors.Peek();
            
            var tryRoomNumber = random.Next(0, possibleRooms-1);
            var tryRoomName = allRooms[tryRoomNumber];
            
            //var tryRoomName = "room009";
            
            var tryMap = ContentLoader.Tilemaps[tryRoomName];
            
            if (canRoomsConnect(currentDoor, tryMap))
            {
                var newRenderPos = calculateRenderPos(currentDoor, currentDoor.room, tryMap);
                var newRoom = new Room(tryRoomName, newRenderPos);

                openDoors.Dequeue();
                
                if (!doesIntersect(newRoom))
                {
                    rooms.Add(newRoom);
                    foreach (var newRoomDoor in newRoom.Doors)
                    {
                        if (newRoomDoor.Direction != (~currentDoor.Direction + 1))
                        {
                            openDoors.Enqueue(newRoomDoor);
                        }
                    }
                    generatedRooms++;
                    Console.Write(" Success \n");
                }
                
            }
        }
        Console.Write("Done");
    }

    private bool canRoomsConnect(Door exitDoor, TiledMap connectingMap)
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
    
    private bool doesIntersect(Room newRoom)
    {
        foreach (var room in rooms)
        {
            if (room.Rectangle.Intersects(newRoom.Rectangle))
            {
                Console.Write("Failed: Rooms intersect \n");
                return true;
            }
        }

        return false;
        
    }
    
    
    private List<string> getAllRoomNames()
    {
        var roomsWithExtension = Directory.GetFiles("Content/rooms","room*.xnb");
        var roomsWithoutExtension = new List<string>();

        foreach (var room in roomsWithExtension)
        {
            roomsWithoutExtension.Add(Path.GetFileNameWithoutExtension(room));
        }

        return roomsWithoutExtension;
    }
    
    private Point calculateRenderPos(Door exitDoor, Room exitRoom, TiledMap connectingMap)
    {
        int entryDoorX;
        int entryDoorY;

        var renderPosX = 0;
        var renderPosY = 0;
        
        var connectingMapDoors = connectingMap.Layers.First(layer => layer.name == "Doors").objects;
        
        switch (exitDoor.Direction.ToString().ToLower()) //TODO cleanup Enum
        {
            case "up":
                
                entryDoorX = (int) connectingMapDoors.First(door => door.name == "Down").x / 16;
                
                renderPosX =  (int) (exitDoor.x / 16 - entryDoorX);
                renderPosY = exitRoom.Position.Y - connectingMap.Height;
                
                break;
                            
            case "down":

                entryDoorX = (int) connectingMapDoors.First(door => door.name == "Up").x / 16;
                
                renderPosX =  (int) (exitDoor.x / 16 - entryDoorX);
                renderPosY = exitRoom.Position.Y + connectingMap.Height;
                
                break;
            
            case "right":

                entryDoorY = (int) connectingMapDoors.First(door => door.name == "Left").y / 16;

                renderPosY = exitDoor.y / 16 - entryDoorY; 
                renderPosX = exitRoom.Position.X + exitRoom._map.Width;

                break;
                            
            case "left":
                
                entryDoorY = (int) connectingMapDoors.First(door => door.name == "Right").y / 16;
                
                renderPosY = exitDoor.y / 16 - entryDoorY;
                renderPosX = exitRoom.Position.X - connectingMap.Width;
                
                break;
        }

        return new Point(renderPosX, renderPosY);

    }
    
}