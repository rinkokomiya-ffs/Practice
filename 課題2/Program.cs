using System;
using System.Collections.Generic;

namespace Data_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            // ファイル名をコンソールから入力
            Console.WriteLine("ファイル名を入力してください。");
            string fileName = Console.ReadLine();

            // 人物リスト作成
            List<Person> People = PersonCreater.Create(fileName);

            // 会社ごとに結果を出力する
            // できればExecute.OutputResultの実行は1回にしたい
            Execute.OutputResult(People, "FF");
            Console.WriteLine();
            Execute.OutputResult(People, "FFS");
        }
    }
}