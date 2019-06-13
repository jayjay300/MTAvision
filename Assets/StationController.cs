using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System.Net;
using UnityEditor;
using System.Globalization;
using System.Collections;

public class StationController : MonoBehaviour
{

    public float lat= 40.7135f, lon= 74.0067f;
    //
	private string b = "https://mtapii.herokuapp.com/";
	private string response,routes;
	private const string URL = "www.google.com";
	private string finaluri;
    private int route,x,y;
    public GameObject lati, longi;
	public RootObject info;
	public Datum[] stations;
    public Sprite[] Stationpics;
    public DateTime now, train;
    //private GameObject NT1, NT2, NT3, NT4, NT5, ST1, ST2, ST3, ST4, ST5;
    public GameObject[] trainobjects;
    // private Text N1time, N2time, N3time, N4time, N5time, S1time, S2time, S3time, S4time, S5time;
    public Text[] time;
    public Image[] trains;
    public string DEBUGTIME;
    public TimeSpan EASTTIMEZONE;
   // private TrackableBehaviour mTrackableBehaviour;


        private IEnumerator StartLocationService()
    {

        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
              yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
              yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
              yield break;
        }

        lat = Input.location.lastData.latitude;
        lati.GetComponent<Text>().text = lat.ToString();
        lon = Input.location.lastData.longitude;
        longi.GetComponent<Text>().text = lon.ToString();
        // longi = lon.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(StartLocationService());
        EASTTIMEZONE = TimeSpan.FromHours(4);
        //  DateTime d2 = DateTime.Parse("2010-08-20T15:00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind);
        // First, check if user has location service enabled

        /*
        if (!Input.location.isEnabledByUser)
           // yield break;

        // Start service before querying location
        Input.location.Start(3);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
          //  yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
          //  yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
          //  yield break;
        }
        else
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            Debug.Log("LAT: " + lat.ToString() + " LONG: " + lon.ToString());
            // Access granted and location value could be retrieved
          //  print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
        */      
        /*
       NT1 = GameObject.Find("NT1");
        NT2 = GameObject.Find("NT2");
        NT3 = GameObject.Find("NT3");
        NT4 = GameObject.Find("NT4");
        NT5 = GameObject.Find("NT5");

        ST1 = GameObject.Find("ST1");
        ST2 = GameObject.Find("ST2");
        ST3 = GameObject.Find("ST3");
        ST4 = GameObject.Find("ST4");
        ST5 = GameObject.Find("ST5");

        N1time = NT1.transform.GetChild(0).gameObject.GetComponent<Text>();
        N2time = NT2.transform.GetChild(0).gameObject.GetComponent<Text>();
        N3time = NT3.transform.GetChild(0).gameObject.GetComponent<Text>();
        N4time = NT4.transform.GetChild(0).gameObject.GetComponent<Text>();
        N5time = NT5.transform.GetChild(0).gameObject.GetComponent<Text>();

        S1time = ST1.transform.GetChild(0).gameObject.GetComponent<Text>();
        S2time = ST2.transform.GetChild(0).gameObject.GetComponent<Text>();
        S3time = ST3.transform.GetChild(0).gameObject.GetComponent<Text>();
        S4time = ST4.transform.GetChild(0).gameObject.GetComponent<Text>();
        S5time = ST5.transform.GetChild(0).gameObject.GetComponent<Text>();
        */
        //N1 = NT1.GetComponent<Image>();
        /*      N2 = NT2.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              N3 = NT3.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              N4 = NT4.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              N5 = NT5.transform.GetChild(0).gameObject.GetComponent<Texture2D>();

              S1 = ST1.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              S2 = ST2.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              S3 = ST3.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              S4 = ST4.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              S5 = ST5.transform.GetChild(0).gameObject.GetComponent<Texture2D>();
              */
        //finaluri = b+"by-location?"+"lat="+lat.ToString()+"&lon="+lon.ToString();
	//UnityWebRequest req = UnityWebRequest.Get(finaluri);
		Debug.Log(finaluri);
		//info = getInfo();
		Debug.Log("test");
		//Debug.Log(getInfo()); // not working?

