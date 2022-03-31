using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class PlayerOwnWorldData{
    public string plummie_tag;
    public string name;
    public string gender;
    public PlayerDataOwn playerData;
    public Trading trading;
   // public FriendList friendList;
    public List<string> listOfFriends;



    [System.Serializable]
    public class PlayerDataOwn{

        public int currentHouseId;
        public int woodCount;
        public int stoneCount;
        public int coinCount;
    }
    
    [System.Serializable]
    public class Trading{

        public string traderId;
        public int idOfslot0;
        public int idOfslot1;
        public int idOfslot2;
        public int idOfslot3;
        public int amountOfslot0;
        public int amountOfslot1;
        public int amountOfslot2;
        public int amountOfslot3;            
    }



    /*
    [System.Serializable]
    public class FriendList{
    }
   */
  



    

    public string Stringify() {
        return JsonUtility.ToJson(this);
    }

    public static PlayerOwnWorldData Parse(string json)
    {
        return JsonUtility.FromJson<PlayerOwnWorldData>(json);
    }

    public IEnumerator FetchPlayerData(System.Action<PlayerOwnWorldData> callback = null)
    {
        Debug.Log("DataFetch");
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/plummies/" + this.plummie_tag))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                //Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(null);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(PlayerOwnWorldData.Parse(request.downloadHandler.text));
                }
            }
        }
    }

    public IEnumerator PostPlayerData(System.Action<bool> callback = null)
    {
        using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/plummies", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(this.Stringify());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
               // Debug.Log(request.error);
                if(callback != null) {
                    callback.Invoke(false);
                }
            }
            else
            {
                // Debug.Log(request.downloadHandler.text);
                if(callback != null) {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }


    public IEnumerator SavePlayerData(System.Action<bool> callback = null)
    {


        Debug.Log("Saveing in PlayerOwnworldData");

        string updateRoute = "http://localhost:3000/plummies/"+this.plummie_tag;

        Debug.Log("UpdateRoute: "+updateRoute);

        using (UnityWebRequest request = UnityWebRequest.Post(updateRoute,"bodyRaw"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(this.Stringify());
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.method = "PUT";

           // Debug.Log("url: "+request.url);
            //Debug.Log("Header: "+ request.bodyData);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
               Debug.Log("Error saving: "+request.error);
                if(callback != null) {
                    callback.Invoke(false);
                }
            }
            else
            {
                 Debug.Log("Request handler: "+request.downloadHandler.text);
                if(callback != null) {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }
}
