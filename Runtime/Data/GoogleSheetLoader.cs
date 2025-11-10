using System.Collections;
using UnityEngine.Networking;


namespace LumosLib
{
    public class GoogleSheetLoader : BaseDataLoader
    {
        public override IEnumerator LoadJsonAsync()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(_path))
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


