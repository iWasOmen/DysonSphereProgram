using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xiaoye97;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Reflection;


namespace SulfuricAcidElectrolysis
{
    [BepInDependency("me.xiaoye97.plugin.Dyson.LDBTool",BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("me.iwasomen.plugin.DysonMod", "SulfuricAcidElectrolyze", "1.0")]
    public class SulfuricAcidElectrolysis : BaseUnityPlugin
    {
        private Sprite icon;
        void Start()
        {
            var ab = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("SulfuricAcidElectrolysis.refinedoilnew"));
            icon = ab.LoadAsset<Sprite>("refinedOilNew");
            LDBTool.PreAddDataAction += addVitriolElectrolyze;
            LDBTool.PreAddDataAction += addLang;

        }


        void addVitriolElectrolyze()
        {
            var oriVitriolRec = LDB.recipes.Select(24);
            var newVitriolRec = oriVitriolRec.Copy();

            newVitriolRec.ID = 284;
            newVitriolRec.Name = "精炼油（高效）";
            newVitriolRec.name = "精炼油（高效）".Translate();
            newVitriolRec.Description = "找到硫酸海，制造精炼油就会变得容易许多。";
            newVitriolRec.description = "找到硫酸海，制造精炼油就会变得容易许多。".Translate();
            newVitriolRec.Items = new int[] { 1116 };
            newVitriolRec.Results = new int[] { 1114, 1000 };
            newVitriolRec.ItemCounts = new int[] { 4 };
            newVitriolRec.ResultCounts = new int[] { 6, 4 };
            newVitriolRec.preTech = LDB.techs.Select(1121);
            newVitriolRec.Explicit = true;
            newVitriolRec.GridIndex = 1610;
            newVitriolRec.SID = "1610";
            newVitriolRec.sid = "1610".Translate();
            Traverse.Create(newVitriolRec).Field("_iconSprite").SetValue(icon);

            var RefinedOil = LDB.items.Select(1114);
            RefinedOil.recipes.Add(newVitriolRec);



            LDBTool.PostAddProto(ProtoType.Recipe, newVitriolRec);

        }
        void addLang()
        {
            StringProto name = new StringProto();
            StringProto desc = new StringProto();
            name.ID = 23019;
            name.Name = "精炼油（高效）";
            name.name = "精炼油（高效）";
            name.ZHCN = "精炼油（高效）";
            name.ENUS = "refined oil(efficient)";
            name.FRFR = "Huile raffinée(haute efficacité)";

            desc.ID = 23020;
            desc.Name = "找到硫酸海，制造精炼油就会变得容易许多。";
            desc.name = "找到硫酸海，制造精炼油就会变得容易许多。";
            desc.ZHCN = "找到硫酸海，制造精炼油就会变得容易许多。";
            desc.ENUS = "It will be a lot easier to produce refined oil if you can find a sea of sulfuric acid";
            desc.FRFR = "Trouver une mer d'acide sulfurique facilite la fabrication d'huile raffinée.";

            LDBTool.PreAddProto(ProtoType.String, name);
            LDBTool.PreAddProto(ProtoType.String, desc);


        }

    }
}
