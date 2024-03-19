using System;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;
using UnityEditor;
using UnityEngine;

namespace _Project.CodeBase.Services.Saving.Tests
{
    public class JsonSaverTest
    {

        [NUnit.Framework.Test]
        public void ClassSavingTest()
        {
            SavingConfig savingConfig = ScriptableObject.CreateInstance<SavingConfig>();
            savingConfig.savePath = "Test";
            
            ISaver saver = new JsonSaver(savingConfig);
            ILoader loader = new JsonSaver(savingConfig);
            
            TestClass testClass = new TestClass();
            testClass.IntField = 10;
            testClass.StringField = "Test";
            
            saver.Save(testClass);
            loader.Load<TestClass>(out var loadedTestClass);
            
            NUnit.Framework.Assert.AreEqual(testClass.IntField, loadedTestClass.IntField);
            NUnit.Framework.Assert.AreEqual(testClass.StringField, loadedTestClass.StringField);
            
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(savingConfig));
        }

        [Serializable]
        [Savable(nameof(TestClass))]
        private class TestClass
        {
            public int IntField;
            public string StringField;
        }
    }
}