		//StartCoroutine(Get(finaluri));
        //setInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<DefaultTrackableEventHandler>().flag == true)
        {
            setInfo();
            gameObject.GetComponent<DefaultTrackableEventHandler>().flag = false;
        }
    }

    void setInfo() //RootObject or Datum?
	{
        finaluri = b + "by-location?" + "lat=" + lat.ToString() + "&lon=" + lon.ToString();
        StartCoroutine(Get(finaluri));
        now = DateTime.Now;
        Debug.Log("THE TIME IS: "+now.ToString());
        //DEBUGTIME = "2019-04-27T16:51:10-04:00";
       // DEBUGTIME = DEBUGTIME.Substring(0, 19); //troubleshoot to make sure this works at all times- pray it does
        //2019-04-27T16:51:10-04:00
      //  DateTime.TryParseExact(DEBUGTIME, @"yyyy-MM-dd\THH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out train);
      //  Debug.Log("THE TRAIN TIME IS: " + train.ToString());
        /*
        N1time.text = info.data[0].N[0].time;
        N2time.text = info.data[0].N[1].time;
        N3time.text = info.data[0].N[2].time;
        N4time.text = info.data[0].N[3].time;
        N5time.text = info.data[0].N[4].time;

        S1time.text = info.data[0].S[0].time;
        S2time.text = info.data[0].S[1].time;
        S3time.text = info.data[0].S[2].time;
        S4time.text = info.data[0].S[3].time;
        S5time.text = info.data[0].S[4].time;
        */
        //N1.overrideSprite = A;
        // N1.overrideSprite = (Sprite)AssetDatabase.LoadAssetAtPath("/Assets/trainlogos/"+info.data[0].N[0].route.ToString() + "+train+logo",typeof(Sprite));
        // routes= info.data[0].N[0].route.ToString();
        //route = int.Parse(routes);
        // N1.overrideSprite = Stationpics[route];
        //HttpWebRequest request = new HttpWebRequest(URL);

        for (x = 0; x <= (trains.Length-1); x++)
        {
            if (x <= 5)
            {
                DEBUGTIME = info.data[0].N[x].time.ToString();
                Debug.Log("THE DEBUG TRAIN TIME IS: " + DEBUGTIME.ToString());
                DEBUGTIME = DEBUGTIME.Substring(0, 19); //troubleshoot to make sure this works at all times- pray it does
                                                        //2019-04-27T16:51:10-04:00
                DateTime.TryParseExact(DEBUGTIME, @"yyyy-MM-dd\THH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out train);
               train= train.Add(EASTTIMEZONE); //FOR time difference
                Debug.Log("THE TRAIN TIME IS: " + train.ToString());


                System.TimeSpan diff1 =  train.Subtract(now);


                //INSERT TIME IN MINUTES HERE

                //  time[x].text = info.data[0].N[x].time.ToString();
                // time[x].text = diff1.ToString()+" min";
                time[x].text = (int)Math.Round(diff1.TotalMinutes) + " min";
                routes = info.data[0].N[x].route.ToString();
            }
            else
            {
                DEBUGTIME = info.data[0].N[x].time.ToString();
                DEBUGTIME = DEBUGTIME.Substring(0, 19); //troubleshoot to make sure this works at all times- pray it does
                                                        //2019-04-27T16:51:10-04:00
                DateTime.TryParseExact(DEBUGTIME, @"yyyy-MM-dd\THH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out train);
              train=  train.Add(EASTTIMEZONE); //FOR time difference
                Debug.Log("THE TRAIN TIME IS: " + train.ToString());


                System.TimeSpan diff1 = train.Subtract(now); //FLIP NOW AND TRAIN

                //INSERT TIME IN MINUTES HERE
                y = x - 5;
                // time[x].text = info.data[0].S[y].time.ToString();
                //time[x].text = diff1.ToString()+" min";
                time[x].text = (int)Math.Round(diff1.TotalMinutes) + " min";
                routes = info.data[0].S[y].route.ToString();

            }


            if (routes == "A")
                route = 0;
            else if (routes == "J")
                route = 14;
            else if (routes == "B")
                route = 8;
            else if (routes == "C")
                route = 9;
            else if (routes == "L")
                route = 15;
            else if (routes == "B")
                route = 14;
            else if (routes == "D")
                route = 10;
            else if (routes == "F")
                route = 12;
            else if (routes == "M")
                route = 16;
            else if (routes == "G")
                route = 13;
            else if (routes == "N")
                route = 17;
            else if (routes == "Q")
                route = 18;
            else if (routes == "R")
                route = 19;
            else if (routes == "Z")
                route = 20;
            else if (routes == "E")
                route = 11;
            else if (routes == "5X")
                route = 5;
            else if (routes == "4X")
                route = 4;
            else if (routes == "6X")
                route = 6;
            else if (routes == "W")
                route = 22;
            else
            {
                route = int.Parse(routes);
            }

            trains[x].overrideSprite = Stationpics[route];

        }



    }


	public IEnumerator Get(string url){
		using(UnityWebRequest www = UnityWebRequest.Get(url)){

			Debug.Log("1test1");
			yield return www.SendWebRequest();

			if(www.isNetworkError){
				Debug.Log(www.error);
			}
			else {

				if(www.isDone){

					string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
					Debug.Log("1test");
					Debug.Log(jsonResult);

					 info  = JsonUtility.FromJson<RootObject>(jsonResult);
					addStations(info);

                    setInfo();
				}
			}


		}
	}


	void addStations(RootObject info) {
		foreach(Datum datum in info.data)
		{

			Debug.Log(datum.name.ToString());
		}


	}

	[System.Serializable]
		public class N
		{
		public string route;
		public string time;
		}
	[System.Serializable]
		public class S
		{
		public string route;
		public string time;
		}
	[System.Serializable]
		public class Stops
		{
		public List<double> coordinates; //correct- saved as string because not really needed in this case
		}
	[System.Serializable]
		public class Datum
		{
		public List<N> N;
		public List<S> S; // acting screwy- N ending up in S -IDIOT IT WAS THE N TRAIN
		public string id;
		public string last_update; 
		public List<double> location; 
		public string name; 
		public List<string> routes;
		public List<Stops> Stops; //not currently pulling- not really necessary anyway
		}
	[System.Serializable]
		public class RootObject
		{
		public List<Datum> data;
		public string updated;
		}
}
