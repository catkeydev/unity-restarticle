using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class TestClient : MonoBehaviour
{
    public string url;
    public string posturl;

    void Start()
    {
        var newuser = new User() { id = 10, name = "Unityuser", currency = 0, level = 0 };

        StartCoroutine(Post(posturl, newuser));
    }

    public IEnumerator Get(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    
                    var user = JsonUtility.FromJson<User>(result);
                    Debug.Log("User name: " + user.name);
                    Debug.Log("User currency: " + user.currency);
                    Debug.Log("User level: " + user.level);
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    public IEnumerator Post(string url, User user)
    {
        var jsonData = JsonUtility.ToJson(user);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"result\":" + result + "}";
                    var resultUserList = KeyUtils.FromJson<User>(result);

                    foreach (var item in resultUserList)
                    {
                        Debug.Log(item.name);
                    }
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

}
