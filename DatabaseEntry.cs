using UnityEngine;

namespace RpgDB
{
    /// <summary>
    /// A string: object Dictionary data type that is serializable in Unity.
    /// </summary>
    [System.Serializable]
    public struct DatabaseEntry
    {
        /// <summary>
        /// Typical hash length.
        /// </summary>
        public const int keyLength = 16;

        /// <summary>
        /// Unique hash.
        /// </summary>
        public string key;

        /// <summary>
        /// Reference to the Object that is being tracked by this entry.
        /// </summary>
        [SerializeField]
        private Object entry;//can only be set internally or Inspector.

        /// <summary>
        /// Reference to the Object that is being tracked by this entry.
        /// </summary>
        public Object Entry { get { return entry; } }
        public DatabaseEntry(string key, Object entry)
        {
            this.key = key;
            this.entry = entry;
        }

        /// <summary>
        /// Return a char that is a letter or a digit
        /// </summary>
        /// <returns></returns>
        private static char GetNewLetterOrDigit()
        {
            char newChar = '$';//init to not a letter or digit

            do
            {
                newChar = (char)Random.Range(48, 126);//0 - z
            } while (!char.IsLetterOrDigit(newChar));

            return newChar;
        }

        /// <summary>
        /// Randomly generate a new string hash.
        /// </summary>
        /// <param name="hashLength">Number of characters in hash.</param>
        /// <returns></returns>
        public static string GenerateNewKeyHash(int hashLength = keyLength)
        {
            var outputHash = new System.Text.StringBuilder();

            for (var i = 0; i < hashLength; ++i)
            {
                //get a new ascii character (non-whitespace, control char)
                outputHash.Append(GetNewLetterOrDigit());
            }

            return outputHash.ToString();
        }

        public void GenerateRandomKey()
        {
            key = GenerateNewKeyHash();
        }

        /// <summary>
        /// Do references point to the same Object?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if references point to the same Object.</returns>
        public bool Compare(DatabaseEntry otherEntry)
        {
            return Compare(this, otherEntry);
        }

        /// <summary>
        /// Do references point to the same Object?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if references point to the same Object.</returns>
        public static bool Compare(DatabaseEntry a, DatabaseEntry b)
        {
            return a.Entry.Equals(b.Entry);//do references point to same object?
        }

        /// <summary>
        /// Set entry to a reference to the given object in filepath.
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadObjectReferenceFromFile(string filePath)
        {
            var asset = AssetBundle.LoadFromFile(filePath);
            if(asset == null)
            {
                Debug.LogError("ERROR! no object at specified file path: " + filePath);
            }
            else
            {
                this.entry = asset;
            }

        }

    }
}
