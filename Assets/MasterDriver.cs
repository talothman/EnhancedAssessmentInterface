using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

namespace eaivr
{
    public class MasterDriver : MonoBehaviour
    {

        enum ExperinmentType { RAY, HAND };
        ExperinmentType experinmentType;

        string pathToDataDirectory;
        string dataFileName;
        string dataFileFullPath;

        float randomValue;

        StreamWriter dataWriter;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            pathToDataDirectory = Application.persistentDataPath + "/user_logs/";
            Randomize();
            SetupDataFile();
            OpenDataFile();
            LoadScene();
            //CloseDataFile();
        }

        public void Randomize()
        {
            randomValue = UnityEngine.Random.value;

            if (randomValue <= 0.5f)
            {
                experinmentType = ExperinmentType.RAY;
                pathToDataDirectory += "ray/";
            }
            else
            {
                experinmentType = ExperinmentType.HAND;
                pathToDataDirectory += "hand/";
            }
        }

        public void SetupDataFile()
        {
            dataFileName = DateTime.Now.ToString("yyyy-dd-M_HH-mm-ss") + ".csv";
            dataFileFullPath = pathToDataDirectory + dataFileName;

            if (!Directory.Exists(pathToDataDirectory))
                Directory.CreateDirectory(pathToDataDirectory);
        }

        void OpenDataFile()
        {
            try
            {
                dataWriter = new StreamWriter(dataFileFullPath);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void CloseDataFile()
        {
            dataWriter.Close();
        }

        void LoadScene()
        {
            if (experinmentType == ExperinmentType.RAY)
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            else
                SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        public void WriteBasicInfo(UserInfo userInfo)
        {
            dataWriter.WriteLine("age, gender, education, previous_comp_exp");
            dataWriter.WriteLine(userInfo.age +"," + userInfo.gender + "," + userInfo.educationLevel + "," + userInfo.knowHow[0]);
            dataWriter.WriteLine("qID, timeTaken, numInteractions, correct");
            //CloseDataFile();
        }

        public void WriteItemResponse(Item item)
        {
            dataWriter.WriteLine(item.questionID + "," + (item.timeEnded - item.timeStart) + "," + item.numOfInteractions
                + "," + item.answeredCorreclty);
        }
    }
}
