using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Harmony;
using BWModLoader;

namespace BWPirateChat
{
    public class PirateChat : MonoBehaviour
    {
        //this is where all the word conversion queries are stored
        public static List<WordConversion> listOfShitAndStuff = new List<WordConversion>();

        static string dictionaryFileName = "wordreplacement.yarrml";

        static string configFileName = "BWPirateChat.cfg";

        public static string[] prefixStrings;

        public static bool validFile;

        static System.Random randomAssNumber = new System.Random();//this will probably be pretty random if I declare it here

        public static bool pirateModeEnabled = true;

        public static int impersonatePermLevel = 1;

        public static int pirateModePermLevel = 1;

        public static int permModeLevel = 2;

        public static int reloadPermLevel = 2;

        public static ModLogger newLog;
        public static WakeNetObject wno;
        [HarmonyPatch(typeof(Chat), "sendChat", new Type[]
        {
            typeof(int),
            typeof(int),
            typeof(string),
            typeof(string),
            typeof(ïçîìäîóäìïæ.åéðñðçîîïêç)
        })]
        static class Whatever
        {
            static bool Prefix(int chatType, int senderTeam, ref string sender, ref string text, ïçîìäîóäìïæ.åéðñðçîîïêç info)
            {
                if (text[0] == '!')
                {
                    //if (GameMode.getPlayerBySocket(info.éäñåíéíìééä).isAdmin)
                    //{
                        //text = text.ToLower();
                        //impersonate command

                        if (text.Split()[0] == "!impersonate_perms" && hasCorrectPermission(info, permModeLevel))
                        {
                            if (text[text.Length - 1].ToString() == "0")
                            {
                                impersonatePermLevel = 0;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "impersonate permissions set to user");
                                writeChangesToConfigFile(configFileName);
                            }
                            else if (text[text.Length - 1].ToString() == "1")
                            {
                                impersonatePermLevel = 1;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "impersonate permissions set to moderator");
                                writeChangesToConfigFile(configFileName);
                            }
                            else if (text[text.Length - 1].ToString() == "2")
                            {
                                impersonatePermLevel = 2;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "impersonate permissions set to admin");
                                writeChangesToConfigFile(configFileName);
                            }
                            else
                            {
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Syntax error");
                            }
                            return false;
                        }
                        if (text.Split()[0] == "!impersonate" && hasCorrectPermission(info, impersonatePermLevel))
                        {
                            Impersonate(ref sender, ref text);
                            newLog.Log("running impersonation script");
                            return true;
                        }
                        //enable/disable pirate mode command
                        if (text.Split()[0] == "!piratespeak" && hasCorrectPermission(info, pirateModePermLevel))
                        {
                            if (text[text.Length - 1].ToString() == "0")
                            {
                                pirateModeEnabled = false;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Pirate speak disabled!");
                                writeChangesToConfigFile(configFileName);
                            }
                            else if (text[text.Length - 1].ToString() == "1")
                            {
                                pirateModeEnabled = true;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Pirate speak enabled!");
                                writeChangesToConfigFile(configFileName);
                            }
                            else
                            {
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Syntax error");
                            }
                            return false;
                        }
                        if (text.Split()[0] == "!piratespeak_perms" && hasCorrectPermission(info, permModeLevel))
                        {
                            if (text[text.Length - 1].ToString() == "0")
                            {
                                pirateModePermLevel = 0;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "pirate speak toggle permissions set to user");
                                writeChangesToConfigFile(configFileName);
                            }
                            else if (text[text.Length - 1].ToString() == "1")
                            {
                                pirateModePermLevel = 1;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "pirate speak toggle permissions set to moderator");
                                writeChangesToConfigFile(configFileName);
                            }
                            else if (text[text.Length - 1].ToString() == "2")
                            {
                                pirateModePermLevel = 2;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "pirate speak toggle permissions set to admin");
                                writeChangesToConfigFile(configFileName);
                            }
                            else
                            {
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Syntax error");
                            }
                            return false;
                        }
                    if (text.Split()[0] == "!reloaddict" && hasCorrectPermission(info, reloadPermLevel))
                    {
                        listOfShitAndStuff.Clear();
                        readDictionaryFile(dictionaryFileName);
                        wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Dictionary file reloaded");
                        return false;
                    }

                    if (text.Split()[0] == "!reloaddict_perms" && hasCorrectPermission(info, permModeLevel))
                    {
                        if (text[text.Length - 1].ToString() == "0")
                        {
                            reloadPermLevel = 0;
                            wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "dictionary reload permissions set to user");
                            writeChangesToConfigFile(configFileName);
                        }
                        else if (text[text.Length - 1].ToString() == "1")
                        {
                            reloadPermLevel = 1;
                            wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "dictionary reload permissions set to moderator");
                            writeChangesToConfigFile(configFileName);
                        }
                        else if (text[text.Length - 1].ToString() == "2")
                        {
                            reloadPermLevel = 2;
                            wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "dictionary reload permissions set to admin");
                            writeChangesToConfigFile(configFileName);
                        }
                        else
                        {
                            wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Syntax error");
                        }
                        return false;
                    }

                    //I'm going to just keep it so admins only can change this
                    if (text.Split()[0] == "!perms" && text.Split()[1] == "BWPirateChat" && GameMode.getPlayerBySocket(info.éäñåíéíìééä).isAdmin)
                        {
                            if (text[text.Length - 1].ToString() == "0")
                            {
                                permModeLevel = 0;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "BWPirateChat.dll perm set permissions set to user");
                                writeChangesToConfigFile(configFileName);
                            }
                            else if (text[text.Length - 1].ToString() == "1")
                            {
                                permModeLevel = 1;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "BWPirateChat.dll perm set permissions set to moderator");
                                writeChangesToConfigFile(configFileName);
                            }
                            else if (text[text.Length - 1].ToString() == "2")
                            {
                                permModeLevel = 2;
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "BWPirateChat.dll perm set permissions set to admin");
                                writeChangesToConfigFile(configFileName);
                            }
                            else
                            {
                                wno.òäóæåòîððòä("broadcastChat", info.éäñåíéíìééä, 1, 1, "game", "Syntax error");
                            }
                            return false;
                        }
                    //}
                        //return true;
                }
                if (pirateModeEnabled)
                {
                    string changedText = PirateTalkText(text);
                    text = changedText;
                }
                //text = "Windows XP is the coolest guy in all of Blackwake";

                return true;
            }
        }

        void Start()
        {
            newLog = new ModLogger("[BWPirateChat]", ModLoader.LogPath + "\\PirateChatLog.txt");

            if (!ïçîìäîóäìïæ.òîóëñëäêêòó)
            {
                newLog.Log("client side, so return and not do anything");
                return; 
            }
                newLog.Log("in the start function");
                HarmonyInstance.DEBUG = true;
            newLog.Log("set debug to true");
            HarmonyInstance harmonyInstancePirateChat = HarmonyInstance.Create("com.github.windowsxp.BWPirateChat");
            newLog.Log("created harmony instance");
            harmonyInstancePirateChat.PatchAll();
            newLog.Log("patched harmony instance");

            //read the dictionary file
            validFile = readDictionaryFile(dictionaryFileName);
            loadConfigFile(configFileName);
            prefixStrings = new string[4];
            prefixStrings[0] = "Avast!";
            prefixStrings[1] = "Arr!";
            prefixStrings[2] = "Avast, ye scurvy dogs!";
            prefixStrings[3] = "Arr, matey.";
        }

        private void Update()
        {
            wno = UI.îêêæëçäëèñî.ìòðëäìóîèäò.GetComponent<WakeNetObject>();
        }
            
        public static bool hasCorrectPermission(ïçîìäîóäìïæ.åéðñðçîîïêç info, int requiredPerm)
        {
            int playerPermissionLevel;
            if (GameMode.getPlayerBySocket(info.éäñåíéíìééä).isAdmin) playerPermissionLevel = 2;
            else if (GameMode.getPlayerBySocket(info.éäñåíéíìééä).isMod) playerPermissionLevel = 1;
            else playerPermissionLevel = 0;

            if (playerPermissionLevel >= requiredPerm)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool readDictionaryFile(string fileName)
        {
            //We need to do as much word parsing and processing as possible on startup to reduce lag. Adding a few extra seconds to server startup isn't near as bad as causing chat lag
            int counter = 0;
            string line;
            string secondWord = "";

            //if dictionary file is not found, switch to using the shitty built-in dictionary rules instead
            if (!File.Exists(fileName)) return false;

            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                int i = 0;
                WordConversion tempVar = new WordConversion();
                while (line[i] != '=')
                {
                    tempVar.originalWord += line[i];
                    i++;
                }
                newLog.Log("made it past the first while loop. Yay!");
                for (int u=i + 1; u < line.Length; u++)
                {
                    newLog.Log("u = " + u);
                    //keep in mind & is the yarrml whitespace character since spaces in my yarml file are used to seperate and delimit multiple entries
                    //if (line[u] == '&')
                    //{
                        //secondWord += ' ';
                    //}
                    //else
                    //{
                        secondWord += line[u];
                    //}
                    //this should get me a list of all the remaining characters in the line in one single variable
                }
                newLog.Log("made it past that one for loop. Woot!");
                newLog.Log("original word is " + tempVar.originalWord);
                int y = 0;
                foreach(string word in secondWord.Split(' '))
                {
                    if (word.Contains("&"))
                    {
                        tempVar.changeItToThis.Add(word.Replace("&", " "));
                        newLog.Log("found a new result word to add which is" + word.Replace("&", " "));
                    }
                    else
                    {
                        tempVar.changeItToThis.Add(word);
                        newLog.Log("found a new result word to add which is" + word);
                    }
                    y++;
                }
                if (y > 1) tempVar.multipleResults = true; else tempVar.multipleResults = false;
                listOfShitAndStuff.Add(new WordConversion());
                listOfShitAndStuff.ToArray()[counter].originalWord = tempVar.originalWord;
                listOfShitAndStuff.ToArray()[counter].changeItToThis = tempVar.changeItToThis;
                listOfShitAndStuff.ToArray()[counter].multipleResults = tempVar.multipleResults;
                secondWord = "";
                counter++;
            }

            file.Close();

            newLog.Log("there were " + counter + "lines parsed");

            /*foreach (WordConversion j in listOfShitAndStuff)
            {
                newLog.Log("original word = " + j.originalWord);
                newLog.Log("to be replaced by" + j.changeItToThis);
                newLog.Log("multiple results" + j.multipleResults.ToString());
            }*/

            //return false means there were zero results found and therefore file not found
            if (counter > 0)
            {
                newLog.Log("read dictionary file without crashing. yay!");
                return true;
            }
            else
            {
                newLog.Log("file " + dictionaryFileName + " not found.");
                return false;
            }

        }

        public static void loadConfigFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                //then create a new one
                newLog.Log("cfg not found. Creating new one");
                StreamWriter cfgFile = new StreamWriter(fileName);

                //populate with default values
                //0 = normal user. 1 = moderator. 2 = admin (for all the permission entries)
                cfgFile.WriteLine("pirate_talk_enabled=1");
                cfgFile.WriteLine("impersonate_perm_lvl=1");
                cfgFile.WriteLine("pirate_talk_toggle_perm_lvl=1");
                cfgFile.WriteLine("perm_changing_lvl=2");
                cfgFile.WriteLine("reload_perm_lvl=2");

                cfgFile.Close();
            }

            //read values from file
            //int counter = 0;
            string line;
            StreamReader cfgFileRead = new StreamReader(fileName);
            while ((line = cfgFileRead.ReadLine()) != null)
            {
                if (line.Contains("pirate_talk_enabled ="))
                {
                    //do whatever you want
                    if (line[line.Length - 1].ToString() == "0")
                    {
                        pirateModeEnabled = false;
                    }
                    else
                    {
                        pirateModeEnabled = true;
                    }
                }
                else if (line.Contains("impersonate_perm_lvl="))
                {
                    impersonatePermLevel = Convert.ToInt32(line[line.Length - 1]) - 48;
                }
                else if (line.Contains("pirate_talk_toggle_perm_lvl="))
                {
                    pirateModePermLevel = Convert.ToInt32(line[line.Length - 1]) - 48;
                }
                else if (line.Contains("perm_changing_lvl="))
                {
                    permModeLevel = Convert.ToInt32(line[line.Length - 1]) - 48;
                }
                else if (line.Contains("reload_perm_lvl="))
                {
                    reloadPermLevel = Convert.ToInt32(line[line.Length - 1]) - 48;
                }

            }

            cfgFileRead.Close();
            newLog.Log("impersonate perm = " + impersonatePermLevel + " pirate mode = " + pirateModeEnabled + " pirate toggle perm = " + pirateModePermLevel + " perm changing = " + permModeLevel + "reload perm level = " + reloadPermLevel);

        }

        public static void writeChangesToConfigFile(string fileName)
        {
            string enabledValue;
            if (pirateModeEnabled == true) enabledValue = "1"; else enabledValue = "0";
            File.Delete(fileName);
            newLog.Log("deleted old config file. getting ready to create a new one");
            StreamWriter cfgFile = new StreamWriter(fileName);

            //populate with default values
            //0 = normal user. 1 = moderator. 2 = admin (for all the permission entries)
            cfgFile.WriteLine("pirate_talk_enabled=" + enabledValue);
            cfgFile.WriteLine("impersonate_perm_lvl=" + impersonatePermLevel);
            cfgFile.WriteLine("pirate_talk_toggle_perm_lvl=" + pirateModePermLevel);
            cfgFile.WriteLine("perm_changing_lvl=" + permModeLevel);
            cfgFile.WriteLine("reload_perm_lvl" + reloadPermLevel);

            cfgFile.Close();
            newLog.Log("impersonate perm = " + impersonatePermLevel + " pirate mode = " + pirateModeEnabled + " pirate toggle perm = " + pirateModePermLevel + " perm changing = " + permModeLevel + "reload perm level = " + reloadPermLevel);
        }

        public static void Impersonate(ref string sender, ref string textInput)
        {
            string textToReturn = textInput.Replace("!impersonate", "");
            textToReturn.Remove(0, 1);
            string senderName = "";
            int i = 0;
            if (textToReturn[1].ToString() == "\"")
            {
                i = 2;
                while(textToReturn[i].ToString() != "\"")
                {
                    senderName += textToReturn[i].ToString();
                    i++;
                }
                textToReturn = textToReturn.Replace("\"" + senderName + "\" ", "");
                textToReturn = textToReturn.Substring(1);
                textInput = textToReturn;
                sender = senderName;
            }
            else
            {
                i = 1;
                while(textToReturn[i].ToString() != " ")
                {
                    senderName += textToReturn[i].ToString();
                    i++;
                }
                textToReturn = textToReturn.Replace(senderName, "");
                textToReturn = textToReturn.Substring(1);
                textInput = textToReturn;
                sender = senderName;
            }
        }

        public static string PirateTalkText(string originalText)
        {
            
            string newText = "";
            List<string> listOfWordsSoFar = new List<string>();

            if (!validFile)
            {
                //let's convert all the words to pirate speak
                foreach (string word in originalText.Split(' '))
                {
                    //List<string> listOfWordsSoFar = new List<string>();
                    if (word == "yeah" || word == "yea" || word == "yes")
                    {
                        listOfWordsSoFar.Add("yar");
                    }
                    else if (word == "hi" || word == "hello" || word == "ahoy")
                    {
                        listOfWordsSoFar.Add("ahoy");
                    }
                    else if (word == "hey")
                    {
                        listOfWordsSoFar.Add("avast");
                    }
                    else if (word == "your" || word == "youre" || word == "you're")//let's troll some grammer nazis while we're at it
                    {
                        listOfWordsSoFar.Add("yer");
                    }
                    else if (word == "you")
                    {
                        listOfWordsSoFar.Add("ye");
                    }
                    else if (word == "ok" || word == "alright" || word == "whatever")
                    {
                        listOfWordsSoFar.Add("arrr");
                    }
                    else
                    {
                        listOfWordsSoFar.Add(word);
                    }

                }
            }
            else
            {
                foreach (string word in (originalText.Split(' ')))
                {
                    if (word == "") continue;//not putting this here causes a bug where if the user enters more than 1 space between a word, no text makes it to the output
                    //treat words with ' in them the same as words without. For example "what's" is the same word as "whats"
                    //also, disregard capitalization since that gets automaticly put in. This is why it gets converted to lowercase
                    string word2 = word.ToLower().Replace("'", "");
                    bool matchFound = false;
                    bool putBackPunctuation = false;
                    bool firstCapital = false;
                    string altWordToMatch = "òîóëñëäêêòó3";//no chance in hell anyone will ever type that so it's safe to use this here
                    string punctuation = "";
                    string newEntry = "";

                    if (word.EndsWith(".") || word.EndsWith("!") || word.EndsWith(",") || word.EndsWith("?"))
                    {
                        putBackPunctuation = true;
                        punctuation = word[word.Length - 1].ToString();
                        altWordToMatch = word2.Substring(0, word.Length - 1);

                    }
                    if (char.IsUpper(word[0]))
                    {
                        firstCapital = true;
                    }
                    foreach (WordConversion query in listOfShitAndStuff)
                    {
                        //newLog.Log("word = " + word);
                        //newLog.Log("does " + word + " = " + query.originalWord + "?");
                        if (word2 == query.originalWord || altWordToMatch == query.originalWord)
                        {
                            //newLog.Log(word + " = " + query.originalWord);
                            matchFound = true;
                            if (!query.multipleResults)
                            {
                                newEntry = query.changeItToThis.ToArray()[0];
                                if (putBackPunctuation == true)
                                {
                                    newEntry += punctuation;
                                    putBackPunctuation = false;
                                }
                                if (firstCapital == true)
                                {
                                    newEntry = char.ToUpper(newEntry[0]).ToString() + newEntry.Substring(1);
                                    firstCapital = false;
                                }
                                else
                                {
                                    //just do nothing fuck it

                                    //listOfWordsSoFar.Add(query.changeItToThis.ToArray()[0]);
                                    //newEntry = query.changeItToThis.ToArray()[0];
                                }
                                listOfWordsSoFar.Add(newEntry);
                                //newLog.Log("the code is telling the game to replace " + word + " with " + query.changeItToThis.ToArray()[0]);
                            }
                            else if (query.multipleResults == true)
                            {
                                System.Random rnd = new System.Random();
                                newEntry = query.changeItToThis.ToArray()[rnd.Next(0, query.changeItToThis.Count)];
                                if (putBackPunctuation == true)
                                {
                                    //listOfWordsSoFar.Add(query.changeItToThis.ToArray()[rnd.Next(0, query.changeItToThis.Count)] + punctuation);
                                    newEntry += punctuation;
                                    putBackPunctuation = false;
                                }
                                if (firstCapital == true)
                                {
                                    newEntry = char.ToUpper(newEntry[0]).ToString() + newEntry.Substring(1);
                                    firstCapital = false;
                                }
                                else
                                {
                                    //listOfWordsSoFar.Add(query.changeItToThis.ToArray()[rnd.Next(0, query.changeItToThis.Count)]);
                                }
                                listOfWordsSoFar.Add(newEntry);
                            }
                        }
                    }
                    putBackPunctuation = false;
                    firstCapital = false;
                    if (matchFound == false)
                    {
                        listOfWordsSoFar.Add(word);
                    }
                    matchFound = false;
                    altWordToMatch = "òîóëñëäêêòó3";
                }
            }

            //random chance of adding a pirate prefix string
            if (randomAssNumber.Next(0,100) > 65)
            {
                newText += prefixStrings[randomAssNumber.Next(0, 3)] + " ";
            }

            //now, let's reconstruct all the words we saved into a single string
            foreach (string savedWord in listOfWordsSoFar)
            {
                newText += savedWord + " ";
            }

            //random chance of adding "arr" to the end
            if (randomAssNumber.Next(0,100) < 40)
            {
                if (newText[newText.Length - 2].ToString() == "?" || newText[newText.Length - 2].ToString() == "!" || newText[newText.Length - 2].ToString() == ".")
                {
                    newText += "Arr! ";
                }
                else
                {
                    newText = newText.Remove(newText.Length - 1);
                    newText += ". Arr! ";
                }

            }

            return newText;
        }

    }
}
