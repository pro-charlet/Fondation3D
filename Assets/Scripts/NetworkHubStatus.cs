using FishNet.Managing;
using FishNet.Transporting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NetworkHubStatus : MonoBehaviour
{
    public Color _stoppedColor;
    public Color _startedColor;
    public Color _changingColor;
    public RawImage _serverIndicator;

    private NetworkManager _networkManager;

    // Start is called before the first frame update
    void Start()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
        if (_networkManager == null)
        {
            Debug.LogError("NetworkManager not found, HUD will not function.");
            return;
        }
        else
        {
            UpdateColor(LocalConnectionState.Stopped, _serverIndicator);

            _networkManager.ServerManager.StartConnection();

            _networkManager.ClientManager.OnClientConnectionState += ServerManager_OnClientConnectionState;
            _networkManager.ClientManager.StartConnection();
        }
        
    }

    private void OnDestroy()
    {
        if (_networkManager == null)
            return;

        //_networkManager.ServerManager.OnServerConnectionState -= ServerManager_OnServerConnectionState;
        _networkManager.ClientManager.OnClientConnectionState -= ServerManager_OnClientConnectionState;
    }
/*
    private void ServerManager_OnServerConnectionState(ServerConnectionStateArgs obj)
    {
        UpdateColor(obj.ConnectionState, _serverIndicator);
    }
*/
    private void ServerManager_OnClientConnectionState(ClientConnectionStateArgs obj)
    {
        UpdateColor(obj.ConnectionState, _serverIndicator);
    }

    private void UpdateColor(LocalConnectionState state, RawImage img)
    {
        Color c;
        if (state == LocalConnectionState.Started)
            c = _startedColor;
        else if (state == LocalConnectionState.Stopped)
            c = _stoppedColor;
        else
            c = _changingColor;

        img.color = c;

    }
}
