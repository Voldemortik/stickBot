using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestBot
{
    class Program
    {
        static TelegramBotClient botClient;

        static Dictionary<string,ICollection<string>> hotWords = new Dictionary<string, ICollection<string>>()
        {
            {"logs",new HashSet<string>(){"продаетлоги","продастлоги","гдекупитьлоги","куплюлог",
                                          "продаютсялоги","куплюлоги","логипродает","логипродаст",
                                          "логигдекупить","логкуплю","логипродаются","логикуплю"}},
            {"rolls",new HashSet<string>(){"куплюроллку","купитьроллки","роллкипродаются"}},
            {"cryptFiles",new HashSet<string>(){"криптфайла","закриптовать","файлсклеить",
                                                "криптуетфайлы","cryptfiles","crypting",}},
            {"checkCC",new HashSet<string>(){"гдепрочекатьcc","чекнутьcc","чекнутьцц","навалидсс"}},
            {"giftsKeys",new HashSet<string>(){"скупагифтов","скупагифтов","скупключей","скупаключей","скупгифтов"}},
            {"пробивюсаинфо",new HashSet<string>(){"пробивюсаинфо","пробьетссн","пробьетдоб",
                                                   "пробьеткр","пробьетбг"}},
            {"paint",new HashSet<string>(){"отрисуйте","отрисует","отрисовать","отрисовка","отрисовку"}},
            {"саморегиmovo",new HashSet<string>(){"пробьетанрн","пробиванрн","саморегимово",
                                                  "саморегиmovo","саморегичайм","саморегиchime","пробивоминфы"}},
            {"обналпалки",new HashSet<string>(){"обналомсаморегов","обналомсамореговпп","обналомсамореговpp",
                                                "сналитпалкусаморег","обналпалки"}},
            {"personInfo",new HashSet<string>(){"ктопробьетчеловека","поисклюбойинформации"}},
            {"traffic",new HashSet<string>(){"трафикомфб","трафикфб","проливфб"}},
            {"brut",new HashSet<string>(){"гдеможнокупитьбрут","купитьбрут",",бруткупить"}},
            {"wikicard",new HashSet<string>(){"викикард","вики","wikicard"}}, 
            {"mrdr",new HashSet<string>(){"рекламамордор","mrdr"}},
            {"docs",new HashSet<string>(){"документыпродаст","юсыдокипродает","докипродаст",
                                          "продаетдокументы","id,dlbuy","селфиспаспортомкуплю"}},
            {"merc",new HashSet<string>(){"ппмерч","мерчпалки","мерчpp","мерчпп"}}, 
            {"прозвон",new HashSet<string>(){"прозвон","прозвоном","прозвонить"}},
            {"yandexFood",new HashSet<string>(){"яндекседу","яндекссделает","ктосделаетеды",
                                                "едуcделать","скидканаеду"}},
            {"stickers",new HashSet<string>(){"ктоэтистикерыделает","ктостикерыделает","стикерыделает",
                                              "хочутожестикер","стикерпак","заебалэтотботсостикерами"}}, 
            {"coder",new HashSet<string>(){"нуженкодер","подскажитекодера","кодеркрипт","крипткодер"}},
            
        };

        static Dictionary<long,Dictionary<string,DateTime>>  keyWaitDict;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("1043638953:AAGxLGVc3GuDlBYvMYZn_gscWUgWJeqdC4Y");
            keyWaitDict = new Dictionary<long, Dictionary<string, DateTime>>();
            Console.WriteLine($"Bot is working");
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);

        }
        
        static public string findKey(string msg){
            var resKey = string.Empty;
                 foreach(var key in hotWords.Keys){
                     foreach(var value in hotWords[key]){
                         if(msg.Contains(value)){
                            resKey = key;
                            break;
                         }
                     }
                 }
            return resKey;
        } 
    
        
        static  bool isWaits(long chatKey,string hotKey){
                bool result = true; 
            if(keyWaitDict.ContainsKey(chatKey)){
                if(keyWaitDict[chatKey].ContainsKey(hotKey)){
                    result = Math.Abs((keyWaitDict[chatKey][hotKey].Subtract(DateTime.Now)).Minutes)<2;
                   // Console.WriteLine($"res:{result} {Math.Abs((keyWaitDict[chatKey][hotKey].Subtract(DateTime.Now)).Minutes)}");
                }
                else{
                    keyWaitDict[chatKey].Add(hotKey,DateTime.Now);
                    result=false;
                  //  Console.WriteLine($"resф:{result} {Math.Abs((keyWaitDict[chatKey][hotKey].Subtract(DateTime.Now)).Minutes)}");
                }
            }
            else{
                keyWaitDict.Add(chatKey,new Dictionary<string, DateTime>(){{hotKey,DateTime.Now}});
                result = false;
              //  Console.WriteLine($"res3:{result} {Math.Abs((keyWaitDict[chatKey][hotKey].Subtract(DateTime.Now)).Minutes)}");
            }
                
           return result;
        }

        static async Task keySwitch(string key,MessageEventArgs e){
            switch(key)
            {
                    case "logs":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMLXkG5WalKUSqSrEYMC_zSpVgaVD4AAloAAybJahGizunWqo9dhBgE" ,
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "rolls":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMJXkG5VbBP7o_XDybNx54zARaTKBoAAkwAAybJahGprcZ7jxbM5xgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "cryptFiles":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMNXkG5WnDAwdLzAu8sEs4kbOJGPXMAAk4AAybJahFiUWSK6zabNxgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "checkCC":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker:"CAACAgIAAxkBAAMPXkG5WyLGgFC5EyTccUMndPJrK6oAAk8AAybJahFa4E63-PqzAhgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "giftsKeys":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMTXkG5YCnUgY4ksoS00Vo6DIoRY1sAAmgAAybJahGZjWwK8bFhRhgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "пробивюсаинфо":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMVXkG5YtHYWUnc_o-_HwU2jGT42YAAAlMAAybJahEYqFv0nAJndRgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "paint":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMXXkG5ZHMG-2I6lUBBq-tD8zsXm80AAlQAAybJahH03KWndEuFxhgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "саморегиmovo":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMZXkG5ZuSl4xM1S94bFzr2oQxUBt4AAlUAAybJahGEFZE6QiF8XRgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "обналпалки":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMbXkG5Z2AQJhHWqDA5FjAQc6a7Wz0AAlsAAybJahFZc8HZlH_KURgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "personInfo":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMdXkG5aFNWrjnqJ5EWIW0IYfS-8-YAAl4AAybJahFS_F8saqDThBgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "traffic":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMfXkG5avmzFXsrizfSd03YH4saWb4AAl8AAybJahHrejdL1p_0HxgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "brut":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMhXkG5awHLvR8VqpXhUo0r9c5NYacAAmoAAybJahHbfV4S0yjhnBgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "wikicard":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker:"CAACAgIAAxkBAAMjXkG5bRI2b6ldUkoL-cq_BE3DQmIAAmAAAybJahEtINxoPF-MSxgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "mrdr":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker:"CAACAgIAAxkBAAMlXkG5buQIHgQoaFFFslqKwPbvbdIAAmEAAybJahHMQe6R-pDV3hgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "docs":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker:"CAACAgIAAxkBAAMnXkG5cHfn1hl0iOdsTvvm0yV0n40AAmIAAybJahFsa5g8IhXsCxgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "merc":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMpXkG5cSzKPdo9S3oYcy_EhNhvBtkAAmMAAybJahEYNzESE4y_DRgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "прозвон":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMrXkG5cqtYApri8_KFhyqjE7ejv-UAAmUAAybJahHq9qxYAAE5s78YBA",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "yandexFood":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker:"CAACAgIAAxkBAAMtXkG5c-mCt0zT-Wj0DOSUUjd0k3UAAmYAAybJahHr8jlfkYpfRhgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "stickers":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker: "CAACAgIAAxkBAAMvXkG5dcsxpTfY89imMHmkvKRHfH0AAlcAAybJahEmoDOu1eelCRgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                    case "coder":
                        await botClient.SendStickerAsync(
                            chatId: e.Message.Chat,
                            sticker:"CAACAgIAAxkBAAMRXkG5XJy4PQunbBKZ2DGBYt_dFWIAAlEAAybJahGK2oE_xa17AxgE",
                            replyToMessageId: e.Message.MessageId
                    );
                    break;
                
            }
        }
        static async void Bot_OnMessage(object sender,MessageEventArgs e){ 
            if(e.Message.Text!=null)
        {

                 var withoutSpacesMsg = e.Message.Text.ToLower().Replace(" ","");
                 var key = findKey(withoutSpacesMsg);
                 if(key!=string.Empty)
                    if(!isWaits(e.Message.Chat.Id,key)){
                        keyWaitDict[e.Message.Chat.Id][key] = DateTime.Now;
                        await keySwitch(key,e);
                    }
            
        }
            
        }
    }
}
