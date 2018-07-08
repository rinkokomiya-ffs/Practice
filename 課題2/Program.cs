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
            // AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //ファイル名をコンソールから入力する
            // Console.WriteLine("CSVファイル名を入力してください。");
            // string fileName = Console.ReadLine();
            try
            {
                string fileName = "SampleData.txt";
            
                // 人物リスト作成
                List<Person> People = PersonCreater.Create(fileName);

                // 会社ごとに結果を出力する
                Execute.OutputResult(People, "FF");
                Execute.OutputResult(People, "FFS");
            }
            catch(Exception ex)
            {
                Console.WriteLine("想定外のエラーが発生しました。");
                Console.WriteLine(ex.ToString());
            }
        }

        // private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        // {
        //     var exception = e.ExceptionObject as Exception;
        //     if (exception == null)
        //     {
        //         Console.WriteLine("System.Exceptionとして扱えない例外");
        //         return;
        //     }
        // }

    }
}