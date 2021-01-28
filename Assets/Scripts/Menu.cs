using System;
using UnityEngine;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;
using UdpKit.Platform.Photon;
using UnityEngine.UI;


public class Menu : GlobalEventListener
{
    public UIManager uIManager;
    public Map<Guid, UdpSession> sessions;

    public Text username;

    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = Guid.NewGuid().ToString();

            BoltMatchmaking.CreateSession(
                //sessionID: matchName,
                sessionID: String.Format("Lobby {0}", BoltNetwork.SessionList.Count + 1),
                sceneToLoad: "Demo"
            );
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", BoltNetwork.SessionList.Count);
        sessions = sessionList;

        GetRoomList();
        /*
        foreach (var session in BoltNetwork.SessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
        */
    }

    public void JoinRandomRoom()
    {
        foreach (var session in sessions)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }

    }

    public void GetRoomList()
    {
        Debug.Log("GetRoomList");
        if (sessions.Count > 0)
        {
            int count = 0;
            foreach (var session in sessions)
            {
                var photonSession = session.Value as PhotonSession;

                if (photonSession.Source == UdpSessionSource.Photon)
                {
                    var matchName = photonSession.HostName;
                    var label = string.Format("{0} | {1}/{2}", matchName, photonSession.ConnectionsCurrent, photonSession.ConnectionsMax);
                    Debug.Log(label);

                    uIManager.rooms[count].SetActive(true);
                    uIManager.text[count].text = label;
                    uIManager.buttons[count].onClick.AddListener(delegate { JoinRoom(photonSession); });
                        

                    //BoltMatchmaking.JoinSession(photonSession);
                }
            }
        }
        else
            Debug.Log("No Rooms");
    }

    void JoinRoom(PhotonSession photonSession)
    {
        BoltMatchmaking.JoinSession(photonSession);
    }
}
