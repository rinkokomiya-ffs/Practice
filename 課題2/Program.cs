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

            // 会社ごとのリスト生成
            var FFSPeople = makePerCompany(People, "FF");
            var FFPeople = makePerCompany(People, "FFS");

            // 会社ごとにお小遣いの最大値を表示
            Console.WriteLine("FF");
            ShowMoney(CalcMoney(FFPeople, "最大値"), "最大値");
            Console.WriteLine("FFS");
            ShowMoney(CalcMoney(FFSPeople, "最大値"), "最大値");
        }

        /// <summary>
        /// 会社ごとのリストを生成する
        /// </summary>
        /// <param name="People"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        private static List<Person> makePerCompany(List<Person> People, string companyName)
             => People.Where(p => p.company == companyName).ToList();

        public static void Execute(List<Person> People, string companyName)
        {
            // リスト生成
            var targetPeople = makePerCompany(People, companyName);

            // 平均値を計算する
            string str = "平均値";
            // 平均値を表示する
            var value = CalcMoney(targetPeople, str);
            ShowMoney(value, str);

            // 最大値を計算する
            string str1 = "最大値";
            // 最大値を表示する
            var value1 = CalcMoney(targetPeople, str1);
            ShowMoney(value1, str1);
            ShowName(SetTargetPeople(targetPeople, value), str1);
                
        }

        /// <summary>
        /// 格納した値をコンソールに出力する
        /// </summary>
        /// <param name="targetPeople"></param>
        /// <param name="type"></param>
        public static void ShowMoney(int value, string type)
        {
            Console.WriteLine("お小遣いの" + type + "：" + value);
        }

        /// <summary>
        /// 名前をコンソールに出力する
        /// </summary>
        /// <param name="targetPeople"></param>
        /// <param name="type"></param>
        public static void ShowName(List<Person> targetPeople, string type)
        {
            Console.WriteLine("お小遣いが" + type + "の人:");
            foreach (var p in targetPeople)
            {
                Console.WriteLine(p.name);
            }
        }

        /// <summary>
        /// 引数に応じた値を計算する
        /// </summary>
        /// <param name="People"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int CalcMoney(List<Person> People, string type)
        {
            switch (type)
            {
                case "平均値":
                    return (int)People.Average(x => x.money);

                case "最大値":
                    return People.Max(x => x.money);

                case "最小値":
                    return People.Min(x => x.money);
                default:
                    throw new ArgumentException("型指定が正しくありません");
            }
        }

        /// <summary>
        /// お小遣いの金額に一致する人物情報をリストに格納
        /// </summary>
        /// <param name="People"></param>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public static List<Person> SetTargetPeople(List<Person> People,int targetValue)
        {
            return People.Where(p => p.money == targetValue).ToList();
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
            if (people == null)
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
        public int id { get; set; }
        public string name { get; set; }
        public int money { get; set; }
        public string company { get; set; }

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
            int topThreeDigit = id / 100000;
            if (topThreeDigit == 128)
            {
                return "FFS";

            }
            else if (topThreeDigit == 100)
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

