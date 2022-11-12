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
    public List<Rectangle> collisionLayer = new List<Rectangle>();
    
    private Random random = new Random((int)DateTime.Now.Ticks);


    public LevelGenerator(int maxRooms, int minRooms)
    {
        this.maxRooms = maxRooms;
        this.minRooms = minRooms;
    }

    public void generateLevel()
    {
        //Build start room
        var startMapName = "start" + random.Next(1,possibleStarts).ToString("000");
        
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

            Door currentDoor;
            
            if(openDoors.Count> 0){
                currentDoor = openDoors.Peek();
            }
            else
            {
                break;
            }
            
            var tryRoomNumber = random.Next(0, possibleRooms-1);
            var tryRoomName = allRooms[tryRoomNumber];

            var tryMap = ContentLoader.Tilemaps[tryRoomName];

            Console.Write(trys++ + ": Generating..." + tryRoomName);
            
            if (RoomsCanConnect(currentDoor, tryMap))
            {
                var newRenderPos = CalculateRenderPos(currentDoor, tryMap);
                var newRoom = new Room(tryRoomName, newRenderPos);
                
                if (!DoesIntersect(newRoom))
                {
                    rooms.Add(newRoom);
                    AddToCollisionLayer(newRoom);
                    
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
                    openDoors.Dequeue();
                }
                
            }
        }
        Console.Write("Done");
    }

    private void AddToCollisionLayer(Room room)
    {
        foreach (var rect in room.CollisionLayer)
        {
            collisionLayer.Add(rect);
        }
    }

    private void AddToCollisionLayer(Rectangle rectangle)
    {
        collisionLayer.Add(rectangle);
    }

    private bool RoomsCanConnect(Door exitDoor, TiledMap connectingMap)
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
    
    private bool DoesIntersect(Room newRoom)
    {
        if (!rooms.Any(room => room.Rectangle.Intersects(newRoom.Rectangle))) return false;
        
        Console.Write("Failed: Rooms intersect \n");
        return true;

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