using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class SpeechToText : MonoBehaviour
{
    private string googleApiKey = "AIzaSyAjiZt-cB0607edQnoTOGJEcxRrLPgD8mc";
    private string response = "";
    private string transcript = "";

    public void Recognize()
    {
        transcript = "";
        string result = string.Empty;
        var path = Path.Combine(Application.streamingAssetsPath, "recordedAudio.wav");
        byte[] voice = File.ReadAllBytes(path);
        StartCoroutine(ConvertAudioToText(voice));
    }

    private IEnumerator ConvertAudioToText(byte[] audioBytes)
    {
        string url = "https://speech.googleapis.com/v1/speech:recognize?key=" + googleApiKey;

        string json = @"
        {
            'config': {
                'encoding': 'LINEAR16',
                'sampleRateHertz': 16000,
                'languageCode': 'zh-TW'
            },
            'audio': {
                'content': '" + System.Convert.ToBase64String(audioBytes) + @"'
            }
        }";

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(url, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                // Debug.Log(www.result);
            }
            else
            {
                response = www.downloadHandler.text;
                // Debug.Log("ST Response: " + response);
                Response();
            }
        }
    }

    private void Response(){
        int t = response.IndexOf("transcript") + "transcript".Length + 4;
        response = response.Substring(t);
        string[] str = response.Split('"');
        transcript = str[0];

        Debug.Log("Transcript: " + transcript);
    }

    public string GetTranscript(){
        if(transcript!="")return transcript;
        return "ERROR";
    }

}
