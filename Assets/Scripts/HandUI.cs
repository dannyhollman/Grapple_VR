using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;

public class HandUI : MonoBehaviour
{
    public Text killsText;
    public Text healthText;

    public Text[] names;
    public Text[] kills;

    private void OnEnable()
    {
        if (!BoltNetwork.IsServer)
        {
            int i = 0;
            foreach (BoltEntity entity in BoltNetwork.Entities)
            {
                IVRPlayerState state;

                entity.TryFindState<IVRPlayerState>(out state);

                if (state != null)
                {
                    names[i].text = state.Username;
                    kills[i].text = state.Kills.ToString();
                    i++;
                }
            }
        }
    }
}
