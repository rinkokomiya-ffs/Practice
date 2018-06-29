using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileIO_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            // ファイル名をコンソールから入力する
            Console.WriteLine("ファイル名を入力してください。");
            string textFile = Console.ReadLine();

            // お小遣いの平均を表示
            ShowMoneyAverage(readCsvFile(textFile));
        }

        /// <summary>
        /// 受け取ったリストの平均値を表示する
        /// </summary>
        /// <param name="money"></param>
        public static void ShowMoneyAverage(List<int> money)
        {
            // listが空の場合
            if (money.Count == 0)
            {
                ShowErrorMessage("計算できるデータはありません。");
                return;
            }

            // 格納した値段の平均値をコンソールに出力する
            Console.WriteLine("お小遣いの平均値:" + money.Average());
        }

        /// <summary>
        /// ファイルを読み込んでリストを生成する
        /// </summary>
        /// <param name="textFile"></param>
        public static List<int> readCsvFile(string textFile)
        {
            // ファイルが存在するかどうか確認する
            // ファイルが存在しない場合
            if (!System.IO.File.Exists(textFile))
            {
                ShowErrorMessage("ファイルが存在しません。");
                Environment.Exit(-1);
            }
            // ファイルが存在する場合
            // 各個人の値段を格納するリストを生成する
            List<int> money = new List<int>();

            using (StreamReader sr = new StreamReader(textFile))
            {
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();

                    // 空行がある場合
                    if (line == "")
                    {
                        ShowErrorMessage("空行が含まれています。");
                        continue;
                    }

                    // データを格納する
                    string[] Data = line.Split(',');

                    // データが３つあるかどうか確認する
                    // ３つない場合
                    if (Data.Length != 3)
                    {
                        ShowErrorMessage("データがフォーマットに沿ってません。");
                        continue;
                    }

                    int myMoney = 0;
                    // 値段が正常値かどうか確認する
                    // 数値でない、もしくは空白の場合
                    if (!int.TryParse(Data[2], out myMoney))
                    {
                        ShowErrorMessage("お小遣いの金額が正しく入力されていません。");
                        continue;
                    }

                    // 値段をlistに格納する
                    money.Add(myMoney);
                }
            }

            return money;
        }

        /// <summary>
        /// 異常データがあった場合にエラーメッセージを表示
        /// </summary>
        /// <param name="message">エラー内容</param>
        public static void ShowErrorMessage(string message)
        {
            Console.WriteLine("Error:" + message);
        }
    }
}




//[C:佐藤　全般]
//doxy形式でコメントを書いてくれていたのでとても見やすかったです。
//実際doxygenを動かしてみると仕様書が生成されました。
//https://drive.google.com/open?id=19_wkRgWYQfU-bpETNKavpoW1N6uumsHu
//⇒解凍後のフォルダに入っている
//　class_file_i_o__practice_1_1_program.html が
//　doxygenで自動生成した仕様書です。　

//[C:佐藤　L16]
//開く対象のファイルをユーザが選べるようになっておりGoodです。
//テストするときも対象ファイルを切り替えやすかったのではないでしょうか。

//[C:佐藤　L19]
//小さな機能単位で関数を分けるのは大事なことです。
//本で学習した内容が実践できていると思います。
//（名前の付け方も具体的で良いです。）

//[W:佐藤　L49 ほか]
//異常系それぞれについて異常要因を画面に表示しているところはGoodです。
//1つ要望として、いまのエラーメッセージだとデータファイルの
//何が間違っているかは分かっても、どこが間違っているかまでは
//特定できません。

//データに誤りが含まれていた場合、データを作った人が
//ファイルを開いてすぐに修正できるように、
//どのデータが間違っていたのか具体的に特定できるような
//情報を足してみてください。

//[C:佐藤　全体]
//全体的にC#が提供しているライブラリを上手に使いこなしているようで、
//実装時にMSDN等のサイトをよく調べているのかなと思いました。
//無駄のない実装になっておりとても良いです。

//# 例外を使わずに全てIF文にしているのには
//# 何かポリシーがあるのでしょうか…？
//# レビューする側としては小宮さんの理解度が
//# 見やすかったのですが、ちょっと気になりました。
