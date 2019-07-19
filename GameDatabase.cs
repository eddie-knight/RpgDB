using System.IO;
using UnityEngine;
using UnityEditor;

namespace RpgDB
{
    public sealed class GameDatabase : MonoBehaviour
    {
        [HideInInspector] public WeaponDatabase Weapons;
        [HideInInspector] public AmmunitionDatabase Ammunition;
        [HideInInspector] public ArmorDatabase Armor;
        [HideInInspector] public UpgradeDatabase Upgrades;
        [HideInInspector] public ClassDatabase Classes;
        [HideInInspector] public ExtensionsDatabase Extensions;

        [SerializeField]//set in Inspector
        private DatabaseEntry[] spriteDatabase = new DatabaseEntry[0];

        [SerializeField]//set in Inspector
        private DatabaseEntry[] soundDatabase = new DatabaseEntry[0];

#if UNITY_EDITOR

        [SerializeField]
        private string TESTFILEPATH = "Assets/Art/Leader Icons";

        private const string metaFileExtension = ".meta";
#endif

        void Awake()
        {
            Weapons = gameObject.AddComponent(typeof(WeaponDatabase)) as WeaponDatabase;
            Ammunition = gameObject.AddComponent(typeof(AmmunitionDatabase)) as AmmunitionDatabase;
            Armor = gameObject.AddComponent(typeof(ArmorDatabase)) as ArmorDatabase;
            Upgrades = gameObject.AddComponent(typeof(UpgradeDatabase)) as UpgradeDatabase;
            Extensions = gameObject.AddComponent(typeof(ExtensionsDatabase)) as ExtensionsDatabase;
            Classes = gameObject.AddComponent(typeof(ClassDatabase)) as ClassDatabase;
        }

        private void Start()
        {
            CheckForValidEntries(spriteDatabase);
            CheckForValidEntries(soundDatabase);
        }

        private void OnValidate()
        {
            GenerateKeysForBlanks(spriteDatabase);
            GenerateKeysForBlanks(soundDatabase);
        }

        private static void GenerateKeysForBlanks(DatabaseEntry[] database)
        {
            for(var i = 0; i < database.Length; ++i)//look through each entry
            {
                if (database[i].key.Equals(""))//if key is blank
                {
                    Debug.Log("Generating a new hash for empty key");
                    database[i] = new DatabaseEntry(DatabaseEntry.GenerateNewKeyHash(), database[i].Entry);//generate a new entry with a usable hash
                }
            }
        }

        /// <summary>
        /// Log to Console if duplicates or null values exist.
        /// </summary>
        /// <param name="database"></param>
        private static void CheckForValidEntries(DatabaseEntry[] database)
        {
            //compare all against the other
            for (int i = 0; i < database.Length - 1; ++i)
            {
                if(database[i].Entry == null)//check for null, while we're here.
                {
                    Debug.LogError("ERROR! Null reference exists in database at index: " + i.ToString());
                    break;
                }

                for(int j = database.Length - 1; j > i; --j)
                {
                    if (database[i].key.Equals(database[j].key))
                    {
                        Debug.LogError("ERROR! Multiple entries in database with same key: " + database[i].key);
                    }

                    if (database[i].Compare(database[j]))
                    {
                        Debug.Log("NOTE: Multiple Entries with different keys present in database!", database[i].Entry);
                    }
                }
            }
            
            if(database.Length > 0 && database[0].Entry == null) Debug.Log("ERROR! Null reference exists in database!");//check first for null
            else if(database.Length > 1 && database[database.Length -1].Entry == null) Debug.Log("ERROR! Null reference exists in database!");//check last for null
        }

        /// <summary>
        /// Get a Sprite from the dictionary using given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Sprite or null if key not in dictionary.</returns>
        public Sprite GetSprite(string key)
        {
            foreach (var entry in spriteDatabase)
            {
                if (key.Equals(entry.key))
                    return (Sprite)entry.Entry;
            }

            //Debug.Log("No Entry with key in database.");
            return null;
        }

        /// <summary>
        /// Get an AudioClip from the dictionary using given key. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>AudioClip or null if key not in dictionary.</returns>
        public AudioClip GetAudioClip(string key)
        {
            foreach (var entry in soundDatabase)
            {
                if (key.Equals(entry.key))
                    return (AudioClip)entry.Entry;
            }

            //Debug.Log("No Entry with key in database.");
            return null;
        }

#if UNITY_EDITOR

        /// <summary>
        /// [ALPHA] Load from Project folder.
        /// </summary>
        /// <param name="folderPath"></param>
        public static DatabaseEntry[] AddToDatabaseFromFolder(DatabaseEntry[] existingDatabase, string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);//get information from specified filepath

            if (!directoryInfo.Exists)
            {
                Debug.LogError("ERROR! No directory at file path: " + folderPath);
                return existingDatabase;//no changes made
            }

            var fileInfoArray = directoryInfo.GetFiles();//get list of files

            if (fileInfoArray.Length == 0)
            {
                Debug.LogError("ERROR! No files located in directory: " + folderPath);
                return existingDatabase;
            }

            var newDatabaseLength = existingDatabase.Length + fileInfoArray.Length / 2;//calculate new size (divide by 2 because of metas)

            var workingDatabase = new DatabaseEntry[newDatabaseLength];//create new array of correct size

            //fill existing
            for(var i = 0; i < existingDatabase.Length; ++i)
            {
                workingDatabase[i] = existingDatabase[i];
            }
            
            //fill array with file info
            for(int i = 0, j = 0, k = existingDatabase.Length; i < fileInfoArray.Length; ++i)
            {
                var filePath = folderPath + "/" + fileInfoArray[i].Name;
                //ignore meta files
                if (FileIsMeta(fileInfoArray[i].Name))
                {
                    continue;
                }

                workingDatabase[j++ + k] = new DatabaseEntry(fileInfoArray[i].Name, AssetDatabase.LoadAssetAtPath(filePath, typeof(Object)));
            }

            CheckForValidEntries(workingDatabase);

            return workingDatabase;
        }

        [ContextMenu("Test load from folder.")]
        private void TESTLOADFROMFILE()
        {
            spriteDatabase = AddToDatabaseFromFolder(spriteDatabase, TESTFILEPATH);
        }

        /// <summary>
        /// Determine if a file name ends in .meta.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool FileIsMeta(string fileName)
        {
            var fileIsMeta = true;
            //check first letters against last 5 letters 
            for (int m = 0, n = fileName.Length - metaFileExtension.Length; m < metaFileExtension.Length; ++m)
            {
                if (metaFileExtension[m] != fileName[m + n])
                {
                    fileIsMeta = false;
                    break;
                }
            }

            return fileIsMeta;
        }

#endif
    }
}