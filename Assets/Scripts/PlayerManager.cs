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

        ShuffleKeys();
        Debug.Log(players[0].Down.ToString());
    }

    public void ShuffleKeys()
    {
        List<KeyCode> unusedKeys = new();
        unusedKeys.AddRange(availableKeys);

        KeyCode RandomKey()
        {
            KeyCode randomKey = unusedKeys[Random.Range(0, unusedKeys.Count)];
            unusedKeys.Remove(randomKey);

            return randomKey;
        }

        foreach (Player player in players)
        {
            player.Up = RandomKey();
            player.UpText.text = player.Up.ToString();
            player.Down = RandomKey();
            player.DownText.text = player.Down.ToString();
            player.Right = RandomKey();
            player.RightText.text = player.Right.ToString();
            player.Left = RandomKey();
            player.LeftText.text = player.Left.ToString();
        }
    }
}
