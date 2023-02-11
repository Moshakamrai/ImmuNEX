using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using LitJson;

public class GetLastRFID : MonoBehaviour
{

    public InputField rfidInputField;


    DataRFID data;
    // Start is called before the first frame update
    void Start()
    {
        data = new DataRFID();
    }

    public async void TestGet()
    {
        var url = "https://us-central1-nestle-activation-e65b3.cloudfunctions.net/players/last";
        using var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/json");

        var operation = www.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }
        if (www.result == UnityWebRequest.Result.Success)
        {

            //Data data = JsonConvert.DeserializeObject<Data>(www.downloadHandler.text);
            data.GetData(www.downloadHandler.text);
            Debug.Log($"Success : {data.email}");
        }
        else
        {
            Debug.Log($"Failed : {www.error}");
        }
    }
}
public class DataRFID
{
    public string id = "";
    public string email = "";
    public string phone = "";
    public string rfid = "";
    public string p_id = "";
    public string organization = "";

    public void GetData(string jsonobj)
    {
        var data = JsonMapper.ToObject(jsonobj);
        id = data["data"]["id"].ToString();
        email = data["data"]["email"].ToString();
        phone = data["data"]["phone"].ToString();
        rfid = data["data"]["rfid"].ToString();
        p_id = data["data"]["p_id"].ToString();
        organization = data["data"]["organization"].ToString();
    }
}