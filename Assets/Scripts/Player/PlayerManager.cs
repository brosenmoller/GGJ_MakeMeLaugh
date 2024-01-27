using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("minimum of 4 * playercount")]
    [SerializeField] private KeyCode[] availableKeys;
    [SerializeField] private Player[] players;

    private void Awake()
    {
        if (availableKeys.Length < players.Length * 4)
        {
            Debug.LogError("To Few keys have been assigned in the playerManager");
        }

        //ShuffleKeys(players);
    }

    //public void ShuffleKeys(params Player[] playersToShuffle)
    //{
    //    List<KeyCode> unusedKeys = new();
    //    unusedKeys.AddRange(availableKeys);

    //    KeyCode RandomKey()
    //    {
    //        KeyCode randomKey = unusedKeys[Random.Range(0, unusedKeys.Count)];
    //        unusedKeys.Remove(randomKey);

    //        return randomKey;
    //    }

    //    foreach (Player player in playersToShuffle)
    //    {
    //        player.Up = RandomKey();
    //        player.controlUI.UpText.text = player.Up.ToString();
    //        player.Down = RandomKey();
    //        player.controlUI.DownText.text = player.Down.ToString();
    //        player.Right = RandomKey();
    //        player.controlUI.UpText.text = player.Right.ToString();
    //        player.Left = RandomKey();
    //        player.controlUI.UpText.text = player.Left.ToString();
    //    }
    //}
}
