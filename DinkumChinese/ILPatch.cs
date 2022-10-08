using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace DinkumChinese
{
    public static class ILPatch
    {
        [HarmonyTranspiler, HarmonyPatch(typeof(AnimalHouseMenu), "fillData")]
        public static IEnumerable<CodeInstruction> AnimalHouseMenu_fillData_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " Year", " Année");
            instructions = ReplaceIL(instructions, " Month", " Mois");
            instructions = ReplaceIL(instructions, " Day", " Jour");
            instructions = ReplaceIL(instructions, "s", "s");
            instructions = ReplaceIL(instructions, "SELL ", "VENDRE ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(AnimalHouseMenu), "openConfirm")]
        public static IEnumerable<CodeInstruction> AnimalHouseMenu_openConfirm_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Sell ", "Vendre ");
            instructions = ReplaceIL(instructions, " for <sprite=11>", " pour <sprite=11>");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BankMenu), "convertButton")]
        public static IEnumerable<CodeInstruction> BankMenu_convertButton_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Convert [<sprite=11> 500 for <sprite=15> 1]", "转换 [<sprite=11> 500 到 <sprite=15> 1]");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BankMenu), "depositButton")]
        public static IEnumerable<CodeInstruction> BankMenu_depositButton_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Deposit", "Déposer");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BankMenu), "open")]
        public static IEnumerable<CodeInstruction> BankMenu_open_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Account Balance", "Solde du compte");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BankMenu), "openAsDonations")]
        public static IEnumerable<CodeInstruction> BankMenu_openAsDonations_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Town Debt", "Dette de la ville");
            instructions = ReplaceIL(instructions, "Donate", "Faire un don");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BankMenu), "withdrawButton")]
        public static IEnumerable<CodeInstruction> BankMenu_withdrawButton_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Withdraw", "Retirer");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BugAndFishCelebration), "openWindow")]
        public static IEnumerable<CodeInstruction> BugAndFishCelebration_openWindow_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "I caught a ", "J'ai caught un ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BulletinBoard), "getMissionText")]
        public static IEnumerable<CodeInstruction> BulletinBoard_getMissionText_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "<sprite=12> Trade ", "<sprite=12> Échanger ");
            instructions = ReplaceIL(instructions, " with ", " avec ");
            instructions = ReplaceIL(instructions, "<sprite=12> Speak to ", "<sprite=12> Parler à ");
            instructions = ReplaceIL(instructions, "<sprite=12> Hunt down the ", "<sprite=12> Traquer le ");
            instructions = ReplaceIL(instructions, " using its last know location on the map", "Utiliser le dernier emplacement connu sur la carte");
            instructions = ReplaceIL(instructions, "<sprite=13> Visit the location on the map to investigate", "<sprite=13> Visitez l'emplacement sur la carte pour enquêter");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BulletinBoard), "showSelectedPost")]
        public static IEnumerable<CodeInstruction> BulletinBoard_showSelectedPost_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "EXPIRED", "EXPIRÉ");
            instructions = ReplaceIL(instructions, " Last Day", " Dernier jour");
            instructions = ReplaceIL(instructions, " Days Remaining", " Jours restants");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BullitenBoardPost), "getBoardRequestItem")]
        public static IEnumerable<CodeInstruction> BullitenBoardPost_getBoardRequestItem_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "any other furniture", "tout autre meuble");
            instructions = ReplaceIL(instructions, "any other clothing", "tout autre vêtement");
            return instructions;
        }

        // todo
        [HarmonyTranspiler, HarmonyPatch(typeof(CameraController), "moveCameraToShowPos", MethodType.Enumerator)]
        public static IEnumerable<CodeInstruction> CameraController_moveCameraToShowPos_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " is visiting the island!", "visite l'île !");
            instructions = ReplaceIL(instructions, "Someone is visiting the island!", "Quelqu'un visite l'île !");
            instructions = ReplaceIL(instructions, "No one is visiting today...", "Personne n'est venu aujourd'hui...");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(ConversationManager), "checkLineForReplacement")]
        public static IEnumerable<CodeInstruction> ConversationManager_checkLineForReplacement_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "South City", "South City");
            instructions = ReplaceIL(instructions, "Journal", "Journal");
            instructions = ReplaceIL(instructions, "Licence", "Licence");
            instructions = ReplaceIL(instructions, "Licences", "Licences");
            instructions = ReplaceIL(instructions, "Airship", "Dirigeable");
            instructions = ReplaceIL(instructions, "Nomad", "Nomade");
            instructions = ReplaceIL(instructions, "Nomads", "Nomades");
            instructions = ReplaceIL(instructions, "I just love the colours!", "J'adore les couleurs !");
            instructions = ReplaceIL(instructions, "I love this one.", "J'aime celui la.");
            instructions = ReplaceIL(instructions, "The composition is wonderful", "Cette combinaison est incroyable");
            instructions = ReplaceIL(instructions, "It speaks to me, you know?", "Ça me parle, tu sais ?");
            instructions = ReplaceIL(instructions, "It makes me feel something...", "Ça me fait un peu envie...");
            instructions = ReplaceIL(instructions, "Made by hand by yours truly!", "Fabriqué à la main par votre serviteur !");
            instructions = ReplaceIL(instructions, "Finished that one off today!", "J'en ai fini un aujourd'hui !");
            instructions = ReplaceIL(instructions, "It feels just right for you, ", "Il se sent juste bien pour vous,");
            instructions = ReplaceIL(instructions, "The colour is very powerful, y'know?", "Les couleurs sont puissantes, vous savez ? ");
            instructions = ReplaceIL(instructions, "It will open your chakras, y'know?", "Ça va ouvrir tes chakras, tu sais ?");
            instructions = ReplaceIL(instructions, "Do you feel the engery coming from it?", "Sentez-vous l'énergie qui s'en dégage ?");
            instructions = ReplaceIL(instructions, "I feel good things coming to whoever buys it.", "Je sens que de bonnes choses arrivent à celui qui l'achète.");
            instructions = ReplaceIL(instructions, "The design just came to me, y'know?", "Le design vient de me venir, tu vois ?");
            instructions = ReplaceIL(instructions, "Y'know, that would look great on you, ", "Tu sais, ça t'irait bien,");
            instructions = ReplaceIL(instructions, "I put a lot of myself into this one.", "J'ai mis beaucoup d'efforts là-dedans.");
            instructions = ReplaceIL(instructions, "Beginning...", "Au début...");
            instructions = ReplaceIL(instructions, "...Nothing happened...", "...Rien ne s'est passé...");
            instructions = ReplaceIL(instructions, "Permit Points", "Points de licence");
            instructions = ReplaceIL(instructions, "s", "s");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(ConversationManager), "talkToNPC")]
        public static IEnumerable<CodeInstruction> ConversationManager_talkToNPC_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "A new deed is available!", "Nouveaux contrats disponibles!");
            instructions = ReplaceIL(instructions, "Talk to Fletch to apply for deeds.", "Parlez à Fletch de la demande d'acte.");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(CraftingManager), "populateCraftList")]
        public static IEnumerable<CodeInstruction> CraftingManager_populateCraftList_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "COOK", "<color=#F87474>CUISINER</color>");
            instructions = ReplaceIL(instructions, "COOKING", "CUISINE");
            instructions = ReplaceIL(instructions, "COMMISSION", "<color=#F87474>COMMISSION</color>");
            instructions = ReplaceIL(instructions, "CRAFTING", "ARTISANAT");
            instructions = ReplaceIL(instructions, "CRAFT", "<color=#F87474>ARTISANAT</color>");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(EquipItemToChar), "OnDestroy")]
        public static IEnumerable<CodeInstruction> EquipItemToChar_OnDestroy_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " has left", "  est parti");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(EquipItemToChar), "UserCode_RpcCharacterJoinedPopup")]
        public static IEnumerable<CodeInstruction> EquipItemToChar_UserCode_RpcCharacterJoinedPopup_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Welcome to ", "Bienvenue à ");
            instructions = ReplaceIL(instructions, " has joined", " à rejoint");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(ExhibitSign), "Start")]
        public static IEnumerable<CodeInstruction> ExhibitSign_Start_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "This exhibit is currently empty.", "Cette exposition est actuellement vide.");
            instructions = ReplaceIL(instructions, "We look forward to future donations!", "Nous attendons avec impatience les futurs dons!");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(ExhibitSign), "updateMySign")]
        public static IEnumerable<CodeInstruction> ExhibitSign_updateMySign_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "In this exhibit:", "Dans cette exposition :");
            return instructions;
        }

        // todo
        [HarmonyTranspiler, HarmonyPatch(typeof(FadeBlackness), "fadeInDateText", MethodType.Enumerator)]
        public static IEnumerable<CodeInstruction> FadeBlackness_fadeInDateText_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Year ", "Année ");
            return instructions;
        }

        // todo
        [HarmonyTranspiler, HarmonyPatch(typeof(GiftedItemWindow), "giveItemDelay", MethodType.Enumerator)]
        public static IEnumerable<CodeInstruction> GiftedItemWindow_giveItemDelay_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "New Licence!", "Nouvelle Licence!");
            instructions = ReplaceIL(instructions, " Level ", " Niveau ");
            instructions = ReplaceIL(instructions, "On ya!", "Oh ouais!");
            instructions = ReplaceIL(instructions, "You received", "vous avez reçus");
            instructions = ReplaceIL(instructions, "New Crafting Recipe", "Nouvelle recette d'artisanat");
            instructions = ReplaceIL(instructions, "An item was sent to your Mailbox", "Un élément a été envoyé à votre boîte aux lettres");
            instructions = ReplaceIL(instructions, "Your pockets were full!", "Vos poches étaient pleines !");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(GiveNPC), "UpdateMenu", MethodType.Enumerator)]
        public static IEnumerable<CodeInstruction> GiveNPC_UpdateMenu_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Place", "Place");
            instructions = ReplaceIL(instructions, "Donate", "Faire un don");
            instructions = ReplaceIL(instructions, "Sell", "Vendre");
            instructions = ReplaceIL(instructions, "Cancel", "Annuler");
            instructions = ReplaceIL(instructions, "Swap", "Échanger");
            instructions = ReplaceIL(instructions, "Give", "Donner");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(InventoryItemDescription), "fillItemDescription")]
        public static IEnumerable<CodeInstruction> InventoryItemDescription_fillItemDescription_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "All year", "Annuel");
            instructions = ReplaceIL(instructions, "Summer", "Été");
            instructions = ReplaceIL(instructions, "Autum", "Automne");
            instructions = ReplaceIL(instructions, "Winter", "Hiver");
            instructions = ReplaceIL(instructions, "Spring", "Printemps");
            instructions = ReplaceIL(instructions, "Bury", "Enterrer");
            instructions = ReplaceIL(instructions, "Speeds up certain production devices for up to 12 tiles", "Accélère certains appareils de production jusqu'à 12 tuiles");
            instructions = ReplaceIL(instructions, "Reaches ", "Rayon de la plage d'irrigation");
            instructions = ReplaceIL(instructions, " tiles out.\n<color=red>Requires Water Tank</color>", "la grille\n<color=red> à besoin d'un réservoir d'eau</color>");
            instructions = ReplaceIL(instructions, "Provides water to sprinklers ", "Fournit de l'eau aux gicleurs");
            instructions = ReplaceIL(instructions, " tiles out.", " Hors de portée");
            instructions = ReplaceIL(instructions, "Fills animal feeders ", "Remplit les mangeoires pour animaux");
            instructions = ReplaceIL(instructions, " tiles out.\n<color=red>Requires Animal Food</color>", "Hors de portée.\n<color=red> Requière de la nourriture</color>");

            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(InventoryLootTableTimeWeatherMaster), "getTimeOfDayFound")]
        public static IEnumerable<CodeInstruction> InventoryLootTableTimeWeatherMaster_getTimeOfDayFound_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "all day", "toute la journée");
            instructions = ReplaceIL(instructions, "during the day", "durant la journée");
            instructions = ReplaceIL(instructions, "early mornings", "tôt le matin");
            instructions = ReplaceIL(instructions, "around noon", "vers midi");
            instructions = ReplaceIL(instructions, "after dark", "la nuit");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(LicenceButton), "fillButton")]
        public static IEnumerable<CodeInstruction> LicenceButton_fillButton_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Level ", "Niveau");
            instructions = ReplaceIL(instructions, "Level up your ", "Améliorez votre");
            instructions = ReplaceIL(instructions, " skill to unlock further levels", "compétence pour débloquer d'autres niveaux");
            instructions = ReplaceIL(instructions, "Max Level", "Niveau Max");
            instructions = ReplaceIL(instructions, "Not Held", "Non détenu");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(LicenceButton), "fillDetailsForJournal")]
        public static IEnumerable<CodeInstruction> LicenceButton_fillDetailsForJournal_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Level ", "Niveau");
            instructions = ReplaceIL(instructions, "Max Level", "Niveau Max");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(LicenceManager), "checkForUnlocksOnLevelUp")]
        public static IEnumerable<CodeInstruction> LicenceManager_checkForUnlocksOnLevelUp_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "A new Licence is available!", "Une nouvelle licence est disponible !");
            instructions = ReplaceIL(instructions, "A new deed is available!", "Un nouvel acte est disponible !");
            instructions = ReplaceIL(instructions, "Talk to Fletch to apply for deeds.", "Parler a Fletch pour demander des actes");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(LicenceManager), "getLicenceLevelDescription")]
        public static IEnumerable<CodeInstruction> LicenceManager_getLicenceLevelDescription_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Coming soon. The holder will get instant access to Building Level 3 once it has arrived",
                "Bientôt disponible. Le titulaire aura un accès instantané au niveau 3 du bâtiment une fois qu'il sera arrivé");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(LicenceManager), "openConfirmWindow")]
        public static IEnumerable<CodeInstruction> LicenceManager_openConfirmWindow_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Level ", "Niveau");
            instructions = ReplaceIL(instructions, "You hold all ", "tu tiens tout ");
            instructions = ReplaceIL(instructions, " levels", "Niveaux");
            instructions = ReplaceIL(instructions, "Level up your ", "Améliorez votre");
            instructions = ReplaceIL(instructions, " skill to unlock further levels", "compétence pour débloquer d'autres niveaux");
            instructions = ReplaceIL(instructions, "You hold all current ", "Vous détenez déjà tout");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(MailManager), "getSentByName")]
        public static IEnumerable<CodeInstruction> MailManager_getSentByName_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Animal Research Centre", "centre de recherche animale");
            instructions = ReplaceIL(instructions, "Dinkum Dev", "Développeur Dinkum");
            instructions = ReplaceIL(instructions, "Fishin' Tipster", "guide de pêche");
            instructions = ReplaceIL(instructions, "Bug Tipster", "Guide des insectes");
            instructions = ReplaceIL(instructions, "Unknown", "Inconnue");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(MailManager), "showLetter")]
        public static IEnumerable<CodeInstruction> MailManager_showLetter_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "From ", "de ");
            instructions = ReplaceIL(instructions, "<size=18><b>To ", "<size=18><b>à ");
            instructions = ReplaceIL(instructions, "\n\n<size=18><b>From ", "\n\n<size=18><b>De ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NetworkMapSharer), "UserCode_RpcAddToMuseum")]
        public static IEnumerable<CodeInstruction> NetworkMapSharer_UserCode_RpcAddToMuseum_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Donated by ", "Donné par：");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NetworkMapSharer), "UserCode_RpcPayTownDebt")]
        public static IEnumerable<CodeInstruction> NetworkMapSharer_UserCode_RpcPayTownDebt_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " donated <sprite=11>", "fait un don <sprite=11>");
            instructions = ReplaceIL(instructions, " towards town debt", " rembourser la dette");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NetworkNavMesh), "onSleepingAmountChange")]
        public static IEnumerable<CodeInstruction> NetworkNavMesh_onSleepingAmountChange_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "<b><color=purple> Sleeping </color></b>", "<b><color=purple> En train de dormir... </color></b>");
            instructions = ReplaceIL(instructions, "<b><color=purple> Ready to Sleep </color></b> [", "<b><color=purple> Prêt a dormir </color></b> [");
            return instructions;
        }

        // todo
        [HarmonyTranspiler, HarmonyPatch(typeof(NetworkNavMesh), "waitForNameToChange", MethodType.Enumerator)]
        public static IEnumerable<CodeInstruction> NetworkNavMesh_waitForNameToChange_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " has joined", "  à rejoint");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NPCRequest), "acceptRequest")]
        public static IEnumerable<CodeInstruction> NPCRequest_acceptRequest_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Request added to Journal", "Demande ajoutée au journal");
            instructions = ReplaceIL(instructions, "This request must be completed by the end of the day.", "Cette demande doit être complétée avant la fin de la journée.");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NPCRequest), "getDesiredItemName")]
        public static IEnumerable<CodeInstruction> NPCRequest_getDesiredItemName_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "any bug", "n'importe quel insecte");
            instructions = ReplaceIL(instructions, "any fish", "n'importe quel poisson");
            instructions = ReplaceIL(instructions, "something to eat", "de la nourriture");
            instructions = ReplaceIL(instructions, "something you've made me at a cooking table", "une partie de la nourriture que vous cuisinez sur la station de cuisson");
            instructions = ReplaceIL(instructions, "furniture", "meubles");
            instructions = ReplaceIL(instructions, "clothing", "Vêtements");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NPCRequest), "getMissionText")]
        public static IEnumerable<CodeInstruction> NPCRequest_getMissionText_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "<sprite=12> Bring ", "<sprite=12> apporter ");
            instructions = ReplaceIL(instructions, "<sprite=13> Collect ", "<sprite=13> recueillir ");
            instructions = ReplaceIL(instructions, "\n<sprite=12> Bring ", "\n<sprite=12> apporter ");
            instructions = ReplaceIL(instructions, "<sprite=12> Collect ", "<sprite=12> recueillir ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NPCSchedual), "getDaysClosed")]
        public static IEnumerable<CodeInstruction> NPCSchedual_getDaysClosed_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Closed: ", "Heure de fermeture: ");
            instructions = ReplaceIL(instructions, " and ", " et ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NPCSchedual), "getNextDayOffName")]
        public static IEnumerable<CodeInstruction> NPCSchedual_getNextDayOffName_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "No Day off", "Pas de jours de congés");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(NPCSchedual), "getOpeningHours")]
        public static IEnumerable<CodeInstruction> NPCSchedual_getOpeningHours_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Open: ", "Horaires d'ouvertures: ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PlayerDetailManager), "switchToLevelWindow")]
        public static IEnumerable<CodeInstruction> PlayerDetailManager_switchToLevelWindow_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Resident for: ", "Résidence de:");
            instructions = ReplaceIL(instructions, " days", " jours");
            instructions = ReplaceIL(instructions, " months", " mois");
            instructions = ReplaceIL(instructions, " years", " années");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PocketsFullNotification), "showMustBeEmpty")]
        public static IEnumerable<CodeInstruction> PocketsFullNotification_showMustBeEmpty_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Must be empty", "Doit être vide");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PocketsFullNotification), "showNoLicence")]
        public static IEnumerable<CodeInstruction> PocketsFullNotification_showNoLicence_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Need Licence", "Licence requise");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PocketsFullNotification), "showPocketsFull")]
        public static IEnumerable<CodeInstruction> PocketsFullNotification_showPocketsFull_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Pockets Full", "le sac à dos est plein");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PocketsFullNotification), "showTooFull")]
        public static IEnumerable<CodeInstruction> PocketsFullNotification_showTooFull_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Too full", "Trop remplie");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PostOnBoard), "acceptTask")]
        public static IEnumerable<CodeInstruction> PostOnBoard_acceptTask_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Request added to Journal by ", "La demande a été ajoutée au journal par ");
            instructions = ReplaceIL(instructions, "Request added to Journal", "Demande ajoutée au journal");
            instructions = ReplaceIL(instructions, "A location was added to your map.", "Lieu ajouté à la carte.");
            instructions = ReplaceIL(instructions, "This request has a time limit.", "Cette demande est limitée dans le temps.");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PostOnBoard), "completeTask")]
        public static IEnumerable<CodeInstruction> PostOnBoard_completeTask_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Request Completed by ", "demande complétée par ");
            instructions = ReplaceIL(instructions, "Investigation Request Complete!", "Demande d'enquête terminée !");
            instructions = ReplaceIL(instructions, "Request Complete!", "Demande effectuée !");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(PostOnBoard), "getPostedByName")]
        public static IEnumerable<CodeInstruction> PostOnBoard_getPostedByName_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Town Announcement", "Annonce de la ville");
            instructions = ReplaceIL(instructions, "Animal Research Centre", "Centre de recherche animale");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(Quest), "getMissionObjText")]
        public static IEnumerable<CodeInstruction> Quest_getMissionObjText_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "<sprite=12> Attract a total of 5 permanent residents to move to ", "<sprite=12> Attire un total de 5 résidents permanents à déménager");
            instructions = ReplaceIL(instructions, "<sprite=12> Talk to ", "\n<sprite=12> Parler à ");
            instructions = ReplaceIL(instructions, " once the Base Tent has been moved", " après avoir déplacé la tente de base");
            instructions = ReplaceIL(instructions, "<sprite=13> Craft a ", "<sprite=13> Fabriquer ");
            instructions = ReplaceIL(instructions, "at the crafting table in the Base Tent.\n<sprite=13> Place the  ", "Sur la table d'artisanat dans la tente de base.\n<sprite=13> placez ");
            instructions = ReplaceIL(instructions, " down outside.\n<sprite=13> Place Tin Ore into ", " à l'extérieur.\n<sprite=13> Placez le minerai d'étain dans ");
            instructions = ReplaceIL(instructions, " and wait for it to become ", "et attendez qu'il fonde");
            instructions = ReplaceIL(instructions, ".\n<sprite=12> Take the ", ".\n<sprite=12> tenir");
            instructions = ReplaceIL(instructions, " to ", " à ");
            instructions = ReplaceIL(instructions, " down outside.\n<sprite=12> Place Tin Ore into ", "à l'extérieur.\n<sprite=12> Placez le minerai d'étain dans ");
            instructions = ReplaceIL(instructions, "at the crafting table in the Base Tent.\n<sprite=12> Place the  ", "à la table d'artisanat dans la tente de base.\n<sprite=12> Placez");
            instructions = ReplaceIL(instructions, "<sprite=12> Craft a ", "<sprite=12> Fabriquer ");
            instructions = ReplaceIL(instructions, "<sprite=13> Buy the ", "<sprite=13> Acheter ");
            instructions = ReplaceIL(instructions, "\n<sprite=12> Talk to ", "\n<sprite=12> Parler à ");
            instructions = ReplaceIL(instructions, "<sprite=12> Buy the ", "<sprite=12> Acheter ");
            instructions = ReplaceIL(instructions, "[Optional] Complete Daily tasks\n<sprite=12> Place sleeping bag and get some rest.", "[Facultatif] Terminez les tâches quotidiennes\n<sprite=12> Placez le sac de couchage et reposez-vous.");
            instructions = ReplaceIL(instructions, "<sprite=13> Find something to eat.\n<sprite=12> Talk to ", "<sprite=13> Trouvez quelque chose à manger.\n<sprite=12> Parlez à");
            instructions = ReplaceIL(instructions, "<sprite=12> Find something to eat.\n<sprite=12> Talk to ", "<sprite=12> Trouvez quelque chose à manger.\n<sprite=12> Parlez à");
            instructions = ReplaceIL(instructions, "<sprite=13> Collect the requested items.\n<sprite=12> Bring items to ", "<sprite=13> Récupérez les éléments demandés.\n<sprite=12> Apportez les éléments à");
            instructions = ReplaceIL(instructions, "<sprite=12> Collect the requested items.", "<sprite=12> Récupérez les éléments demandés.");
            instructions = ReplaceIL(instructions, "\n<sprite=12> Bring items to ", "\n<sprite=12> Apportez des objets à");
            instructions = ReplaceIL(instructions, "<sprite=12> Do some favours for John", "<sprite=12> Faites des faveurs à John");
            instructions = ReplaceIL(instructions, "<sprite=13> Do some favours for John", "<sprite=13> Faites des faveurs à John");
            instructions = ReplaceIL(instructions, "\n<sprite=12> Spend money or sell items in John's store", "\n<sprite=12> Dépenser de l'argent ou vendre des articles dans le magasin de John");
            instructions = ReplaceIL(instructions, "\n<sprite=13> Spend money or sell items in John's store", "\n<sprite=13> Dépenser de l'argent ou vendre des articles dans le magasin de John");
            instructions = ReplaceIL(instructions, "\n<sprite=12> Convince John to move in.", "\n<sprite=12> Convaincre John d'emménager.");
            instructions = ReplaceIL(instructions, "<sprite=12> Ask ", "<sprite=12> Interroger");
            instructions = ReplaceIL(instructions, " about the town to apply for the ", " sur la ville pour postuler ");
            instructions = ReplaceIL(instructions, "<sprite=12> Place the ", "<sprite=12> Placer ");
            instructions = ReplaceIL(instructions, "<sprite=12> Wait for construction of the ", "<sprite=12> Attendre la construction du ");
            instructions = ReplaceIL(instructions, " to be completed", " à compléter");
            instructions = ReplaceIL(instructions, "<sprite=12> Place the required items into the construction box at the deed site", "<sprite=12> Placez les éléments requis dans la boîte de construction sur le site de l'acte");
            instructions = ReplaceIL(instructions, "<sprite=12> Place ", "<sprite=12> Place");
            instructions = ReplaceIL(instructions, "<sprite=13> Place ", "<sprite=13> Place");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestManager), "completeQuest")]
        public static IEnumerable<CodeInstruction> QuestManager_completeQuest_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "A new deed is available!", "Un nouvel acte est disponible !");
            instructions = ReplaceIL(instructions, "Talk to Fletch to apply for deeds.", "Parlez à Fletch pour demander des actes.");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "displayQuest")]
        public static IEnumerable<CodeInstruction> QuestTracker_displayQuest_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " days remaining", " Jours restants");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "displayRequest")]
        public static IEnumerable<CodeInstruction> QuestTracker_displayRequest_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Request for ", "Demande pour ");
            instructions = ReplaceIL(instructions, " has asked you to get ", " veux te demander d'obtenir ");
            instructions = ReplaceIL(instructions, "By the end of the day", "A la fin de la journée");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "displayTrackingRecipe")]
        public static IEnumerable<CodeInstruction> QuestTracker_displayTrackingRecipe_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " Recipe", " Recette");
            instructions = ReplaceIL(instructions, "These items are required to craft ", "Ces objets sont nécessaires pour fabriquer ");
            instructions = ReplaceIL(instructions, "\n Unpin this to stop tracking recipe.", "\n Désépinglez ceci pour arrêter le suivi de la recette.");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "fillMissionTextForRecipe")]
        public static IEnumerable<CodeInstruction> QuestTracker_fillMissionTextForRecipe_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Crafting ", "Fabriquer ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "pressPinRecipeButton")]
        public static IEnumerable<CodeInstruction> QuestTracker_pressPinRecipeButton_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " Recipe", " Recette");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "updateLookingAtTask")]
        public static IEnumerable<CodeInstruction> QuestTracker_updateLookingAtTask_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "<sprite=17> Pinned", "<sprite=17> Épinglé");
            instructions = ReplaceIL(instructions, "<sprite=16> Pinned", "<sprite=16> Épinglé");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "updatePinnedRecipeButton")]
        public static IEnumerable<CodeInstruction> QuestTracker_updatePinnedRecipeButton_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "<sprite=13> Track Recipe Ingredients", "<sprite=13> Suivre les ingrédients de la recette");
            instructions = ReplaceIL(instructions, "<sprite=12> Track Recipe Ingredients", "<sprite=12> Suivre les ingrédients de la recette");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(QuestTracker), "updatePinnedTask")]
        public static IEnumerable<CodeInstruction> QuestTracker_updatePinnedTask_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Request for ", "Demande pour");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(RealWorldTimeLight), "showTimeOnClock")]
        public static IEnumerable<CodeInstruction> RealWorldTimeLight_showTimeOnClock_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "<size=10>PM</size>", "<size=10>PM</size>");
            instructions = ReplaceIL(instructions, "<size=10>AM</size>", "<size=10>AM</size>");
            instructions = ReplaceIL(instructions, "Late", "En retard");
            return instructions;
        }

        /// <summary>
        /// 在IL中替换文本
        /// </summary>
        public static IEnumerable<CodeInstruction> ReplaceIL(IEnumerable<CodeInstruction> instructions, string target, string i18n)
        {
            bool success = false;
            var list = instructions.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var ci = list[i];
                if (ci.opcode == OpCodes.Ldstr)
                {
                    if ((string)ci.operand == target)
                    {
                        ci.operand = i18n;
                        success = true;
                    }
                }
            }
            if (!success)
            {
                Debug.LogWarning($"汉化插件欲将{target}替换成{i18n}失败，没有找到目标");
            }
            return list.AsEnumerable();
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(SaveSlotButton), "fillFromSaveSlot")]
        public static IEnumerable<CodeInstruction> SaveSlotButton_fillFromSaveSlot_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Year ", "Année ");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(SeasonAndTime), "getLocationName")]
        public static IEnumerable<CodeInstruction> SeasonAndTime_getLocationName_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Everywhere", "Partout");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(Task), MethodType.Constructor, new Type[] { typeof(int), typeof(int) })]
        public static IEnumerable<CodeInstruction> Task_Constructor_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Harvest ", "Récolter ");
            instructions = ReplaceIL(instructions, "Catch ", "Attraper ");
            instructions = ReplaceIL(instructions, " Bugs", " insectes");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(Task), "generateTask")]
        public static IEnumerable<CodeInstruction> Task_generateTask_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " ", "");
            instructions = ReplaceIL(instructions, "Harvest ", "Récolter");
            instructions = ReplaceIL(instructions, "Chat with ", "Parler avec");
            instructions = ReplaceIL(instructions, " residents", " résidents");
            instructions = ReplaceIL(instructions, "Bury ", "Enterrer ");
            instructions = ReplaceIL(instructions, " Fruit", " Fruit");
            instructions = ReplaceIL(instructions, "Collect ", "Recueillir ");
            instructions = ReplaceIL(instructions, " Shells", "Coquilles");
            instructions = ReplaceIL(instructions, "Sell ", "Vendre");
            instructions = ReplaceIL(instructions, "Do a job for someone", "Faire un travail pour quelqu'un");
            instructions = ReplaceIL(instructions, "Plant ", "Planté ");
            instructions = ReplaceIL(instructions, " Wild Seeds", " Graines sauvages");
            instructions = ReplaceIL(instructions, "Dig up dirt ", "Déterrer la terre");
            instructions = ReplaceIL(instructions, " times", " fois");
            instructions = ReplaceIL(instructions, "Catch ", "attraper ");
            instructions = ReplaceIL(instructions, " Bugs", " Insectes");
            instructions = ReplaceIL(instructions, "Craft ", "Fabriquer ");
            instructions = ReplaceIL(instructions, " Items", " Articles");
            instructions = ReplaceIL(instructions, "Eat something", "Manger quelque chose");
            instructions = ReplaceIL(instructions, "Make ", "Faire ");
            instructions = ReplaceIL(instructions, "Spend ", "Dépenser ");
            instructions = ReplaceIL(instructions, "Travel ", "Voyager ");
            instructions = ReplaceIL(instructions, "m on foot.", "mètres (à pied)");
            instructions = ReplaceIL(instructions, "m by vehicle", "mètres (en véhicule)");
            instructions = ReplaceIL(instructions, "Cook ", "Cuisiner");
            instructions = ReplaceIL(instructions, " meat", " Viande");
            instructions = ReplaceIL(instructions, " fruit", " fruit");
            instructions = ReplaceIL(instructions, "Cook something at the Cooking table", "Cuisinez quelque chose à la table de cuisson");
            instructions = ReplaceIL(instructions, " tree seeds", " graines d'arbres");
            instructions = ReplaceIL(instructions, "crop seeds", " graines dans vos champs");
            instructions = ReplaceIL(instructions, "Water ", "Eau ");
            instructions = ReplaceIL(instructions, " crops", " récoltes");
            instructions = ReplaceIL(instructions, "Smash ", "Fracasser ");
            instructions = ReplaceIL(instructions, " rocks", " Rochers");
            instructions = ReplaceIL(instructions, " ore rocks", " roches de minerai");
            instructions = ReplaceIL(instructions, "Smelt some ore into a bar", "Faire fondre du minerai");
            instructions = ReplaceIL(instructions, "Grind ", "Moudre");
            instructions = ReplaceIL(instructions, " stones", "des pierres");
            instructions = ReplaceIL(instructions, "Cut down ", "Couper ");
            instructions = ReplaceIL(instructions, " trees", " des arbres");
            instructions = ReplaceIL(instructions, "Clear ", "Dégager ");
            instructions = ReplaceIL(instructions, " tree stumps", " souches d'arbres");
            instructions = ReplaceIL(instructions, "Saw ", "Vu ");
            instructions = ReplaceIL(instructions, " planks", "planches");
            instructions = ReplaceIL(instructions, " Fish", " Poisson");
            instructions = ReplaceIL(instructions, " grass", " herbe");
            instructions = ReplaceIL(instructions, "Pet an animal", "Caresser un animal");
            instructions = ReplaceIL(instructions, "Buy some new clothes", "Achetez de nouveaux vêtements");
            instructions = ReplaceIL(instructions, "Buy some new furniture", "Achetez de nouveaux meubles");
            instructions = ReplaceIL(instructions, "Buy some new wallpaper", "Achetez du nouveau papier peint");
            instructions = ReplaceIL(instructions, "Buy some new flooring", "Achetez un nouveau revêtement de sol");
            instructions = ReplaceIL(instructions, "Compost something", "Compostez quelque chose");
            instructions = ReplaceIL(instructions, "Craft a new tool", "Fabriquer un nouvel outil");
            instructions = ReplaceIL(instructions, "Buy ", "Acheter ");
            instructions = ReplaceIL(instructions, " seeds", " des graines");
            instructions = ReplaceIL(instructions, "Trap an animal and deliver it", "Piéger un animal et le livrer");
            instructions = ReplaceIL(instructions, "Hunt ", "Chasser ");
            instructions = ReplaceIL(instructions, " animals", " animaux");
            instructions = ReplaceIL(instructions, "Buy a new tool", "Acheter un nouvel outil");
            instructions = ReplaceIL(instructions, "Break a tool", "Casser un outil");
            instructions = ReplaceIL(instructions, "Find some burried treasure", "Trouver un trésor enfoui");
            instructions = ReplaceIL(instructions, "No mission set", "Pas de mission");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(TileObjectSettings), "getWhyCantPlaceDeedText")]
        public static IEnumerable<CodeInstruction> TileObjectSettings_getWhyCantPlaceDeedText_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Can't place here", "Impossible de placer ici");
            instructions = ReplaceIL(instructions, "Someone is in the way", "Quelqu'un est sur le chemin");
            instructions = ReplaceIL(instructions, "Not on level ground", "Placer sur un terrain plat");
            instructions = ReplaceIL(instructions, "Can't be placed in water", "Ne peut pas être placé dans l'eau");
            instructions = ReplaceIL(instructions, "Something in the way", "Quelque chose sur le chemin");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(UseBook), "plantBookRoutine", MethodType.Enumerator)]
        public static IEnumerable<CodeInstruction> UseBook_plantBookRoutine_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, " Plant", " Plante");
            instructions = ReplaceIL(instructions, "Ready for harvest", "Prêt pour la récolte");
            instructions = ReplaceIL(instructions, "Mature in:\n", "Mûr dans:\n");
            instructions = ReplaceIL(instructions, " days.", "jours");
            instructions = ReplaceIL(instructions, " days", " jours");
            instructions = ReplaceIL(instructions, "Plant", "Plante");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(WeatherManager), "currentWeather")]
        public static IEnumerable<CodeInstruction> WeatherManager_currentWeather_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "It is currently ", "il fait actuellement ");
            instructions = ReplaceIL(instructions, "° and ", "° avec ");
            instructions = ReplaceIL(instructions, "Storming", "une tempête");
            instructions = ReplaceIL(instructions, "Raining", "de la pluie");
            instructions = ReplaceIL(instructions, "Foggy", "du brouillard");
            instructions = ReplaceIL(instructions, "Fine", "un beau temps");
            instructions = ReplaceIL(instructions, ". With a", ". Avec un vent");
            instructions = ReplaceIL(instructions, " Strong", " fort");
            instructions = ReplaceIL(instructions, " Light", " faible");
            instructions = ReplaceIL(instructions, " Northern ", " vers le Nord");
            instructions = ReplaceIL(instructions, " Southern ", " vers le Sud");
            instructions = ReplaceIL(instructions, " Westernly ", " vers l'Ouest");
            instructions = ReplaceIL(instructions, " Easternly ", " vers l'Est");
            instructions = ReplaceIL(instructions, " Wind.", ".");
            return instructions;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(WeatherManager), "tomorrowsWeather")]
        public static IEnumerable<CodeInstruction> WeatherManager_tomorrowsWeather_Patch(IEnumerable<CodeInstruction> instructions)
        {
            instructions = ReplaceIL(instructions, "Tomorrow expect ", "la météo de demain:");
            instructions = ReplaceIL(instructions, "Storms", "tempête");
            instructions = ReplaceIL(instructions, "Rain", "pluie");
            instructions = ReplaceIL(instructions, "Fog", "brouillard");
            instructions = ReplaceIL(instructions, "Fine Weather", "beau temps");
            instructions = ReplaceIL(instructions, ". With", ". avec");
            instructions = ReplaceIL(instructions, " Strong", "fort");
            instructions = ReplaceIL(instructions, " Light", "faible");
            instructions = ReplaceIL(instructions, " Northern ", "Nord");
            instructions = ReplaceIL(instructions, " Southern ", "Sud");
            instructions = ReplaceIL(instructions, " Westernly ", "Oeust");
            instructions = ReplaceIL(instructions, " Easternly ", "Est");
            instructions = ReplaceIL(instructions, "Wind. With temperatures around ", "vent. température proche de ");
            instructions = ReplaceIL(instructions, "°.", "°.");
            return instructions;
        }

        // instructions = ReplaceIL(instructions, "", "");
    }
}