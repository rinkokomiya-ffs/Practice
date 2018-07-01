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
            string fileName = Console.ReadLine();
            
            // 人物リスト作成
            List<Person> People = PersonCreater.Create(fileName);

            // 会社ごとに結果を出力する
            Execute.OutputResult(People, "FF");
            Execute.OutputResult(People, "FFS");
        }
    }
}