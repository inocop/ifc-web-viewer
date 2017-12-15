using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ifc_web_viewer.Models
{
    public class IFCConvertModel
    {
        public void ConvertIfcToObj(string ifcFilePath)
        {
            var process = new Process();

            process.StartInfo.FileName = "IfcConvert";          // コマンド名
            process.StartInfo.Arguments = ifcFilePath;          // 引数
            process.StartInfo.CreateNoWindow = true;            // DOSプロンプトの黒い画面を非表示
            process.StartInfo.UseShellExecute = false;          // プロセスを新しいウィンドウで起動するか否か
            process.StartInfo.RedirectStandardOutput = true;    // 標準出力をリダイレクトして取得したい
            process.Start();
        }


        public async Task ConvertIfcToObjAsync(string ifcFilePath)
        {
            await Task.Run(() => ConvertIfcToObj(ifcFilePath));
        }
    }
}
