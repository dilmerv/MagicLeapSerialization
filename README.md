# MagicLeapSerialization
A Serialization project specific for Magic Leap creators to use for saving and loading data from their Unity experiences

There are two entities available which can be extended based on your requirements:

1. Player.cs - This is a very basic object that holds player information

2. Level - This is a very basic object that holds level information

3. Game Data - Holds 2 properties for a Player and an array of levels

Take a look at the GameData.json located in the root of the project and also the TestSerializer object which calls into a class called SerializerManager which is reponsible for saving and loading JSON from the file system.
