using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using LitJson;

public class GetRequest : MonoBehaviour
{
    public Text text;
    public InputField rfidInputField;
    
    Data data;

    void Start()
    {
        data = new Data();
    }

    public async void TestGet(string rfid) 
    {
        //string userID = rfidInputField.text.ToString();

        var url = "https://us-central1-nestle-activation-e65b3.cloudfunctions.net/players?rfid=" + rfid;
        using var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/json");

        var operation = www.SendWebRequest();

        while(!operation.isDone){
            await Task.Yield();
        }
        if (www.result == UnityWebRequest.Result.Success)
        {
            
            //Data data = JsonConvert.DeserializeObject<Data>(www.downloadHandler.text);
            
            data.GetData(www.downloadHandler.text);
            //text.text = data.email;
            Debug.Log($"Success : {data.email}");

            UIManager.instance.ShowUserInfo(data.name, data.organization, data.email, data.phone, data.rfid , data.gender);
            Debug.Log(data.name);
            Debug.Log(data.gender);
        }
        else
        {
            Debug.Log($"Failed : {www.error}");
        }
    }
}
public class Data
{
    public string phone = "";
    public string name = "";
    public string rfid = "";
    public string p_id = "";
    public string email = "";
    public string organization = "";
    public string gender = "";

    public void GetData(string jsonobj) 
    {        
        var data = JsonMapper.ToObject(jsonobj);
        phone = data["result"][0]["phone"].ToString();
        name = data["result"][0]["name"].ToString();
        rfid = data["result"][0]["rfid"].ToString();
        p_id = data["result"][0]["p_id"].ToString();
        email = data["result"][0]["email"].ToString();
        organization = data["result"][0]["organization"].ToString();
        gender = data["result"][0]["gender"].ToString();

    }
}