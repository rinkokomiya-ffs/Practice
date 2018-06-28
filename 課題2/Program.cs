using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            // ファイル名をコンソールから入力する
            Console.WriteLine("ファイル名を入力してください。");
            string textFile = Console.ReadLine();

            // 人物リスト生成
            var People = ReadCsvFile(textFile);

            // お小遣いの平均を表示
            ShowMoneyAverage(People);

            // お小遣いの最大値を表示
            ShowMoneyMax(People);
        }

        /// <summary>
        /// お小遣いの平均値を表示する
        /// </summary>
        /// <param name="money">格納した人物リスト</param>
        public static void ShowMoneyAverage(List<Person> People)
        {
            // 格納した値段の平均値をコンソールに出力する
            Console.WriteLine("お小遣いの平均値:" + People.Average(x => x.money));
        }

        /// <summary>
        /// お小遣いの最大値を表示する
        /// </summary>
        /// <param name="People">格納した人物リスト</param>
        public static void ShowMoneyMax(List<Person> People)
        {
            // 最大値を格納
            var maxValue = People.Max(x => x.money);

            // 最大値のときの人物情報をリストに格納
            List<Person> richPeople = new List<Person>();
            richPeople = People.Where(p => p.money == maxValue).ToList();

            // 格納した値段の最大値とその人物をコンソールに出力する
            Console.WriteLine("お小遣いの最大値:" + maxValue );
            Console.WriteLine("お小遣いが最大値の人:");
            foreach (var p in richPeople)
            {
                Console.WriteLine(p.name);
            }
        }

        /// <summary>
        /// お小遣いの最小値を表示する
        /// </summary>
        /// <param name="People">格納した人物リスト</param>
        public static void ShowMoneyMin(List<Person> People)
        {
            // 最小値を格納
            var minValue = People.Min(x => x.money);

            // 最小値のときの人物情報をリストに格納
            List<Person> poorPeople = new List<Person>();
            poorPeople = People.Where(p => p.money == minValue).ToList();

            // 格納した値段の最大値とその人物をコンソールに出力する
            Console.WriteLine("お小遣いの最小値:" + minValue);
            Console.WriteLine("お小遣いが最小値の人:");
            foreach (var p in poorPeople)
            {
                Console.WriteLine(p.name);
            }
        }

        public static List<Person> CalcMoney(List<Person> People, string type)
        {
            int targetMoney = 0;
            switch (type)
            {
                case "Max":
                    // 最大値を格納
                    targetMoney = People.Max(x => x.money);
                    break;

                case "Min":
                    // 最小値を格納
                    targetMoney = People.Min(x => x.money);
                    break;
                default:
                    throw new ArgumentException("型指定が正しくありません");
            }

            // 最大値もしくは最小値の人物情報をリストに格納
            return People.Where(p => p.money == targetMoney).ToList();
        }

        /// <summary>
        /// ファイルを読み込んでリストを生成する
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public static List<Person> ReadCsvFile(string fileName)
        {
            // ファイルが存在するかどうか確認する
            // ファイルが存在しない場合
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Error: ファイルが存在しません。");
                return null;
            }
            // ファイルが存在する場合
            // 各個人の情報を格納するPersonのリストを生成する
            List<Person> people = new List<Person>();

            using (StreamReader sr = new StreamReader(fileName))
            {
            	// 行数をカウント
            	int lineNum = 0;
                
                // ファイルの末尾まで読み込む
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    lineNum++;

                    // 空行がある場合
                    if (line == "")
                    {
                        ShowErrorMessage(lineNum, "空行が含まれています。");
                        continue;
                    }

                    // データを格納する
                    
                    // コンマ区切りのデータを格納
                    string[] Data = line.Split(',');

                    // データが３つあるかどうか確認する
                    // ３つない場合
                    if (Data.Length != 3)
                    {
                        ShowErrorMessage(lineNum, "データがフォーマットに沿ってません。");
                        continue;
                    }

                    int myMoney = 0;
                    // 値段が正常値かどうか確認する
                    // 数値でない、もしくは空白の場合
                    if (!int.TryParse(Data[2], out myMoney))
                    {
                        ShowErrorMessage(lineNum, "お小遣いの金額が正しく入力されていません。");
                        continue;
                    }

                    // 値段をlistに格納する
                    people.Add(new Person(int.Parse(Data[0]), Data[1], myMoney));
                }
            }

            // nullだったらメッセージを表示して終了
            if(people == null)
            {
                Console.WriteLine("Error: 計算できるデータはありません。");
                Environment.Exit(-1);
            }

            return people;
        }

        /// <summary>
        /// 異常データがあった場合に行数とエラーメッセージを表示
        /// </summary>
        /// <param name="lineNum">行数</param>
        /// <param name="message">エラー内容</param>
        public static void ShowErrorMessage(int lineNum, string message)
        {
            Console.WriteLine("Error: (L" + lineNum + ") " + message);
        }
    }

    
    /// <summary>
    /// 個人情報を格納するクラス
    /// </summary>
    public class Person
    {
    	public int id {get; set;}
    	public string name {get; set;}
    	public int money {get; set;}
    	public string company {get; set;}
    	
    	public Person(int id, string name, int money)
    	{
    		this.id = id;
    		this.name = name;
    		this.money = money;
    		company = CheckCompany(id);
    	}
    	
    	/// <summary>
        /// 社員番号の上3桁から会社名を判別する
        /// </summary>
        /// <param name="id">社員番号</param>
    	private string CheckCompany(int id)
    	{
    		int topThreeDigit = id / 1000000;
    		if(topThreeDigit == 128)
            {
                return "FFS";

            }
            else if(topThreeDigit == 120)
            {
                return "FF";

            }
            else
            {
                return "unknown";
            }
        }
    }
}

