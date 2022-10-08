using BepInEx;
using I2.Loc;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;
using TMPro;
using System.IO;
using BepInEx.Configuration;
using XYModLib;
using Newtonsoft.Json;
using System;
using I2LocPatch;

namespace DinkumChinese
{
    [BepInPlugin("xiaoye97.Dinkum.DinkumChinese", "DinkumChinese", "1.10.0")]
    public class DinkumChinesePlugin : BaseUnityPlugin
    {
        public static DinkumChinesePlugin Inst;

        public static bool Pause
        {
            get
            {
                return pause;
            }
            set
            {
                pause = value;
            }
        }

        private static bool pause;

        public ConfigEntry<bool> DevMode;
        public ConfigEntry<bool> DontLoadLocOnDevMode;
        public ConfigEntry<bool> LogNoTranslation;

        public List<TextLocData> DynamicTextLocList = new List<TextLocData>();
        public List<TextLocData> PostTextLocList = new List<TextLocData>();
        public List<TextLocData> QuestTextLocList = new List<TextLocData>();
        public List<TextLocData> TipsTextLocList = new List<TextLocData>();
        public List<TextLocData> MailTextLocList = new List<TextLocData>();
        public List<TextLocData> AnimalsTextLocList = new List<TextLocData>();

        public UIWindow DebugWindow;
        public UIWindow ErrorWindow;
        public string ErrorStr;
        public bool IsPluginLoaded;

        private void Awake()
        {
            Inst = this;
            DevMode = Config.Bind<bool>("Dev", "DevMode", false, "En mode développement, vous pouvez appuyer sur la touche de raccourci pour déclencher la fonction de développement");
            DontLoadLocOnDevMode = Config.Bind<bool>("Dev", "DontLoadLocOnDevMode", true, "En mode développement, la traduction DynamicText Post Quest n'est pas chargée, ce qui est pratique pour le dump");
            LogNoTranslation = Config.Bind<bool>("Tool", "LogNoTranslation", true, "Peut générer des cibles non traduites");
            DebugWindow = new UIWindow("Outil de test chinois [Ctrl+clavier 4]");
            DebugWindow.OnWinodwGUI = DebugWindowGUI;
            ErrorWindow = new UIWindow("Erreur de sinisation");
            ErrorWindow.OnWinodwGUI = ErrorWindowFunc;
            try
            {
                Harmony.CreateAndPatchAll(typeof(DinkumChinesePlugin));
                Harmony.CreateAndPatchAll(typeof(ILPatch));
                Harmony.CreateAndPatchAll(typeof(StringReturnPatch));
                Harmony.CreateAndPatchAll(typeof(StartTranslatePatch));
                Harmony.CreateAndPatchAll(typeof(SpritePatch));
            }
            catch (ExecutionEngineException ex)
            {
                ErrorStr = $"Il y a une erreur en chinois. Il est supposé que le nom d'utilisateur ou le chemin du jeu contient des caractères non anglais. \nInformations sur l'exception :\n{ex}";
                ErrorWindow.Show = true;
            }
            catch (Exception ex)
            {
                ErrorStr = $"Il y a une erreur en chinois. \nInformations sur les exceptions :\n{ex}";
                ErrorWindow.Show = true;
            }
            if (DevMode.Value && DontLoadLocOnDevMode.Value)
            {
                return;
            }
            Invoke("LogFlagTrue", 2f);
            DynamicTextLocList = TextLocData.LoadFromTxtFile($"{Paths.PluginPath}/I2LocPatch/DynamicTextLoc.txt");
            PostTextLocList = TextLocData.LoadFromJsonFile($"{Paths.PluginPath}/I2LocPatch/PostTextLoc.json");
            QuestTextLocList = TextLocData.LoadFromJsonFile($"{Paths.PluginPath}/I2LocPatch/QuestTextLoc.json");
            TipsTextLocList = TextLocData.LoadFromJsonFile($"{Paths.PluginPath}/I2LocPatch/TipsTextLoc.json");
            MailTextLocList = TextLocData.LoadFromJsonFile($"{Paths.PluginPath}/I2LocPatch/MailTextLoc.json");
            AnimalsTextLocList = TextLocData.LoadFromJsonFile($"{Paths.PluginPath}/I2LocPatch/AnimalsTextLoc.json");
        }

