using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string FilePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "TheRoom.fun");
        }
    }
    public static void SaveGame(Transform player, Transform room)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        SaveData data = new SaveData(player, room);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static SaveData LoadGame()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + FilePath);
            return null;
        }
    }

}
