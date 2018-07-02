using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Data_practice
{
    /// <summary>
    /// ファイルを読み込んで人物リストを生成する
    /// </summary>
    public class PersonCreater
    {
        /// <summary>
        /// 生成した人物リストを返す
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<Person> Create(string fileName)
            => ConvertPerson(CheckAbnormalData(ReadCsvFile(fileName)));

        /// <summary>
        /// csv形式のファイルを読む
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>ファイル1行ごとのリスト</returns>
        private static List<string> ReadCsvFile(string fileName)
        {
            // ファイルが存在するかどうか確認する
            if (!File.Exists(fileName))
            {
                // ファイルが存在しない場合は終了する
                ErrorExit("ファイルが存在しません。");
            }
            
            // ファイルが存在する場合
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                // ファイルの末尾まで読み込む
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    lines.Add(line);
                }
            }
            return lines;
        }

        /// <summary>
        /// 異常データがあるか確認する
        /// </summary>
        /// <param name="lines">ファイル1行ごとのリスト</param>
        /// <returns>正常データ</returns>
        private static List<string[]> CheckAbnormalData(List<string> lines)
        {
            List<string[]> CorrectData = new List<string[]>();

            // 行数
            int lineNum = 0;

            foreach (var line in lines)
            {
                lineNum++;
                // 空行がある場合
                if (line == "")
                {
                    ShowErrorMessage(lineNum, "空行が含まれています。");
                    continue;
                }

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

                CorrectData.Add(Data);
            }
            return CorrectData;
        }

        /// <summary>
        /// 読み込んだ正常データを人物リストに変換する
        /// </summary>
        /// <param name="CorrectData">正常データ</param>
        /// <returns>人物リスト</returns>
        private static List<Person> ConvertPerson(List<string[]> CorrectData)
        {
            // nullだったらメッセージを表示して終了
            if (CorrectData.Count() == 0)
            {
                ErrorExit("計算できるデータはありません。");
            }

            // 各人物の情報を格納するPersonのリストを生成する
            List<Person> People = new List<Person>();
            
            // 人物情報をlistに格納する
            foreach (var c in CorrectData)
            {
                People.Add(new Person(int.Parse(c[0]), c[1], int.Parse(c[2])));
            }
            return People;
        }

        /// <summary>
        /// エラーが出た場合終了
        /// </summary>
        /// <param name="message">エラー内容</param>
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
        private static void ShowErrorMessage(int lineNum, string message)
        {
            Console.WriteLine("Error: (L" + lineNum + ") " + message);
        }
    }
}