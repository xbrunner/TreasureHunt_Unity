using HoloToolkit.Unity.InputModule;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // TextMeshPro for 3D Text in Unity

public class ClickBehaviourScript : MonoBehaviour, IInputClickHandler
{
    private string url = "https://services1.arcgis.com/i9MtZ1vtgD3gTnyL/arcgis/rest/services/treassure/FeatureServer/0"; //URL to ArcGIS Web Service
    private string treasureLabel = ""; // To display TreasureName on LeaderBoard
    public string treasureName = "Bethselaminian+Backpack"; // To query Treasure

    public TextMeshPro textPrefab; // the TextMeshPro to display
    private TextMeshPro infotext; // the TextMeshPro for local use

    public Mesh close; // Treasure is closed
    public Mesh open; // Treasure is open



    public void OnInputClicked(InputClickedEventData eventData)
    {
        // Make the cube darker
        //GetComponent<Renderer>().material.color = new Color32(191, 11, 0, 100);

        //Start Coroutine to query our data
        StartCoroutine(GetTreasureInfo(treasureName));
    }

    IEnumerator GetTreasureInfo(string treasName)
    {
        // {0}-> URL and {1}-> treasureName
        string uri = string.Format("{0}/query?where=treasure_name=+%09%27{1}%27&outFields=user_id,timestamp,collected_coins&orderByFields=collected_coins+DESC%2C+timestamp+DESC%3B&resultRecordCount=10&returnGeometry=false&f=json", new object[]
        {
            url,
            treasName,
        });

        // Builder for the infoText we want to show
        var infoTextStr = new System.Text.StringBuilder();

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            //Request and wait for the desired JSON
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else {
                if (webRequest.isDone) {
                    // If we get an results handle the result
                    string jsonResult = webRequest.downloadHandler.text;

                    //JSON to Treassure using Deserialize
                    TreasureObject treasureObject = new TreasureObject();
                    treasureObject = JsonConvert.DeserializeObject<TreasureObject>(jsonResult);


                    // Update Treasure Label (for LeaderBoard)
                    if (treasName == "Bethselaminian+Backpack")
                    {
                        treasureLabel = "Bethselaminian Backpack";
                    }
                    else if (treasName == "Staff+of+the+Forlorn+Traveler")
                    {
                        treasureLabel = "Staff of the Forlorn Traveler";
                    }
                    else treasureLabel = "Common Tavern Ale";

                    infoTextStr.AppendLine("***TOP 10***");
                    infoTextStr.AppendLine("For " + treasureLabel);
                    infoTextStr.AppendLine("UserId" + " - " + "TimeStamp" + " - " + "CollectedCoins");
                    infoTextStr.AppendLine("_________________________________");
                    foreach (Feature feature in treasureObject.Features) {
                        Attributes a = feature.Attributes;
                        infoTextStr.AppendLine(a.UserId + " - " + a.Timestamp + " - " + a.CollectedCoins);
                    }

                    infotext.enabled = true; // Reset LeaderBoard in cas it has disappear
                    GetComponent<MeshFilter>().sharedMesh = open; // Open treasure
                    yield return new WaitForSeconds(1); // Wait 1 sec to open the treasure

                    infotext.GetComponent<TextMeshPro>().SetText(infoTextStr);
                    yield return new WaitForSeconds(5); // Make the LeaderBoard disappear after 5 seconds

                    infotext.enabled = false; // LeaderBoard Disappear

                    yield return new WaitForSeconds(1); // Wait 1 sec to close the treasure
                    GetComponent<MeshFilter>().sharedMesh = close; // Close treasure
                    


                    //GetComponent<Renderer>().material.color = new Color32(5, 191, 0, 100);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create and place the textMeshPro
        infotext = Instantiate(textPrefab) as TextMeshPro;

        // Child of the treasure
        infotext.transform.SetParent(gameObject.transform);



    }

    void Update() {
        // Leaderboard follow position of Hololens Cursor
        var camPos = Camera.main.transform.position + Camera.main.transform.forward;

        // Update position of leaderboard
        infotext.transform.position = camPos;
        infotext.transform.rotation = Camera.main.transform.rotation;
        infotext.transform.localScale = Vector3.one *0.4f;

    }

}
