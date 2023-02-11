using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using LitJson;
public class PostRequest : MonoBehaviour
{
    public RFIDReader rfidReader;

    string lastScannedRFID;
    string lastFeedback;

    public void SetCurrentRFID(string rfid)
    {
        lastScannedRFID = rfid;
        Debug.LogError("Last scanned rfid " + lastScannedRFID);
    }

    public void SetLastFeedback(string feedback)
    {
        lastFeedback = feedback;
    }
    public void PostRequestWeb()
    {
        StartCoroutine(UploadUserInfo());
    }

    public void UpdateGroups(int groupNo)
    {
        StartCoroutine(UploadGrouping(groupNo));
    }

    public void PostFeedback(string rfid)
    {
        StartCoroutine(UploadFeedback(rfid));
    }

    IEnumerator UploadUserInfo()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", "n@gmail.com");
        form.AddField("name", "Fuad");
        form.AddField("organization", "IPDC");
        form.AddField("phone", "2441139");
        form.AddField("rfid", "4532a");

        UnityWebRequest www = UnityWebRequest.Post("https://us-central1-nestle-activation-e65b3.cloudfunctions.net/players", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;

            Debug.Log(results);
        }
        www.Dispose();

    }

    IEnumerator UploadGrouping(int groupNo)
    {
        Debug.LogError("Assigning group " + groupNo + "to " + lastScannedRFID);
        WWWForm form = new WWWForm();
        form.AddField("playerRfid", lastScannedRFID);
        //byte[] myData = System.Text.Encoding.UTF8.GetBytes(UIManager.instance.currentRFID);
        UnityWebRequest www = UnityWebRequest.Post("https://us-central1-nestle-activation-e65b3.cloudfunctions.net/players/" + groupNo, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            //show error message on UI
        }
        else
        {
            rfidReader.buttonPressed = false;
            rfidReader.TurnInputOn();
            //UIManager.instance.IncrementGroupCounter(groupNo - 1);
            string results = www.downloadHandler.text;
            Debug.Log(results);
        }
        www.Dispose();
    }

    IEnumerator UploadFeedback(string rfid)
    {
        WWWForm form = new WWWForm();
        form.AddField("feedback", lastFeedback);
        Debug.LogError("posting for " + rfid);
        UnityWebRequest www = UnityWebRequest.Post("https://us-central1-nestle-activation-e65b3.cloudfunctions.net/players/feedback/" + rfid, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            //show error message on UI
        }
        else
        {
            FeedbackHandler.feedbackInstance.DisableFeedback();
            //turn feedback form back on
            string results = www.downloadHandler.text;
           // FeedbackHandler.feedbackInstance.feedbackInput.text = "";
            Debug.Log(results);
        }
        www.Dispose();
    }
}

