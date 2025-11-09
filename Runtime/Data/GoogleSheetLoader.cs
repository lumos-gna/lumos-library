using System.Collections;
using UnityEngine.Networking;


namespace LumosLib
{
    public class GoogleSheetLoader
    {
        private string baseUrl =
            "https://script.google.com/macros/s/AKfycbwRE8EdNSjWSWYMKiojpQabG6ksD7K1neE5wKx-O0_QZ9qHvvpoLgh7H5AX0vc7YBt92w/exec";

        public string Json { get; private set; }


        public void SetPath(string path)
        {
            
        }
        
        public IEnumerator LoadJsonAsync()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(baseUrl))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    DebugUtil.LogError($"{www.error}", " LOAD FAIL ");
                    yield break;
                }
            
                Json = www.downloadHandler.text;
            }
        }
    }
}


