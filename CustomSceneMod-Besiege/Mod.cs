using UnityEngine;
using Modding;
using System.Collections.Generic;
using System;

namespace CustomScene
{


    public class Mod : ModEntryPoint
    {
        
        private GameObject ModObject;

        public static EnvironmentMod environmentMod;
        public static BlockInformationMod blockInformationMod;
        public static TimerMod timerMod;
        public static Prop prop;

        public override void OnLoad()
        {
            string DisplayName = "Besiege Custom Scene";

            string Version = "2.0.0";

            ModObject = new GameObject
            {
                name = string.Format("{0} {1}", DisplayName, Version)
            };

            //prop = ModObject.AddComponent<Prop>();

            ModObject.AddComponent<test>();

            //GameObject customScene = new GameObject("CustomScene");
            //environmentMod = customScene.AddComponent<EnvironmentMod>();
            //customScene.AddComponent<UI.EnvironmentSettingUI>();
            //customScene.transform.SetParent(ModObject.transform);

            //GameObject toolbox = new GameObject("ToolBox");
            //blockInformationMod = toolbox.AddComponent<BlockInformationMod>();
            //timerMod = toolbox.AddComponent<TimerMod>();
            //toolbox.AddComponent<UI.ToolBoxSettingUI>();
            //toolbox.transform.SetParent(ModObject.transform);

            //GameObject miniMap = new GameObject("Mini Map");
            //miniMap.AddComponent<UI.MiniMapSettingUI>();
            //miniMap.transform.SetParent(Mod.transform);

            UnityEngine.Object.DontDestroyOnLoad(ModObject);

        }
    }



   public class test :MonoBehaviour
    {


        testClass ts, ts1;

        SceneSettingFile sceneSettingFile;

        public class testClass: Modding.Serialization.Element
        {
       
            [Modding.Serialization.RequireToValidate]
            public string SceneName { get; set; }
            [Modding.Serialization.CanBeEmpty]
            public string Author { get; set; }
            [Modding.Serialization.CanBeEmpty]
            public testClass2 testClass2 { get; set; }


            public override string ToString()
            {
                string str = "";
                if (testClass2 != null)
                    str = string.Format("{0}-{1}\n{2}", SceneName, Author, testClass2.ToString());
                else
                    str = string.Format("{0}-{1}\n{2}", SceneName, Author, "null");

                return str;
            }
        }
     
        public class testClass2
        {
            public string matType;
            public string matName;
            //public Material material;

            public static Dictionary<string, Type> dic = new Dictionary<string, Type>
           {
               { "contents",typeof(string) },
               { "shader",typeof(Shader)},
                { "material",typeof(Material)}
           };

            public override string ToString()
            {
                string str = "";

                str = string.Format("{0}+{1}", matType, matName);

                return str;
            }

        }

        void Start()
        {
            ts = new testClass();
            ts.SceneName = "test map";
            ts.Author = "ltm";
            //ts.testClass2 = new testClass2();
            //ts.testClass2.matName = "mat name";
            //ts.testClass2.matType = "shader";
            //ts.testClass2.material = new Material(Shader.Find("diffuse"));

            sceneSettingFile = new SceneSettingFile() { AuthorName = "ltm", SceneDescription = "Ver.0.0.1", Texture = new Serializable.Shader() { name="test-shader", propertise = new Serializable.Shader.ShaderPropertise[] { new Serializable.Shader.ShaderPropertise() { Name ="mainTexture", DataType ="Texture", Value = "test-tex"}, new Serializable.Shader.ShaderPropertise() { Name = "color", DataType = "Color", Value = "1,1,1,1" } } } };
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("SerializeXml");

                ModIO.SerializeXml(sceneSettingFile, "setting.xml", true);

            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("DeserializeXml");
                sceneSettingFile = ModIO.DeserializeXml<SceneSettingFile>("setting.xml", true,false);
                
                Debug.Log(sceneSettingFile.ToString());
            }
        }

    }
}