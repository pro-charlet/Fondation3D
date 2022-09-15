using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using Realms.Sync.ErrorHandling;
using System.Threading.Tasks;


public class RealmController : MonoBehaviour
{
    public static RealmController Instance;
    public string RealmAppId;

    public Realm _realm;
    private App _realmApp;
    private User _realmUser;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void OnDisable() {
        if(_realm != null) {
            _realm.Dispose();
        }
    }

    public async Task<string> Login(string email, string password) {
        if(email != "" && password != "") {
            _realmApp = App.Create(new AppConfiguration(RealmAppId) {
                //MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            if(_realmUser == null) {
                _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
            }

            var syncConfig = new FlexibleSyncConfiguration(_realmApp.CurrentUser)
            {
                PopulateInitialSubscriptions = (realm) =>
                {
                    realm.Subscriptions.Add(realm.All<Planete>(), new SubscriptionOptions() { Name = "All_Planetes" });
                },
                ClientResetHandler = new ManualRecoveryHandler(HandleManualResetCallback)
            };

/*
            FlexibleSyncConfiguration syncConfig = new FlexibleSyncConfiguration(_realmUser);
            syncConfig.PopulateInitialSubscriptions = (realm) =>
            {
                realm.Subscriptions.Add(realm.All<Planete>(), new SubscriptionOptions() { Name = "All_Planetes" });

            };
            //syncConfig.ClientResetHandler = new ManualRecoveryHandler(HandleManualResetCallback);
            */
            _realm = await Realm.GetInstanceAsync(syncConfig);


            _realm.Subscriptions.Update(() =>
            {
                _realm.Subscriptions.Add(_realm.All<PlaneteDetail>(), new SubscriptionOptions() { Name = "All_PlaneteDetails" });
                _realm.Subscriptions.Add(_realm.All<Flotte>(), new SubscriptionOptions() { Name = "All_Flottes" });
                _realm.Subscriptions.Add(_realm.All<Batiment>(), new SubscriptionOptions() { Name = "All_Batiments" });
            });

            await _realm.Subscriptions.WaitForSynchronizationAsync();

            return _realmUser.Id;
        }
    return "";
    }

    public IEnumerable<Planete> LoadPlanetes(string mySystemeId)
    {
        IEnumerable<Planete> planetes = null;
        if (_realm != null) {
            planetes = _realm.All<Planete>().Where(s => s.SystemeId == mySystemeId);
        }

        return planetes;
    }

    public IEnumerable<PlaneteDetail> LoadPlanetesDetails(string mySystemeId)
    {
        IEnumerable<PlaneteDetail> planeteDetails = null;
        if (_realm != null) {
            planeteDetails = _realm.All<PlaneteDetail>().Where(s => s.SystemeId == mySystemeId);
        }

        return planeteDetails;
    }

    public IEnumerable<Flotte> LoadFlottes(string mySystemeId)
    {
        IEnumerable<Flotte> flottes = null;
        if (_realm != null) {
            flottes = _realm.All<Flotte>().Where(s => s.SystemeId == mySystemeId);
        }

        return flottes;
    }

    public IEnumerable<Batiment> LoadBatiments(string mySystemeId)
    {
        IEnumerable<Batiment> batiments = null;
        if (_realm != null) {
            batiments = _realm.All<Batiment>().Where(s => s.SystemeId == mySystemeId);
        }

        return batiments;
    }

    private void HandleManualResetCallback(ClientResetException clientResetException)
    {
        Debug.Log("Reset realm Client !!");
        if(_realm != null) {
            _realm.Dispose();
        }
        clientResetException.InitiateClientReset();
    }
}
