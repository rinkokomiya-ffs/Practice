using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_practice
{
    // ファイルを読み込んでリストを生成する
    public class PersonCreater
    {
        public static List<string> ReadCsvFile(string fileName)
        {
            // ファイルが存在するかどうか確認する
            if (!File.Exists(fileName))
            {
                // ファイルが存在しない場合は終了する
                ErrorExit("ファイルが存在しません。");
            }
            // ファイルが存在する場合
            // stringのリストを追加
                List<string> strings = new List<string>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                // ファイルの末尾まで読み込む
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    strings.Add(line);
                }
                
            }
            return strings;
        }
        
        // エラーチェック
        private static List<string[]> IgnoreAbnormalData(List<string> strings)
        {
            List<string[]> correctStrings = new List<string[]>();
            
            int lineNum = 0;

            foreach(var line in strings)
            {
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
                
                correctStrings.Add(Data);
            }
            return correctStrings;
        }
        
        private static List<Person> ConvertPerson(List<string[]> correctStrings)
        {
            // nullだったらメッセージを表示して終了
            if (correctStrings.Count() == 0)
            {
                ErrorExit("計算できるデータはありません。");
            }
            // 各個人の情報を格納するPersonのリストを生成する
            List<Person> People = new List<Person>();
            // 値段をlistに格納する
            foreach(var c in correctStrings)
            {
                People.Add(new Person(int.Parse(c[0]), c[1], int.Parse(c[2])));
            }
            
            return People;
        }


        public static List<Person> Create(string fileName)
        {
            return ConvertPerson(IgnoreAbnormalData(ReadCsvFile(fileName)));
            
        }
        
        // エラーが出たら終了する
        private static void ErrorExit(string message)
        {
            Console.WriteLine("Error: " + message);
            Environment.Exit(-1);
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
}