        public void LogFlagTrue()
        {
            IsPluginLoaded = true;
        }

        public void ErrorWindowFunc()
        {
            GUILayout.Label(ErrorStr);
        }

        private void Start()
        {
            OnGameStartOnceFix();
        }

        private void Update()
        {
            if (DevMode.Value)
            {
                // Ctrl + 小键盘4 切换GUI (Ctrl + Pavé numérique 4 Basculer l'interface graphique)
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad4))
                {
                    DebugWindow.Show = !DebugWindow.Show;
                }
                // Ctrl + 小键盘5 切换暂停游戏，游戏速度1 (Ctrl + Pavé numérique 5 bascule le jeu en pause, vitesse de jeu 1)
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Pause = !Pause;
                    Time.timeScale = Pause ? 0 : 1;
                }
                // Ctrl + 小键盘6 切换暂停游戏，游戏速度10 (Commutateur Ctrl + Numpad 6 pour mettre le jeu en pause, la vitesse de jeu est de 10)
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Pause = !Pause;
                    Time.timeScale = Pause ? 1 : 10;
                }
                // Ctrl + 小键盘7 dump场景内所有文本，不包括隐藏的文本 (Ctrl + Numpad 7 vide tout le texte de la scène, à l'exception du texte masqué)
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad7))
                {
                    DumpText(false);
                }
                // Ctrl + 小键盘8 dump场景内所有文本，包括隐藏的文本 (Ctrl + Numpad 8 vide tout le texte de la scène, y compris le texte masqué)
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad8))
                {
                    DumpText(true);
                }
            }
            FixChatFont();
        }

        private void OnGUI()
        {
            DebugWindow.OnGUI();
            ErrorWindow.OnGUI();
        }

        private Vector2 cv;

        public void DebugWindowGUI()
        {
            GUILayout.BeginVertical("Ruban", GUI.skin.window);
            if (GUILayout.Button("[Ctrl+Numpad 5] Commutateur pour mettre le jeu en pause, vitesse de jeu 1"))
            {
                Pause = !Pause;
                Time.timeScale = Pause ? 0 : 1;
            }
            if (GUILayout.Button("[Ctrl + Pavé numérique 6] Commutez pour mettre le jeu en pause, la vitesse du jeu est de 10"))
            {
                Pause = !Pause;
                Time.timeScale = Pause ? 1 : 10;
            }
            if (GUILayout.Button("vérifier les crochets"))
            {
                CheckKuoHao();
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Dump", GUI.skin.window);
            if (GUILayout.Button("[Ctrl + Pavé numérique 7] vide tout le texte de la scène, à l'exception du texte masqué"))
            {
                DumpText(false);
            }
            if (GUILayout.Button("[Ctrl + Pavé numérique 8] vider tout le texte de la scène, y compris le texte masqué"))
            {
                DumpText(true);
            }
            if (GUILayout.Button("vider tous les dialogues qui ne sont pas dans la table multilingue (doit être inachevé)"))
            {
                DumpAllConversation();
            }
            if (GUILayout.Button("poste de décharge (nécessite un état inachevé)"))
            {
                DumpAllPost();
            }
            if (GUILayout.Button("quête de vidage (nécessite un état inachevé)"))
            {
                DumpAllQuest();
            }
            if (GUILayout.Button("dump mail (doit être inachevé)"))
            {
                DumpAllMail();
            }
            if (GUILayout.Button("dump tips (nécessite un état inachevé)"))
            {
                DumpAllTips();
            }
            if (GUILayout.Button("jeter des animaux (doit être inachevé)"))
            {
                DumpAnimals();
            }
            if (GUILayout.Button("Vider les éléments sans clés de traduction (doit être inachevé)"))
            {
                DumpAllUnTermItem();
            }
            GUILayout.EndVertical();
        }

        private int lastChatCount;
        private bool isChatHide;
        private float showChatCD;

        public void FixChatFont()
        {
            if (ChatBox.chat != null)
            {
                if (isChatHide)
                {
                    showChatCD -= Time.deltaTime;
                    if (showChatCD < 0)
                    {
                        isChatHide = false;
                        foreach (var chat in ChatBox.chat.chatLog)
                        {
                            chat.contents.enabled = false;
                            chat.contents.enabled = true;
                        }
                    }
                }
                if (ChatBox.chat.chatLog.Count != lastChatCount)
                {
                    lastChatCount = ChatBox.chat.chatLog.Count;
                    isChatHide = true;
                    showChatCD = 0.5f;
                }
            }
        }

        public static void LogInfo(string log)
        {
            Inst.Logger.LogInfo(log);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(OptionsMenu), "Start")]
        public static void OptionsMenuStartPatch()
        {
            LocalizationManager.CurrentLanguage = "French";
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RealWorldTimeLight), "setUpDayAndDate")]
        public static bool RealWorldTimeLight_setUpDayAndDate_Patch(RealWorldTimeLight __instance)
        {
            __instance.seasonAverageTemp = __instance.seasonAverageTemps[WorldManager.manageWorld.month - 1];
            __instance.DayText.text = __instance.getDayName(WorldManager.manageWorld.day - 1);
            __instance.DateText.text = (WorldManager.manageWorld.day + (WorldManager.manageWorld.week - 1) * 7).ToString("00");
            __instance.SeasonText.text = __instance.getSeasonName(WorldManager.manageWorld.month - 1);
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Conversation), "getIntroName")]
        public static bool Conversation_getIntroName(Conversation __instance, ref string __result, int i)
        {
            if (Inst.DevMode.Value && Inst.DontLoadLocOnDevMode.Value) return true;
            string result = $"{__instance.saidBy}/{__instance.gameObject.name}_Intro_{i.ToString("D3")}";
            __result = result;
            if (!LocalizationManager.Sources[0].ContainsTerm(result))
            {
                if (__instance.startLineAlt.aConverstationSequnce.Length > i)
                {
                    if (string.IsNullOrWhiteSpace(__instance.startLineAlt.aConverstationSequnce[i]))
                    {
                        __result = result;
                    }
                    else
                    {
                        __result = result + "_" + __instance.startLineAlt.aConverstationSequnce[i].GetHashCode();
                    }
                }
            }
            if (Inst.DevMode.Value)
                Debug.Log($"Conversation_getIntroName {__result}");
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Conversation), "getOptionName")]
        public static bool Conversation_getOptionName(Conversation __instance, ref string __result, int i)
        {
            if (Inst.DevMode.Value && Inst.DontLoadLocOnDevMode.Value) return true;
            string result = $"{__instance.saidBy}/{__instance.gameObject.name}_Option_{i.ToString("D3")}";
            __result = result;
            if (!LocalizationManager.Sources[0].ContainsTerm(result))
            {
                if (__instance.optionNames.Length > i)
                {
                    if (string.IsNullOrWhiteSpace(__instance.optionNames[i]))
                    {
                        __result = result;
                    }
                    else
                    {
                        __result = result + "_" + __instance.optionNames[i].GetHashCode();
                    }
                }
            }
            if (Inst.DevMode.Value)
                Debug.Log($"Conversation_getOptionName {__result}");
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Conversation), "getResponseName")]
        public static bool Conversation_getResponseName(Conversation __instance, ref string __result, int i, int r)
        {
            if (Inst.DevMode.Value && Inst.DontLoadLocOnDevMode.Value) return true;
            string result = $"{__instance.saidBy}/{__instance.gameObject.name}_Response_{i.ToString("D3")}_{r.ToString("D3")}";
            __result = result;
            if (!LocalizationManager.Sources[0].ContainsTerm(result))
            {
                if (__instance.responesAlt.Length > i)
                {
                    if (__instance.responesAlt[i].aConverstationSequnce.Length > r)
                    {
                        if (string.IsNullOrWhiteSpace(__instance.responesAlt[i].aConverstationSequnce[r]))
                        {
                            __result = result;
                        }
                        else
                        {
                            __result = result + "_" + __instance.responesAlt[i].aConverstationSequnce[r].GetHashCode();
                        }
                    }
                }
            }
            if (Inst.DevMode.Value)
                Debug.Log($"Conversation_getResponseName {__result}");
            return false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(LocalizationManager), "TryGetTranslation")]
        public static void Localize_OnLocalize(string Term, bool __result)
        {
            if (Inst.IsPluginLoaded && Inst.LogNoTranslation.Value)
            {
                if (!__result)
                {
                    Debug.LogWarning($"LocalizationManager n'a pas réussi à obtenir la traduction:Term:{Term}");
                }
            }
        }

        public static Queue<TextMeshProUGUI> waitShowTMPs = new Queue<TextMeshProUGUI>();

        /// <summary>
        /// 检查翻译中的括号是否匹配 (Vérifier si les parenthèses dans la traduction correspondent)
        /// </summary>
        public void CheckKuoHao()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            // 索引和Excel表中的行对应的偏移 (L'index et l'offset correspondant à la ligne dans le tableau Excel)
            int hangOffset = 3;
            int findCount = 0;
            StringBuilder sb = new StringBuilder();
            LogInfo($"Commencez à vérifier les parenthèses dans les traductions:");
            Regex reg = new Regex(@"(?is)(?<=\<)[^\>]+(?=\>)");
            var mResourcesCache = Traverse.Create(ResourceManager.pInstance).Field("mResourcesCache").GetValue<Dictionary<string, UnityEngine.Object>>();
            LanguageSourceAsset asset = mResourcesCache.Values.First() as LanguageSourceAsset;
            int len = asset.SourceData.mTerms.Count;
            for (int i = 0; i < len; i++)
            {
                var term = asset.SourceData.mTerms[i];
                if (string.IsNullOrWhiteSpace(term.Languages[3])) continue;
                MatchCollection mc1 = reg.Matches(term.Languages[0]);
                MatchCollection mc2 = reg.Matches(term.Languages[3]);
                if (mc1.Count != mc2.Count)
                {
                    string log = $"numéro de ligne:{i + hangOffset} Key:{term.Term} Nombre de parenthèses incohérent{mc1.Count}entre parenthèses en chinois{mc2.Count}parenthèses";
                    LogInfo(log);
                    sb.AppendLine(log);
                    findCount++;
                }
                else if (mc1.Count > 0)
                {
                    for (int j = 0; j < mc1.Count; j++)
                    {
                        if (mc1[j].Value != mc2[j].Value)
                        {
                            string log = $"numéro de ligne:{i + hangOffset} Key:{term.Term} dans le{j}Incompatible avec les parenthèses dans la version originale:<{mc1[j].Value}> en traduction:<{mc2[j].Value}>";
                            LogInfo(log);
                            sb.AppendLine(log);
                            findCount++;
                        }
                    }
                }
            }
            sw.Stop();
            LogInfo($"Inspection terminée，venez{findCount}article en question，long{sw.ElapsedMilliseconds}ms");
            System.IO.File.WriteAllText($"{Paths.GameRootPath}/CheckKuoHao.txt", sb.ToString());
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string GetPath(Transform t)
        {
            List<string> paths = new List<string>();
            StringBuilder sb = new StringBuilder();
            paths.Add(t.name);
            Transform p = t.parent;
            while (p != null)
            {
                paths.Add(p.name);
                p = p.parent;
            }
            for (int i = paths.Count - 1; i >= 0; i--)
            {
                sb.Append(paths[i]);
                if (i != 0)
                {
                    sb.Append('/');
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 当游戏开始时只需要一次的处理 (Un seul traitement est requis au démarrage du jeu)
        /// </summary>
        public void OnGameStartOnceFix()
        {
            // 动物的生物群系翻译 (traduction biome animal)
            //AnimalManager.manage.northernOceanFish.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.northernOceanFish.locationName);
            //AnimalManager.manage.southernOceanFish.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.southernOceanFish.locationName);
            //AnimalManager.manage.riverFish.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.riverFish.locationName);
            //AnimalManager.manage.mangroveFish.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.mangroveFish.locationName);
            //AnimalManager.manage.billabongFish.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.billabongFish.locationName);
            //AnimalManager.manage.topicalBugs.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.topicalBugs.locationName);
            //AnimalManager.manage.desertBugs.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.desertBugs.locationName);
            //AnimalManager.manage.bushlandBugs.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.bushlandBugs.locationName);
            //AnimalManager.manage.pineLandBugs.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.pineLandBugs.locationName);
            //AnimalManager.manage.plainsBugs.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.plainsBugs.locationName);
            //AnimalManager.manage.underWaterOceanCreatures.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.underWaterOceanCreatures.locationName);
            //AnimalManager.manage.underWaterRiverCreatures.locationName =
            //    TextLocData.GetLoc(DynamicTextLocList, AnimalManager.manage.underWaterRiverCreatures.locationName);
        }

        #region Dump

        /// <summary>
        /// Dump当前的文本
        /// </summary>
        /// <param name="includeInactive"></param>
        public void DumpText(bool includeInactive)
        {
            StringBuilder sb = new StringBuilder();
            var tmps = GameObject.FindObjectsOfType<TextMeshProUGUI>(includeInactive);
            foreach (var tmp in tmps)
            {
                var i2 = tmp.GetComponent<Localize>();
                if (i2 != null) continue;
                sb.AppendLine("===========");
                sb.AppendLine($"path:{GetPath(tmp.transform)}");
                sb.AppendLine($"text:{tmp.text.StrToI2Str()}");
            }
            File.WriteAllText($"{Paths.GameRootPath}/I2/TextDump.txt", sb.ToString());
            LogInfo($"Dump完毕,{Paths.GameRootPath}/I2/TextDump.txt");
        }

        public void DumpAllConversation()
        {
            List<Conversation> conversations = new List<Conversation>();
            // 直接从资源搜索单独的Conversation
            conversations.AddRange(Resources.FindObjectsOfTypeAll<Conversation>());

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine($"Key\tEnglish");
            List<string> terms = new List<string>();
            I2File i2File = new I2File();
            i2File.Name = "NoTermConversation";
            i2File.Languages = new List<string>() { "English" };

            foreach (var c in conversations)
            {
                // Intro
                for (int i = 0; i < c.startLineAlt.aConverstationSequnce.Length; i++)
                {
                    string key = c.getIntroName(i);
                    if (!LocalizationManager.Sources[0].ContainsTerm(key))
                    {
                        if (!string.IsNullOrWhiteSpace(c.startLineAlt.aConverstationSequnce[i]))
                        {
                            string term = $"{key}_{c.startLineAlt.aConverstationSequnce[i].GetHashCode()}";
                            string line = $"{term}\t{c.startLineAlt.aConverstationSequnce[i].StrToI2Str()}";
                            if (terms.Contains(term))
                            {
                                string log = $"Répéter le terme, ignorer. {line}";
                                Logger.LogError(log);
                            }
                            else
                            {
                                terms.Add(term);
                                TermLine termLine = new TermLine();
                                termLine.Name = term;
                                termLine.Texts = new string[] { c.startLineAlt.aConverstationSequnce[i] };
                                i2File.Lines.Add(termLine);
                                //sb.AppendLine(line);
                                LogInfo(line);
                            }
                        }
                    }
                }
                // Option
                for (int j = 0; j < c.optionNames.Length; j++)
                {
                    if (!c.optionNames[j].Contains("<"))
                    {
                        string key = c.getOptionName(j);
                        if (!LocalizationManager.Sources[0].ContainsTerm(key))
                        {
                            if (!string.IsNullOrWhiteSpace(c.optionNames[j]))
                            {
                                string term = $"{key}_{c.optionNames[j].GetHashCode()}";
                                string line = $"{term}\t{c.optionNames[j].StrToI2Str()}";
                                if (terms.Contains(term))
                                {
                                    string log = $"Répéter le terme, ignorer. {line}";
                                    Logger.LogError(log);
                                }
                                else
                                {
                                    terms.Add(term);
                                    //sb.AppendLine(line);
                                    TermLine termLine = new TermLine();
                                    termLine.Name = term;
                                    termLine.Texts = new string[] { c.optionNames[j] };
                                    i2File.Lines.Add(termLine);
                                    LogInfo(line);
                                }
                            }
                        }
                    }
                }
                // Respone
                for (int k = 0; k < c.responesAlt.Length; k++)
                {
                    for (int l = 0; l < c.responesAlt[k].aConverstationSequnce.Length; l++)
                    {
                        string key = c.getResponseName(k, l);
                        if (!LocalizationManager.Sources[0].ContainsTerm(key))
                        {
                            if (!string.IsNullOrWhiteSpace(c.responesAlt[k].aConverstationSequnce[l]))
                            {
                                string term = $"{key}_{c.responesAlt[k].aConverstationSequnce[l].GetHashCode()}";
                                string line = $"{term}\t{c.responesAlt[k].aConverstationSequnce[l].StrToI2Str()}";
                                if (terms.Contains(term))
                                {
                                    string log = $"Répéter le terme, ignorer.{line}";
                                    Logger.LogError(log);
                                }
                                else
                                {
                                    terms.Add(term);
                                    //sb.AppendLine(line);
                                    TermLine termLine = new TermLine();
                                    termLine.Name = term;
                                    termLine.Texts = new string[] { c.responesAlt[k].aConverstationSequnce[l] };
                                    i2File.Lines.Add(termLine);
                                    LogInfo(line);
                                }
                            }
                        }
                    }
                }
            }
            i2File.WriteCSVTable($"{Paths.GameRootPath}/I2/{i2File.Name}.csv");
            LogInfo($"Dump {i2File.Name}完毕");
        }

        public void DumpAllPost()
        {
            List<BullitenBoardPost> list = new List<BullitenBoardPost>();
            list.Add(BulletinBoard.board.announcementPosts[0]);
            list.Add(BulletinBoard.board.huntingTemplate);
            list.Add(BulletinBoard.board.captureTemplate);
            list.Add(BulletinBoard.board.tradeTemplate);
            list.Add(BulletinBoard.board.photoTemplate);
            list.Add(BulletinBoard.board.cookingTemplate);
            list.Add(BulletinBoard.board.smeltingTemplate);
            list.Add(BulletinBoard.board.compostTemplate);
            list.Add(BulletinBoard.board.sateliteTemplate);
            list.Add(BulletinBoard.board.craftingTemplate);
            list.Add(BulletinBoard.board.shippingRequestTemplate);
            List<TextLocData> list2 = new List<TextLocData>();
            foreach (var p in list)
            {
                list2.Add(new TextLocData(p.title, ""));
                list2.Add(new TextLocData(p.contentText, ""));
            }
            var json = JsonConvert.SerializeObject(list2, Formatting.Indented);
            File.WriteAllText($"{Paths.GameRootPath}/I2/PostTextLoc.json", json);
            Debug.Log(json);
        }

        public void DumpAllQuest()
        {
            var mgr = QuestManager.manage;
            List<TextLocData> list = new List<TextLocData>();
            foreach (var q in mgr.allQuests)
            {
                list.Add(new TextLocData(q.QuestName, ""));
                list.Add(new TextLocData(q.QuestDescription, ""));
            }
            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText($"{Paths.GameRootPath}/I2/QuestTextLoc.json", json);
            Debug.Log(json);
        }

        public void DumpAllMail()
        {
            var mgr = MailManager.manage;
            List<TextLocData> list = new List<TextLocData>();
            list.Add(new TextLocData(mgr.animalResearchLetter.letterText, ""));
            list.Add(new TextLocData(mgr.returnTrapLetter.letterText, ""));
            list.Add(new TextLocData(mgr.devLetter.letterText, ""));
            list.Add(new TextLocData(mgr.catalogueItemLetter.letterText, ""));
            list.Add(new TextLocData(mgr.craftmanDayOff.letterText, ""));
            foreach (var m in mgr.randomLetters) list.Add(new TextLocData(m.letterText, ""));
            foreach (var m in mgr.thankYouLetters) list.Add(new TextLocData(m.letterText, ""));
            foreach (var m in mgr.didNotFitInInvLetter) list.Add(new TextLocData(m.letterText, ""));
            foreach (var m in mgr.fishingTips) list.Add(new TextLocData(m.letterText, ""));
            foreach (var m in mgr.bugTips) list.Add(new TextLocData(m.letterText, ""));
            foreach (var m in mgr.licenceLevelUp) list.Add(new TextLocData(m.letterText, ""));
            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText($"{Paths.GameRootPath}/I2/MailTextLoc.json", json);
            Debug.Log(json);
        }

        public void DumpAllTips()
        {
            var mgr = GameObject.FindObjectOfType<LoadingScreenImageAndTips>(true);
            List<TextLocData> list = new List<TextLocData>();
            foreach (var tip in mgr.tips) list.Add(new TextLocData(tip, ""));
            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText($"{Paths.GameRootPath}/I2/TipsTextLoc.json", json);
            Debug.Log(json);
        }

        public void DumpAnimals()
        {
            var mgr = AnimalManager.manage;
            List<TextLocData> list = new List<TextLocData>();
            foreach (var a in mgr.allAnimals) list.Add(new TextLocData(a.animalName, ""));
            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText($"{Paths.GameRootPath}/I2/AnimalsTextLoc.json", json);
            Debug.Log(json);
        }

        public void DumpAllUnTermItem()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Key\tEnglish");
            List<string> keys = new List<string>();
            foreach (var item in Inventory.inv.allItems)
            {
                int id = Inventory.inv.getInvItemId(item);
                string nameKey = "InventoryItemNames/InvItem_" + id.ToString();
                string descKey = "InventoryItemDescriptions/InvDesc_" + id.ToString();
                if (!LocalizationManager.Sources[0].ContainsTerm(nameKey))
                {
                    string line = nameKey + "\t" + item.itemName;
                    LogInfo(line);
                    if (keys.Contains(nameKey))
                    {
                        string log = $"出现重复的key {nameKey} 已阻止此项添加";
                        Logger.LogError(log);
                    }
                    else
                    {
                        keys.Add(nameKey);
                        sb.AppendLine(line);
                    }
                }
                if (!LocalizationManager.Sources[0].ContainsTerm(descKey))
                {
                    string line = descKey + "\t" + item.itemDescription;
                    LogInfo(line);
                    if (keys.Contains(descKey))
                    {
                        string log = $"出现重复的key {descKey} 已阻止此项添加";
                        Logger.LogError(log);
                    }
                    else
                    {
                        keys.Add(descKey);
                        sb.AppendLine(line);
                    }
                }
            }
            File.WriteAllText($"{Paths.GameRootPath}/I2/UnTermItem.csv", sb.ToString());
        }

        #endregion Dump
    }
}