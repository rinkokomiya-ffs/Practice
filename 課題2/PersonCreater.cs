using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Data_practice
{
    /// <summary>
    /// ファイルを読み込んでPersonのリストを作成するクラス
    /// </summary>
    public class PersonCreater
    {
        /// <summary>
        /// ファイルを読み込んでPersonリスト生成処理を実行する
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public static List<Person> Create(string fileName)
        {
            return ConvertPerson(IgnoreAbnormalData(ReadCsvFile(fileName)));
        }

        /// <summary>
        /// ファイルを読み込む
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public static List<string> ReadCsvFile(string fileName)
        {
            // 1. ファイルが存在するかどうか確認する
            // 2. ファイルに読み取り権限があるかどうか確認する
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
        
        /// <summary>
        /// データのエラーチェックを行う
        /// </summary>
        /// <param name="strings">読み込んだデータ</param>
        private static List<string[]> IgnoreAbnormalData(List<string> strings)
        {
            List<string[]> correctStrings = new List<string[]>();
            
            int lineNum = 0;

            foreach(var line in strings)
            {
                lineNum++;
                // 3. 空行がある場合
                if (line == "")
                {
                    ShowErrorMessage(lineNum, "空行が含まれています。");
                    continue;
                }
                
                // データを格納する
                // コンマ区切りのデータを格納
                string[] Data = line.Split(',');

                // 4. データが３つあるかどうか確認する
                if (Data.Length != 3)
                {
                    ShowErrorMessage(lineNum, "データがフォーマットに沿ってません。");
                    continue;
                }

                // Data Validation
                // 5. 社員番号が1から始まる数字8桁かどうか
                // 正規表現で判定する
                bool isId = Regex.IsMatch(Data[0], "^1[0-9]{7}$");
                if(!isId)
                {
                    ShowErrorMessage(lineNum, "社員番号が正しく入力されていません。");
                    continue;
                }

                // 6. 名前が空かどうか
                if(Data[1] == "")
                {
                    ShowErrorMessage(lineNum, "名前が入力されていません。");
                    continue;
                }

                // 7. 文字コードを判定して文字化けしないようにする
                // Data[1] = ConvertEncoding(Data[1]);

                int myMoney = 0;
                // 値段が正常値かどうか確認する
                // 8. 数値でない、もしくは空白の場合
                if (!int.TryParse(Data[2], out myMoney))
                {
                    ShowErrorMessage(lineNum, "お小遣いの金額が正しく入力されていません。");
                    continue;
                }
                // 9. 値が正かどうか確認する
                if(myMoney < 0)
                {
                    ShowErrorMessage(lineNum, "お小遣いの金額が負です");
                    continue;
                }
                
                correctStrings.Add(Data);
            }
            return correctStrings;
        }
        
        /// <summary>
        /// Personのリストを生成する
        /// </summary>
        /// <param name="correctStrings">正常なデータ</param>
        private static List<Person> ConvertPerson(List<string[]> correctStrings)
        {
            // 10. nullだったらメッセージを表示して終了
            if (correctStrings.Count() == 0)
            {
                ErrorExit("計算できるデータはありません。");
            }
            // 各個人の情報を格納するPersonのリストを生成する
            List<Person> People = new List<Person>();
            // 値段をlistに格納する
            foreach(var c in correctStrings)
            {
                People.Add(new Person(c[0], c[1], int.Parse(c[2])));
            }           
            return People;
        }
        
        /// <summary>
        /// エラー発生時に終了する
        /// </summary>
        /// <param name="message">エラー内容</param>
        private static void ErrorExit(string message)
        {
            Console.WriteLine("Error: " + message);
            Environment.Exit(-1);
        }
        
        /// <summary>
        /// 異常データがあった場合に行数とエラーメッセージを表示する
        /// </summary>
        /// <param name="lineNum">行数</param>
        /// <param name="message">エラー内容</param>
        public static void ShowErrorMessage(int lineNum, string message)
        {
            Console.WriteLine("Error: (L" + lineNum + ") " + message);
        }

        /// <summary>
        /// 文字列をUTF-8に変換する
        /// </summary>
        /// <param name="src">変換する文字列</param>
        // private static string ConvertEncoding(string src)
        // {
        //     //まずはバイト配列に変換する
        //     byte[] bytesUTF8 = System.Text.Encoding.Default.GetBytes(src);

        //     //バイト配列をUTF8の文字コードとしてStringに変換する
        //     return System.Text.Encoding.UTF8.GetString(bytesUTF8);
        // }
    }
}