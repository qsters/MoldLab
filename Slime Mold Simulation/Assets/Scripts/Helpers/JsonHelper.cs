using UnityEngine;


namespace Helpers
{
    public class JsonHelper
    {
        public static bool CheckIfSaved()
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/HasBeenSaved.json"))
            {
                string jsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/HasBeenSaved.json");
            
                SaveData saved = JsonUtility.FromJson<SaveData>(jsonData);
                return saved.saved;
            }
            return false;
        }
        
        public static void UpdateData(bool saved)
        {
            SaveData saveData = new SaveData();
            saveData.saved = saved;
            string jsonData = JsonUtility.ToJson(saveData);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/HasBeenSaved.json", jsonData);
        }
    }
}