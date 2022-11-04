using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledCS;

namespace MonoGameLevelGenerator.Core;

public class Level
{
    private List<Room> rooms = new List<Room>();
    
    public List<Rectangle> collisionLayer;
    
    public Level(int roomCount)
    {
        generateLevel(roomCount);
    }


    private void generateLevel(int count)
    {
        var roomCount = count;
        TiledMap map = ContentLoader.Tilemaps["room009"];
        var roomCap = roomCount-1;

        Room start = new Room(map, new Point(0, 0));
        collisionLayer = start.CollisionLayer;
        
        rooms.Add(start);
        

        while (roomCap > 0)
        {
            var oldRooms = rooms.ToList();
            
            foreach (var room in oldRooms)
            {
                foreach (var roomDoor in room.Doors)
                {
                    if (roomCap > 0)
                    {
                        var newMap = ContentLoader.Tilemaps["room009"];
                        var newRenderPos = calculateRenderPos(roomDoor, room, newMap);
                        var newRoom = new Room(newMap, newRenderPos);
                        rooms.Add(newRoom);

                        var list1 = collisionLayer;
                        var list2 = newRoom.CollisionLayer;

                        collisionLayer = list1.Concat(list2).ToList();
                        
                        roomCap--;
                    }
                }
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var room in rooms)
        {
            room.Draw(spriteBatch);
        }
    }

    private Point calculateRenderPos(TiledObject exitDoor, Room connectingRoom, TiledMap connectingMap)
    {
        int entryDoorX;
        int entryDoorY;

        var renderPosX = 0;
        var renderPosY = 0;
        
        var connectingMapDoors = connectingMap.Layers.First(layer => layer.name == "Doors").objects;
        
        switch (exitDoor.name.ToLower())
        {
            case "up":
                
                entryDoorX = (int) connectingMapDoors.First(door => door.name == "Down").x / 16;
                
                renderPosX =  (int) (exitDoor.x / 16 - entryDoorX);
                renderPosY = connectingRoom.Position.Y - connectingMap.Height;
                
                break;
                            
            case "down":

                entryDoorX = (int) connectingMapDoors.First(door => door.name == "Up").x / 16;
                
                renderPosX =  (int) (exitDoor.x / 16 - entryDoorX);
                renderPosY = connectingRoom.Position.Y + connectingMap.Height;
                
                break;
            
            case "right":

                entryDoorY = (int) connectingMapDoors.First(door => door.name == "Left").y / 16;
                
                renderPosY = entryDoorY - (int) exitDoor.y / 16;
                renderPosX = connectingRoom.Position.X + connectingRoom._map.Width;

                break;
                            
            case "left":
                
                entryDoorY = (int) connectingMapDoors.First(door => door.name == "Right").y / 16;
                
                renderPosY = entryDoorY - (int) exitDoor.y / 16;
                renderPosX = connectingRoom.Position.X - connectingMap.Width;
                
                break;
        }

        return new Point(renderPosX, renderPosY);

    }
    
}