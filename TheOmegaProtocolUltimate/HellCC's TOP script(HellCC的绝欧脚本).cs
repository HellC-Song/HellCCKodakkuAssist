using System;
using KodakkuAssist.Module.GameEvent;
using KodakkuAssist.Script;
using KodakkuAssist.Module.GameEvent.Struct;
using KodakkuAssist.Module.Draw;
using System.Windows.Forms;

namespace MyScriptNamespace
{
    [ScriptType(name: "HellCC's TOP script(HellCC的绝欧脚本)",
        territorys: [1122],
        guid: "69aab792-24ac-1841-79b1-5e3ac0b3e6ef",
        version: "0.0.0.1",
        note: notesOfTheScript,
        author: "HellCC")]
    public class SimpleScript
    {
        /// <summary>
        /// note will be displayed to the user as a tooltip.
        /// </summary>

        const string notesOfTheScript =
        """
        ***** Please read the note here carefully before running the script! *****
        ***** 请在使用此脚本前仔细阅读此处的说明! *****
        Work In progress. 施工中。
        """;
        
        [UserSetting(note:"This is a test Property")]
        public int prop1 { get; set; } = 1;
        [UserSetting("Another Test Property")]
        public bool prop2 { get; set; } = false;

        [UserSetting("UserColorSetting")]
        public ScriptColor color { get; set; } = new();

        [UserSetting("EnumSetting")]
        public TestEnum enumSetting { get; set; }
        int n = 0;


        public enum TestEnum
        {
            First,
            Second
        }
        /// <summary>
        /// This method is called at the start of each battle reset.
        /// If this method is not defined, the program will execute an empty method.
        /// </summary>
        public void Init(ScriptAccessory accessory)
        {
            n = 0;
        }

        /// <summary>
        /// name is the name of the method as presented to the user.
        /// eventType is the type of event that triggers this method.
        /// eventCondition is an array of strings specifying the properties that the event must have,
        /// in the format name:value,For specific details, please refer to the GameEvent of the plugin.
        /// userControl set to false will make the method not be shown to the user
        /// and cannot be disabled by the user.
        /// Please note, the method will be executed asynchronously.
        /// </summary>
        /// <param name="event">The event instance that triggers this method.</param>
        /// <param name="accessory">Pass the instances of methods and data that might be needed.</param>
        [ScriptMethod(name: "Test StartCasting",eventType: EventTypeEnum.StartCasting,eventCondition: ["ActionId:133"])]
        public void PrintInfo(Event @event, ScriptAccessory accessory)
        {
            n++;
            accessory.Method.SendChat($"{@event["SourceId"]} {n}-th use the Medica II");
            accessory.Log.Debug($"Prop2 is {prop2}");
            accessory.Log.Debug($"enum is {enumSetting}");
        }

        [ScriptMethod(name: "Test Draw", eventType: EventTypeEnum.ActionEffect,eventCondition: ["ActionId:124"])]
        public void DrawCircle(Event @event, ScriptAccessory accessory)
        {
            var prop = accessory.Data.GetDefaultDrawProperties();
            prop.Owner = Convert.ToUInt32(@event["SourceId"],16);
            prop.DestoryAt = 2000;
            prop.Color=color.V4;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, prop);
        }

        [ScriptMethod(name: "Unconfigurable Method", eventType: EventTypeEnum.ActionEffect,eventCondition: ["ActionId:124"],userControl:false)]
        public void UnconfigurableMethod(Event @event, ScriptAccessory accessory)
        {
            accessory.Log.Debug($"The unconfigurable method has been triggered.");
        }


    }
}
