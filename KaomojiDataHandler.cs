using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;

namespace KaomojiSharp {
    public class KaomojiDataHandler {
        public static bool IsLoaded { get; private set; } = false;

        /// <summary>
        /// Loads from the default file KaomojiData.json in the executable directory.
        /// If this data file is moved, use Load(string path).
        /// By default, if IsLoaded is false, this function is called when a get-kaomoji function is called.
        /// </summary>
        /// <returns>Load success.</returns>
        public static void Load() {
            Load("KaomojiData.json");
        }

        /// <summary>
        /// Loads a KaomojiSharp data JSON file from the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void Load(string path) {
            using (StreamReader file = File.OpenText(path)) {
                JsonSerializer serializer = new JsonSerializer();
                DataSet dataSet = (DataSet)serializer.Deserialize(file, typeof(DataSet));

                DataTable dataTable = dataSet.Tables["Kaomoji"];

                foreach (DataRow row in dataTable.Rows) {
                    long[] rawCategories = (long[])row["Categories"];

                    //Create a new KaomojiCategory array with the same length as rawCategories.
                    KaomojiFlags.Category[] categories = new KaomojiFlags.Category[rawCategories.Length];
                    
                    for(int i = 0; i < categories.Length; i++) {
                        //Get an integer representation of the long value.
                        int rawCategory = (int)rawCategories[i];

                        //Check if the integer value is definited in KaomojiCategory
                        if(!Enum.IsDefined(typeof(KaomojiFlags.Category), rawCategory)) {
                            throw new Exception($"Category is invalid at emoticon {row["Categories"]}");
                        }

                        //Set the enum value based on the integer.
                        categories[i] = (KaomojiFlags.Category)rawCategory;
                    }

                    new Kaomoji((string)row["Emoticon"], new KaomojiFlags(categories)).Register();
                }
            }
            IsLoaded = true;
        }
    }
}
