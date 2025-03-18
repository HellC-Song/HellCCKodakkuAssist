using System;
using KodakkuAssist.Module.GameEvent;
using KodakkuAssist.Script;
using KodakkuAssist.Module.GameEvent.Struct;
using KodakkuAssist.Module.Draw;
using System.Windows.Forms;

namespace HellCCKodakkuAssist.TheOmegaProtocolUltimate
{
    [ScriptType(name: "HellCC's TOP script(HellCC的绝欧脚本)",
        territorys: [1122],
        guid: "69aab792-24ac-1841-79b1-5e3ac0b3e6ef",
        version: "0.0.0.2",
        note: "Work in Progress. 施工中",
        author: "HellCC")
    ]
    public class The_Omega_Protocol
    {
        /// <summary>
        /// note will be displayed to the user as a tooltip.
        /// </summary>
        
        [UserSetting("启用文本提示")]
        public bool Enable_Text_Prompts { get; set; } = true;
    
        [UserSetting("文本提示语言")]
        public Languages_Of_Text_Prompts Language_Of_Text_Prompts { get; set; } =  Languages_Of_Text_Prompts.Simplified_Chinese_简体中文;
    
        [UserSetting("启用开发者模式")]
        public bool Enable_Developer_Mode { get; set; } = false;

        [UserSetting("绘图颜色")]
        public ScriptColor color { get; set; } = new();
    
        public enum Languages_Of_Text_Prompts {
        
        Simplified_Chinese_简体中文,
        English_英文
        
        }

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
