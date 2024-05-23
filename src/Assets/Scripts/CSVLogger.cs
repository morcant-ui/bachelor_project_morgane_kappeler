using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
#if WINDOWS_UWP
using Windows.Storage;
#endif

namespace holoutils
{
    /// <summary>
    /// Component that Logs data to a CSV.
    /// Assumes header is fixed.
    /// Copy and paste this logger to create your own CSV logger.
    /// CSV Logger breaks data up into settions (starts when application starts) which are folders
    /// and instances which are files
    /// A session starts when the application starts, it ends when the session ends.
    /// 
    /// In Editor, writes to MyDocuments/SessionFolderRoot folder
    /// On Device, saves data in the Pictures/SessionFolderRoot
    /// 
    /// How to use:
    /// Find the csvlogger
    /// if it has not started a CSV, create one.
    /// every frame, log stuff
    /// Flush data regularly
    /// 
    /// **Important: Requires the PicturesLibrary capability!**
    /// </summary>
    public class CSVLogger : MonoBehaviour

    {
        #region Constants to modify
        private const string DataSuffix = "data";
        private const string CSVHeader1 = "Step, popped sphere, s1_id, s1_size, s1_color, s2_id, s2_size, s2_color, s3_id, s3_size, s3_color, pop time, p1 touch, p2 touch, p3 touch";
        private const string CSVHeader2 = "Step, popped sphere, s1_id, s1_size, s1_color, s1_height, s1_speed, s2_id, s2_size, s2_color, s2_height, s2_speed, s3_id, s3_size, s3_color, s3_height, s3_speed, pop time, p1 touch, p2 touch, p3 touch";
        private const string SessionFolderRoot = "CSVLogger";
        #endregion

        #region private members
        private string m_sessionPath;
        private string m_filePath;
        private string m_recordingId;
        private string m_sessionId;

        private StringBuilder m_csvData;
        #endregion
        #region public members
        public string RecordingInstance => m_recordingId;
        private string scene;
        #endregion

        // Use this for initialization
        // it starts a coroutine
        async void Start()
        {
            StartCoroutine(MNS());
            scene = SceneManager.GetActiveScene().name;
        }

        IEnumerator MNS()
        {
            MakeNewSession();
            yield return null;
        }

        // method reunning asychnously for getting the the folder "Documents" and creating a new folder "CSVLogger"
        // It is automatically called when the CSVLogger is attached to an object.
        // The CSVLogger component has to be linked to the object in the unity editor for avoiding lags and errors when calling the StartNewCSV() method
        async Task MakeNewSession()
        {
            m_sessionId = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            Debug.Log("Hello: " + m_sessionId);
            string rootPath = "";
#if WINDOWS_UWP
            StorageFolder sessionParentFolder = await KnownFolders.DocumentsLibrary
                .CreateFolderAsync(SessionFolderRoot,
                CreationCollisionOption.OpenIfExists);
            rootPath = sessionParentFolder.Path;
#else
            rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SessionFolderRoot);
            if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
#endif
            m_sessionPath = rootPath;
            Directory.CreateDirectory(rootPath);
            Debug.Log("CSVLogger logging data to " + m_sessionPath);
        }

        // called at the beginning of the game, before logging data
        public void StartNewCSV(int sceneNumber)
        {
            m_recordingId = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            var filename = m_recordingId + "-" + scene + "-" + DataSuffix + ".csv";
            m_filePath = Path.Combine(m_sessionPath, filename);
            if (m_csvData != null)
            {
                EndCSV();
            }
            m_csvData = new StringBuilder();
            if (sceneNumber == 1)
            {
                m_csvData.AppendLine(CSVHeader1);
            }
            if (sceneNumber == 2)
            {
                m_csvData.AppendLine(CSVHeader2);
            }
        }

        // called by OnDestroy() or by StartNewCSV() when the m_csvData is not null
        public void EndCSV()
        {
            if (m_csvData == null)
            {
                return;
            }
            using (var csvWriter = new StreamWriter(m_filePath, true))
            {
                csvWriter.Write(m_csvData.ToString());
                csvWriter.Close();
            }
            m_recordingId = null;
            m_csvData = null;
        }

        public void OnDestroy()
        {
            Debug.Log("destroy");
            EndCSV();
        }

        // called when a new line of data has to be stored, it is then saved in a file at the end of the program with FlushData()
        public void AddRow(List<String> rowData)
        {
            AddRow(string.Join(",", rowData.ToArray()));
        }

        public void AddRow(string row)
        {
            m_csvData.AppendLine(row);
        }

        /// <summary>
        /// Writes all current data to current file
        /// </summary>
        public void FlushData()
        {
            using (var csvWriter = new StreamWriter(m_filePath, true))
            {
                csvWriter.Write(m_csvData.ToString());
                csvWriter.Close();
            }
            m_csvData.Clear();
        }

        // not used

        /// <summary>
        /// Returns a row populated with common start data like
        /// recording id, session id, timestamp
        /// </summary>
        /// <returns></returns>
        public List<String> RowWithStartData()
        {
            List<String> rowData = new List<String>();
            rowData.Add(Time.timeSinceLevelLoad.ToString("##.000"));
            rowData.Add(m_recordingId);
            rowData.Add(m_recordingId);
            return rowData;
        }

    }
}
