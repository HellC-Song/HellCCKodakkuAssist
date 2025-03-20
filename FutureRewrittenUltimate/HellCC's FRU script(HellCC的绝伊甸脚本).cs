using System;
using KodakkuAssist.Module.GameEvent;
using KodakkuAssist.Script;
using KodakkuAssist.Module.GameEvent.Struct;
using KodakkuAssist.Module.Draw;
using HellCCKodakkuAssist.FutureRewrittenUltimate;
using System.Windows.Forms;

namespace HellCCKodakkuAssist.FutureRewrittenUltimate
{
    [ScriptType(name: "HellCC's FRU script(HellCC的绝伊甸脚本)",
        territorys: [1238],
        guid: "e8f65bb5-5c4c-405c-8735-c1b637eb852c",
        version: "0.0.0.1",
        note: "Work in Progress. 施工中",
        author: "HellCC")
    ]
    public class Future_Rewritten_Ultimate
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
        
        private double parse = 0;
        private List<int> P1转轮召抓人 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
        public void Init(ScriptAccessory accessory)
        {
            parse = 1d;
            P1转轮召抓人 = [0, 0, 0, 0, 0, 0, 0, 0];
        }
        [ScriptMethod(name: "P1_转轮召_抓人", eventType: EventTypeEnum.StatusAdd, eventCondition: ["StatusID:4165"], userControl: false)]
        public void P1_转轮召_抓人(Event @event, ScriptAccessory accessory)
        {
            if (parse != 1d) return;
            if (!ParseObjectId(@event["TargetId"], out var tid)) return;
            lock (this)
            {
                P1转轮召抓人[accessory.Data.PartyList.IndexOf(((uint)tid))] = 1;
            }
        }

        [ScriptMethod(name: "Phase1 Stack Range Of Turn Of The Heavens 光轮召唤分摊标记",
            eventType: EventTypeEnum.StartCasting,
            eventCondition: ["ActionId:regex:^(40152)$"])]
        public void Phase1_Stack_Range_Of_Turn_Of_The_Heavens_光轮召唤分摊标记(Event @event, ScriptAccessory accessory)
        {
            int highPriorityTarget = P1转轮召抓人.IndexOf(1);
            int lowPriorityTarget = P1转轮召抓人.LastIndexOf(1);
            accessory.Method.MarkClear();
            if(highPriorityTarget > 3){
                accessory.Method.Mark(accessory.Data.PartyList[0], MarkType.Stop1);
                accessory.Method.Mark(accessory.Data.PartyList[highPriorityTarget], MarkType.Stop2);
            }
            else{
                if(lowPriorityTarget < 4){
                    accessory.Method.Mark(accessory.Data.PartyList[4], MarkType.Stop1);
                    accessory.Method.Mark(accessory.Data.PartyList[lowPriorityTarget], MarkType.Stop2);
                }
            }
        }
    }
}
