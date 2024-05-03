﻿using System.Security.Cryptography;

namespace RPSGame.Tools;

public static class Randomiser
{
    public static int RandomMove(string[] namesOfMoves)
    {
        Random _rnd = new Random();
        int move = _rnd.Next(0, namesOfMoves.Length);
        return move;
    }

    public static byte[] RandomKey()
    {
        using (RandomNumberGenerator  rnd = RandomNumberGenerator.Create()) 
        {
            var key = new byte[256];
            rnd.GetBytes(key);
            return key;
        }

    }
}