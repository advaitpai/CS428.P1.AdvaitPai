using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class M_WeatherScript : MonoBehaviour
{

    string weatherApiUrl = "https://api.openweathermap.org/data/2.5/weather?lat=40.6892&lon=-74.0445&APPID=b00299bc547d13ff9a8694046cc133a2&units=imperial"; 

    public GameObject weatherText;

    // Start is called before the first frame update
    void Start()
    {

    // wait a couple seconds to start and then refresh every 900 seconds

       InvokeRepeating("GetDataFromWeb", 2f, 900f);
   }

   void GetDataFromWeb()
   {

       StartCoroutine(GetRequest(weatherApiUrl));
   }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);         
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                int startTemp = webRequest.downloadHandler.text.IndexOf("temp",0);
                int endTemp = webRequest.downloadHandler.text.IndexOf(",",startTemp);
                double tempF = float.Parse(webRequest.downloadHandler.text.Substring(startTemp+6, (endTemp-startTemp-6)));
                int easyTempF = Mathf.RoundToInt((float)tempF);
                //Debug.Log ("integer temperature is " + easyTempF.ToString());
                int startConditions = webRequest.downloadHandler.text.IndexOf("main",0);
                int endConditions = webRequest.downloadHandler.text.IndexOf(",",startConditions);
                string conditions = webRequest.downloadHandler.text.Substring(startConditions+7, (endConditions-startConditions-8));
                //Debug.Log(conditions);

                weatherText.GetComponent<TextMeshPro>().text = "Current Weather\n\n" + easyTempF.ToString() + "°F\n" + conditions;
                // weatherText.GetComponent<TextMeshPro>().text = "Current Weather\n" +  webRequest.downloadHandler.text;     
            }
        }
    }
}
