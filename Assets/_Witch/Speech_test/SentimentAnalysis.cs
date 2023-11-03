using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
using System.IO;

public class SentimentAnalysis : MonoBehaviour
{
    private string apiKey;
    private string url = "https://language.googleapis.com/v1/documents:analyzeSentiment?key="; // Google Cloud Natural Language API endpoint
    private string response = "";
    float magnitude, score;

    void Start(){
        var path = Path.Combine(Application.persistentDataPath, "apiKey.txt");
        if(File.Exists(path))apiKey = File.ReadAllText(path);
        else Debug.Log("No API Key");
    }

    public void AnalyzeSentiment(string text)
    {
        magnitude = -2f;
        score = -2f;
        StartCoroutine(SendRequest(text));
    }

    IEnumerator SendRequest(string text)
    {
        string jsonRequestBody = "{\"document\":{\"type\":\"PLAIN_TEXT\",\"language\":\"zh-TW\",\"content\":\"" + text + "\"}}";
        byte[] byteData = Encoding.UTF8.GetBytes(jsonRequestBody);

        UnityWebRequest www = new UnityWebRequest(url + apiKey, "POST");
        www.uploadHandler = new UploadHandlerRaw(byteData);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            response = www.downloadHandler.text;
            // Debug.Log("NL Response: " + response);
            Response();
        }
    }

    private void Response(){
        int m = response.IndexOf("magnitude") + "magnitude".Length + 3;
        int s = response.IndexOf("score") + "score".Length + 3;
        
        magnitude = float.Parse(response.Substring(m, 3));
        
        if(response[s]=='-')score = float.Parse(response.Substring(s, 4));
        else score = float.Parse(response.Substring(s, 3));

        Debug.Log("Magnitude: " + magnitude + ", Score: " + score);
    }
    
    public float GetScore(){
        return score;
    }
    public float GetMagnitude(){
        return magnitude;
    }
}