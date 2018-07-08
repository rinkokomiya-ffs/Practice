using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Data_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            // 集約例外ハンドラの登録
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // ファイル名をコンソールから入力する
            // Console.WriteLine("CSVファイル名を入力してください。");
            // string fileName = Console.ReadLine();

            //try
            //{
                string fileName = "SampleData.txt";
            
                // 人物リスト作成
                List<Person> People = PersonCreater.Create(fileName);

                // 会社ごとに結果を出力する
                Execute.OutputResult(People, "FF");
                Execute.OutputResult(People, "FFS");
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("想定外のエラーが発生しました。");
            //    Console.WriteLine(ex.ToString());
            //}
        }

        /// <summary>
        /// 集約例外ハンドラ（想定外のエラーをキャッチする）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // 11. ファイルが壊れている 
            // 12. ファイルがロックされている
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                //エラー処理
                Console.WriteLine("集約例外ハンドラで例外をキャッチしました。アプリケーションを終了します。");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Environment.Exit(-1); 
            }
        }

    }
}