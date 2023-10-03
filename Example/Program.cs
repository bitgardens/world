﻿// See https://aka.ms/new-console-template for more information

using WorldRepr.World;

var w = World.FromJson("""
   {
     "RawTopology": { "65537": "0100", "131073": "0001" },
     "RawMeta": {
       "0": { "Kind": 0 },
       "65536": { "Kind": 0 },
       "131072": { "Kind": 0 },
       "1": { "Kind": 0 },
       "65537": { "Kind": 1 },
       "131073": { "Kind": 1 },
       "2": { "Kind": 0 },
       "65538": { "Kind": 0 },
       "131074": { "Kind": 0 }
     }
   }
""");

Console.WriteLine("parsed: ok");

Console.WriteLine("---");
Console.WriteLine(w.ToJson());