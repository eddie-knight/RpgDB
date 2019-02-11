using UnityEngine;
using RpgDB;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace RpgDB
{
    public class CharacterSave : MonoBehaviour
    {
        public RpgDB.Character character;

        public static void Save(Character player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.dataPath + "/player.sav", FileMode.Create);

            CharacterSaveData data = new CharacterSaveData(player);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public void Load()
        {
            if (File.Exists(Application.dataPath + "/player.sav"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(Application.dataPath + "/player.sav", FileMode.Open);

                CharacterSaveData data = formatter.Deserialize(stream) as CharacterSaveData;
                character = data.LoadSaveData();
                stream.Close();
            }
            else
                Debug.Log("Save File Not Found.");
        }

        public void Debugger()
        {
            Debug.Log(character.Class.Name);
        }

    }
}